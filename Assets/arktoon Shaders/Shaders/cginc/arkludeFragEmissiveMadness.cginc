float4 frag(VertexOutput i) : COLOR {

    float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir * lerp(1, i.faceSign, _DoubleSidedFlipBackfaceNormal));
    float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
    float3 _BumpMap_var = UnpackScaleNormal(tex2D(REF_BUMPMAP,TRANSFORM_TEX(i.uv0, REF_BUMPMAP)), REF_BUMPSCALE);
    float3 normalLocal = _BumpMap_var.rgb;
    float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz + float3(0, +0.0000000001, 0));
    float3 lightColor = _LightColor0.rgb;
    float3 halfDirection = normalize(viewDirection+lightDirection);

    #if !defined(SHADOWS_SCREEN)
        float attenuation = 1;
    #else
        UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
    #endif

    float4 _MainTex_var = UNITY_SAMPLE_TEX2D(REF_MAINTEX, TRANSFORM_TEX(i.uv0, REF_MAINTEX));
    float3 Diffuse = (_MainTex_var.rgb*REF_COLOR.rgb);
    Diffuse = lerp(Diffuse, Diffuse * i.color,_VertexColorBlendDiffuse);

    // アウトラインであればDiffuseとColorを混ぜる
    if (_OutlineUseColorShift) {
        float3 Outline_Diff_HSV = CalculateHSV((Diffuse * _OutlineTextureColorRate + i.col * (1 - _OutlineTextureColorRate)), _OutlineHueShiftFromBase, _OutlineSaturationFromBase, _OutlineValueFromBase);
        Diffuse = lerp(Diffuse, Outline_Diff_HSV, i.isOutline);
    } else {
        Diffuse = lerp(Diffuse, (Diffuse * _OutlineTextureColorRate + i.col * (1 - _OutlineTextureColorRate)), i.isOutline);
    }

    #ifdef ARKTOON_CUTOUT
        clip((_MainTex_var.a * REF_COLOR.a) - _CutoutCutoutAdjust);
    #endif

    #if defined(ARKTOON_CUTOUT) || defined(ARKTOON_FADE)
        if (i.isOutline) {
            float _OutlineMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_OutlineMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _OutlineMask)).r;
            clip(_OutlineMask_var.r - _OutlineCutoffRange);
        }
    #endif

    // 光源サンプリング方法(0:Arktoon, 1:Cubed)
    float3 ShadeSH9Plus = float3(0,0,0);
    float3 ShadeSH9Minus = float3(0,0,0);
    if (_LightSampling == 0) {
        // 明るい部分と暗い部分をサンプリング・グレースケールでリマッピングして全面の光量を再計算
        ShadeSH9Plus = GetSHLength();
        ShadeSH9Minus = ShadeSH9(float4(0,0,0,1));
    } else {
        // 空間上、真上を向いたときの光と真下を向いたときの光でサンプリング
        ShadeSH9Plus = ShadeSH9Direct();
        ShadeSH9Minus = ShadeSH9Indirect();
    }

    // 陰の計算
    float3 directLighting = saturate((ShadeSH9Plus+lightColor));
    ShadeSH9Minus *= _ShadowIndirectIntensity;
    float3 indirectLighting = saturate(ShadeSH9Minus);

    float3 grayscale_vector = grayscale_vector_node();
    float grayscalelightcolor = dot(lightColor,grayscale_vector);
    float grayscaleDirectLighting = (((dot(lightDirection,normalDirection)*0.5+0.5)*grayscalelightcolor*attenuation)+dot(ShadeSH9Normal( normalDirection ),grayscale_vector));
    float bottomIndirectLighting = dot(ShadeSH9Minus,grayscale_vector);
    float topIndirectLighting = dot(ShadeSH9Plus,grayscale_vector);
    float lightDifference = ((topIndirectLighting+grayscalelightcolor)-bottomIndirectLighting);
    float remappedLight = ((grayscaleDirectLighting-bottomIndirectLighting)/lightDifference);
    float _ShadowStrengthMask_var = tex2D(_ShadowStrengthMask, TRANSFORM_TEX(i.uv0, _ShadowStrengthMask));

    fixed _ShadowborderBlur_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowborderBlurMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ShadowborderBlurMask)).r * _ShadowborderBlur;
    float ShadowborderMin = max(0, _Shadowborder - _ShadowborderBlur_var/2);
    float ShadowborderMax = min(1, _Shadowborder + _ShadowborderBlur_var/2);
    float grayscaleDirectLighting2 = (((dot(lightDirection,normalDirection)*0.5+0.5)*grayscalelightcolor) + dot(ShadeSH9Normal( normalDirection ),grayscale_vector));
    float remappedLight2 = ((grayscaleDirectLighting2-bottomIndirectLighting)/lightDifference);
    float directContribution = 1.0 - ((1.0 - saturate(( (saturate(remappedLight2) - ShadowborderMin)) / (ShadowborderMax - ShadowborderMin))));

    float selfShade = saturate(dot(lightDirection,normalDirection)+1+_OtherShadowAdjust);
    float otherShadow = saturate(saturate((attenuation-0.5)*2)+(1-selfShade)*_OtherShadowBorderSharpness);
    directContribution = lerp(0, directContribution, saturate(1-((1-otherShadow) * saturate(dot(lightColor,grayscale_for_light())*1.5))));

    // #ifdef USE_SHADOW_STEPS
        directContribution = lerp(directContribution, min(1,floor(directContribution * _ShadowSteps) / (_ShadowSteps - 1)), _ShadowUseStep);
    // #endif

    directContribution = 1.0 - (1.0 - directContribution) * _ShadowStrengthMask_var * _ShadowStrength;

    // 光の受光に関する更なる補正
    // ・LightIntensityIfBackface(裏面を描画中に変動する受光倍率)
    // ・ShadowCapのModeがLightShutterの時にかかるマスク乗算
    float additionalContributionMultiplier = 1;
    additionalContributionMultiplier *= i.lightIntensityIfBackface;

    if (_ShadowCapBlendMode == 2) { // Light Shutter
        float3 normalDirectionShadowCap = normalize(mul( float3(normalLocal.r*_ShadowCapNormalMix,normalLocal.g*_ShadowCapNormalMix,normalLocal.b), tangentTransform )); // Perturbed normals
        float2 transformShadowCap = float2(0,0);
        if (_UsePositionRelatedCalc) {
            float3 transformShadowCapViewDir = mul( UNITY_MATRIX_V, float4(viewDirection,0) ).xyz * float3(-1,-1,1) + float3(0,0,1);
            float3 transformShadowCapNormal = mul( UNITY_MATRIX_V, float4(normalDirectionShadowCap,0) ).xyz;
            float2 transformShadowCap_old = transformShadowCapNormal.rg*0.5+0.5;
            transformShadowCapNormal  *= float3(-1,-1,1);
            float3 transformShadowCapCombined = transformShadowCapViewDir * dot(transformShadowCapViewDir, transformShadowCapNormal) / transformShadowCapViewDir.z - transformShadowCapNormal;
            transformShadowCap = lerp(((transformShadowCapCombined.rg*0.5)+0.5), transformShadowCap_old, max(0,transformShadowCapNormal.z));
        } else {
            transformShadowCap = (mul( UNITY_MATRIX_V, float4(normalDirectionShadowCap,0) ).xyz.rg*0.5+0.5);
        }
        float4 _ShadowCapTexture_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowCapTexture, REF_MAINTEX, TRANSFORM_TEX(transformShadowCap, _ShadowCapTexture));
        float4 _ShadowCapBlendMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowCapBlendMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ShadowCapBlendMask));
        additionalContributionMultiplier *= max(0,(1.0 - ((1.0 - (_ShadowCapTexture_var.rgb))*_ShadowCapBlendMask_var.rgb)*_ShadowCapBlend));
    }

    directContribution *= additionalContributionMultiplier;

    // 頂点ライティング：PixelLightから溢れた4光源をそれぞれ計算
    float3 coloredLight_sum = float3(0,0,0);
    if (_UseVertexLight) {
        fixed _PointShadowborderBlur_var = UNITY_SAMPLE_TEX2D_SAMPLER(_PointShadowborderBlurMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _PointShadowborderBlurMask)).r * _PointShadowborderBlur;
        float VertexShadowborderMin = max(0, _PointShadowborder - _PointShadowborderBlur_var/2.0);
        float VertexShadowborderMax = min(1, _PointShadowborder + _PointShadowborderBlur_var/2.0);
        float4 directContributionVertex = 1.0 - ((1.0 - saturate(( (saturate(i.ambientAttenuation) - VertexShadowborderMin)) / (VertexShadowborderMax - VertexShadowborderMin))));
        // #ifdef USE_POINT_SHADOW_STEPS
            directContributionVertex = lerp(directContributionVertex, min(1,floor(directContributionVertex * _PointShadowSteps) / (_PointShadowSteps - 1)), _PointShadowUseStep);
        // #endif
        directContributionVertex *= additionalContributionMultiplier;
        float3 coloredLight_0 = max(directContributionVertex.r * i.lightColor0 * i.ambientAttenuation.r, i.lightColor0 * i.ambientIndirect.r * (1-_PointShadowStrength));
        float3 coloredLight_1 = max(directContributionVertex.g * i.lightColor1 * i.ambientAttenuation.g, i.lightColor1 * i.ambientIndirect.g * (1-_PointShadowStrength));
        float3 coloredLight_2 = max(directContributionVertex.b * i.lightColor2 * i.ambientAttenuation.b, i.lightColor2 * i.ambientIndirect.b * (1-_PointShadowStrength));
        float3 coloredLight_3 = max(directContributionVertex.a * i.lightColor3 * i.ambientAttenuation.a, i.lightColor3 * i.ambientIndirect.a * (1-_PointShadowStrength));
        coloredLight_sum = (coloredLight_0 + coloredLight_1 + coloredLight_2 + coloredLight_3) * _PointAddIntensity;
    }

    float3 finalLight = lerp(indirectLighting,directLighting,directContribution)+coloredLight_sum;

    // カスタム陰を使っている場合、directContributionや直前のfinalLightを使い、finalLightを上書きする
    float3 toonedMap = float3(0,0,0);
    if (_ShadowPlanBUsePlanB) {
        float3 shadeMixValue = lerp(directLighting, finalLight, _ShadowPlanBDefaultShadowMix);
        float3 ShadeMap = float3(0,0,0);
        if (_ShadowPlanBUseCustomShadowTexture) {
            float4 _ShadowPlanBCustomShadowTexture_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowPlanBCustomShadowTexture, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ShadowPlanBCustomShadowTexture));
            float3 shadowCustomTexture = _ShadowPlanBCustomShadowTexture_var.rgb * _ShadowPlanBCustomShadowTextureRGB.rgb;
            ShadeMap = shadowCustomTexture*shadeMixValue;
        } else {
            float3 Diff_HSV = CalculateHSV(Diffuse, _ShadowPlanBHueShiftFromBase, _ShadowPlanBSaturationFromBase, _ShadowPlanBValueFromBase);
            ShadeMap = Diff_HSV*shadeMixValue;
        }

        if (_CustomShadow2nd) {
            float ShadowborderMin2 = max(0, (_ShadowPlanB2border * _Shadowborder) - _ShadowPlanB2borderBlur/2);
            float ShadowborderMax2 = min(1, (_ShadowPlanB2border * _Shadowborder) + _ShadowPlanB2borderBlur/2);
            float directContribution2 = 1.0 - ((1.0 - saturate(( (saturate(remappedLight2) - ShadowborderMin2)) / (ShadowborderMax2 - ShadowborderMin2))));  // /2の部分をパラメーターにしたい
            directContribution2 *= additionalContributionMultiplier;
            float3 ShadeMap2 = float3(0,0,0);
            if (_ShadowPlanB2UseCustomShadowTexture) {
                float4 _ShadowPlanB2CustomShadowTexture_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowPlanB2CustomShadowTexture, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ShadowPlanB2CustomShadowTexture));
                float3 shadowCustomTexture2 = _ShadowPlanB2CustomShadowTexture_var.rgb * _ShadowPlanB2CustomShadowTextureRGB.rgb;
                shadowCustomTexture2 =  lerp(shadowCustomTexture2, shadowCustomTexture2 * i.color,_VertexColorBlendDiffuse); // 頂点カラーを合成
                ShadeMap2 = shadowCustomTexture2*shadeMixValue;
            } else {
                float3 Diff_HSV2 = CalculateHSV(Diffuse, _ShadowPlanB2HueShiftFromBase, _ShadowPlanB2SaturationFromBase, _ShadowPlanB2ValueFromBase);
                ShadeMap2 = Diff_HSV2*shadeMixValue;
            }
            ShadeMap = lerp(ShadeMap2,ShadeMap,directContribution2);
        }

        finalLight = lerp(ShadeMap,directLighting,directContribution)+coloredLight_sum;
        toonedMap = lerp(ShadeMap,Diffuse*finalLight,finalLight);
    } else {
        toonedMap = Diffuse*finalLight;
    }

    // アウトラインであればShadeMixを反映
    toonedMap = lerp(toonedMap, (toonedMap * _OutlineShadeMix + (Diffuse+(Diffuse*coloredLight_sum)) * (1 - _OutlineShadeMix)), i.isOutline);

    // 裏面であればHSVShiftを反映
    if(_DoubleSidedBackfaceUseColorShift) {
        toonedMap = lerp(toonedMap, CalculateHSV(toonedMap, _DoubleSidedBackfaceHueShiftFromBase, _DoubleSidedBackfaceSaturationFromBase, _DoubleSidedBackfaceValueFromBase), i.isBackface);
        Diffuse = lerp(Diffuse, CalculateHSV(Diffuse, _DoubleSidedBackfaceHueShiftFromBase, _DoubleSidedBackfaceSaturationFromBase, _DoubleSidedBackfaceValueFromBase), i.isBackface);
    }

    float3 ReflectionMap = float3(0,0,0);
    float3 specular = float3(0,0,0);
    float3 matcap = float3(0,0,0);
    float3 RimLight = float3(0,0,0);
    float3 shadowcap = float3(1000,1000,1000);

    #if !defined(ARKTOON_REFRACTED)
    if (_UseOutline == 0 || !i.isOutline) {
    #endif
        // オプション：Reflection
        if (_UseReflection) {
            float3 normalDirectionReflection = normalize(mul( float3(normalLocal.r*_ReflectionNormalMix,normalLocal.g*_ReflectionNormalMix,normalLocal.b), tangentTransform ));
            float reflNdotV = abs(dot( normalDirectionReflection, viewDirection ));
            float _ReflectionSmoothnessMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ReflectionReflectionMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ReflectionReflectionMask));
            float reflectionSmoothness = _ReflectionReflectionPower*_ReflectionSmoothnessMask_var;
            float perceptualRoughnessRefl = 1.0 - reflectionSmoothness;
            float3 reflDir = reflect(-viewDirection, normalDirectionReflection);
            float roughnessRefl = SmoothnessToRoughness(reflectionSmoothness);
            float3 indirectSpecular = float3(0,0,0);
            if (_UseReflectionProbe) {
                indirectSpecular = GetIndirectSpecular(lightColor, lightDirection,
                    normalDirectionReflection, viewDirection, reflDir, attenuation, roughnessRefl, i.posWorld.xyz
                );
                if (any(indirectSpecular.xyz) == 0) indirectSpecular = GetIndirectSpecularCubemap(_ReflectionCubemap, _ReflectionCubemap_HDR, reflDir, roughnessRefl);
            } else {
                indirectSpecular = GetIndirectSpecularCubemap(_ReflectionCubemap, _ReflectionCubemap_HDR, reflDir, roughnessRefl);
            }
            float3 specularColorRefl = reflectionSmoothness;
            float specularMonochromeRefl;
            float3 diffuseColorRefl = Diffuse;
            diffuseColorRefl = DiffuseAndSpecularFromMetallic( diffuseColorRefl, specularColorRefl, specularColorRefl, specularMonochromeRefl );
            specularMonochromeRefl = 1.0-specularMonochromeRefl;
            half grazingTermRefl = saturate( reflectionSmoothness + specularMonochromeRefl );
            #ifdef UNITY_COLORSPACE_GAMMA
                half surfaceReduction = 1.0-0.28*roughnessRefl*perceptualRoughnessRefl;
            #else
                half surfaceReduction = 1.0/(roughnessRefl*roughnessRefl + 1.0);
            #endif
            indirectSpecular *= FresnelLerp (specularColorRefl, grazingTermRefl, reflNdotV);
            indirectSpecular *= surfaceReduction *lerp(float3(1,1,1), finalLight,_ReflectionShadeMix);
            float reflSuppress = _ReflectionSuppressBaseColorValue * reflectionSmoothness;
            toonedMap = lerp(toonedMap,toonedMap * (1-surfaceReduction), reflSuppress);
            ReflectionMap = indirectSpecular*lerp(float3(1,1,1), finalLight,_ReflectionShadeMix);
        }

        // オプション：Gloss
        if(_UseGloss) {
            float glossNdotV = abs(dot( normalDirection, viewDirection ));
            float _GlossBlendMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_GlossBlendMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _GlossBlendMask));
            float gloss = _GlossBlend * _GlossBlendMask_var;
            float perceptualRoughness = 1.0 - gloss;
            float roughness = perceptualRoughness * perceptualRoughness;
            float specPow = exp2( gloss * 10.0+1.0);
            float NdotL = saturate(dot( normalDirection, lightDirection ));
            float LdotH = saturate(dot(lightDirection, halfDirection));
            float3 specularColor = _GlossPower;
            float specularMonochrome;
            float3 diffuseColor = Diffuse;
            diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
            specularMonochrome = 1.0-specularMonochrome;
            float NdotH = saturate(dot( normalDirection, halfDirection ));
            float VdotH = saturate(dot( viewDirection, halfDirection ));
            float visTerm = SmithJointGGXVisibilityTerm( NdotL, glossNdotV, roughness );
            float normTerm = GGXTerm(NdotH, roughness);
            float specularPBL = (visTerm*normTerm) * UNITY_PI;
            #ifdef UNITY_COLORSPACE_GAMMA
                specularPBL = sqrt(max(1e-4h, specularPBL));
            #endif
            specularPBL = max(0, specularPBL * NdotL);
            #if defined(_SPECULARHIGHLIGHTS_OFF)
                specularPBL = 0.0;
            #endif
            specularPBL *= any(specularColor) ? 1.0 : 0.0;
            float3 attenColor = attenuation * _LightColor0.xyz;
            float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
            half grazingTerm = saturate( gloss + specularMonochrome );
            specular = attenuation * directSpecular * _GlossColor.rgb;
        }

        // オプション：MatCap
        if (_MatcapBlendMode < 3) {
            float3 normalDirectionMatcap = normalize(mul( float3(normalLocal.r*_MatcapNormalMix,normalLocal.g*_MatcapNormalMix,normalLocal.b), tangentTransform )); // Perturbed normals
            float2 transformMatcap = float2(0,0);
            if (_UsePositionRelatedCalc) {
                float3 transformMatcapViewDir = mul( UNITY_MATRIX_V, float4(viewDirection,0) ).xyz * float3(-1,-1,1) + float3(0,0,1);
                float3 transformMatcapNormal = mul( UNITY_MATRIX_V, float4(normalDirectionMatcap,0) ).xyz;
                float2 transformMatcap_old = transformMatcapNormal.rg*0.5+0.5;
                transformMatcapNormal *= float3(-1,-1,1);
                float3 transformMatcapCombined = transformMatcapViewDir * dot(transformMatcapViewDir, transformMatcapNormal) / transformMatcapViewDir.z - transformMatcapNormal;
                transformMatcap = lerp(((transformMatcapCombined.rg*0.5)+0.5), transformMatcap_old, max(0,transformMatcapNormal.z));
            } else {
                transformMatcap = (mul( UNITY_MATRIX_V, float4(normalDirectionMatcap,0) ).xyz.rg*0.5+0.5);
            }
            float4 _MatcapTexture_var = UNITY_SAMPLE_TEX2D_SAMPLER(_MatcapTexture, REF_MAINTEX, TRANSFORM_TEX(transformMatcap, _MatcapTexture));
            float4 _MatcapBlendMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_MatcapBlendMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _MatcapBlendMask));
            matcap = ((_MatcapColor.rgb*_MatcapTexture_var.rgb)*_MatcapBlendMask_var.rgb*_MatcapBlend) * lerp(float3(1,1,1), finalLight,_MatcapShadeMix);
        }

        // オプション：Rim
        if (_UseRim) {
            float _RimBlendMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_RimBlendMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _RimBlendMask));
            float4 _RimTexture_var = UNITY_SAMPLE_TEX2D_SAMPLER(_RimTexture, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _RimTexture));
            RimLight = (
                            lerp( _RimTexture_var.rgb, Diffuse, _RimUseBaseTexture )
                            * pow(
                                min(1.0, 1.0 - max(0, dot(normalDirection * lerp(i.faceSign, 1, _DoubleSidedFlipBackfaceNormal), viewDirection) ) + _RimUpperSideWidth)
                                , _RimFresnelPower
                                )
                            * _RimBlend
                            * _RimColor.rgb
                            * _RimBlendMask_var
                            * lerp(float3(1,1,1), finalLight,_RimShadeMix)
                        );
        }

        // オプション:ShadeCap
        if (_ShadowCapBlendMode < 2) {
            float3 normalDirectionShadowCap = normalize(mul( float3(normalLocal.r*_ShadowCapNormalMix,normalLocal.g*_ShadowCapNormalMix,normalLocal.b), tangentTransform )); // Perturbed normals
            float2 transformShadowCap = float2(0,0);
            if (_UsePositionRelatedCalc) {
                float3 transformShadowCapViewDir = mul( UNITY_MATRIX_V, float4(viewDirection,0) ).xyz * float3(-1,-1,1) + float3(0,0,1);
                float3 transformShadowCapNormal = mul( UNITY_MATRIX_V, float4(normalDirectionShadowCap,0) ).xyz;
                float2 transformShadowCap_old = transformShadowCapNormal.rg*0.5+0.5;
                transformShadowCapNormal  *= float3(-1,-1,1);
                float3 transformShadowCapCombined = transformShadowCapViewDir * dot(transformShadowCapViewDir, transformShadowCapNormal) / transformShadowCapViewDir.z - transformShadowCapNormal;
                transformShadowCap = lerp(((transformShadowCapCombined.rg*0.5)+0.5), transformShadowCap_old, max(0,transformShadowCapNormal.z));
            } else {
                transformShadowCap = (mul( UNITY_MATRIX_V, float4(normalDirectionShadowCap,0) ).xyz.rg*0.5+0.5);
            }
            float4 _ShadowCapTexture_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowCapTexture, REF_MAINTEX, TRANSFORM_TEX(transformShadowCap, _ShadowCapTexture));
            float4 _ShadowCapBlendMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_ShadowCapBlendMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _ShadowCapBlendMask));
            shadowcap = (1.0 - ((1.0 - (_ShadowCapTexture_var.rgb))*_ShadowCapBlendMask_var.rgb)*_ShadowCapBlend);
        }
    #if !defined(ARKTOON_REFRACTED)
    }
    #endif

    float3 finalcolor2 = toonedMap+ReflectionMap + specular;

    // ShadeCapのブレンドモード
    if (_ShadowCapBlendMode == 0) { // Darken
        finalcolor2 = min(finalcolor2, shadowcap);
    } else if  (_ShadowCapBlendMode == 1) { // Multiply
        finalcolor2 = finalcolor2 * shadowcap;
    }

    // MatCapのブレンドモード
    if (_MatcapBlendMode == 0) { // Add
        finalcolor2 = finalcolor2 + matcap;
    } else if (_MatcapBlendMode == 1) { // Lighten
        finalcolor2 = max(finalcolor2, matcap);
    } else if (_MatcapBlendMode == 2) { // Screen
        finalcolor2 = 1-(1-finalcolor2) * (1-matcap);
    }

    // 屈折
    #ifdef ARKTOON_REFRACTED
        float refractionValue = pow(1.0-max(0,dot(normalDirection, viewDirection)),_RefractionFresnelExp);
        float2 sceneUVs = (i.projPos.xy / i.projPos.w) + ((refractionValue*_RefractionStrength) * mul( UNITY_MATRIX_V, float4(normalDirection,0) ).xyz.rgb.rg);
        float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
    #endif


    // Emission Parallax
    float3 emissionParallax = float3(0,0,0);
    if(_UseEmissionParallax) {
        float _EmissionParallaxDepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissionParallaxDepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissionParallaxDepthMask)).r;
        float2 emissionParallaxTransform = _EmissionParallaxDepth * (_EmissionParallaxDepthMask_var - _EmissionParallaxDepthMaskInvert) * mul(tangentTransform, viewDirection).xy + i.uv0;
        float _EmissionMask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissionParallaxMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissionParallaxMask)).r;
        float3 _EmissionParallaxTex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissionParallaxTex, REF_MAINTEX, TRANSFORM_TEX(emissionParallaxTransform, _EmissionParallaxTex)).rgb * _EmissionParallaxColor.rgb;
        emissionParallax = _EmissionParallaxTex_var * _EmissionMask_var;
    }

    #ifdef ARKTOON_EMISSIVE_FREAK
        float time = _Time.r;

        float2 emissiveFreak1uv = i.uv0 + float2(fmod(_EmissiveFreak1U * time, 1.0 / _EmissiveFreak1Tex_ST.x), fmod(time * _EmissiveFreak1V, 1.0 / _EmissiveFreak1Tex_ST.y));
        float _EmissiveFreak1DepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak1DepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak1DepthMask)).r;
        float2 emissiveFreak1Transform = _EmissiveFreak1Depth * (_EmissiveFreak1DepthMask_var - _EmissiveFreak1DepthMaskInvert) * mul(tangentTransform, viewDirection).xy + emissiveFreak1uv;
        float _EmissiveFreak1Mask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak1Mask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak1Mask)).r;
        float3 _EmissiveFreak1Tex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak1Tex, REF_MAINTEX, TRANSFORM_TEX(emissiveFreak1Transform, _EmissiveFreak1Tex)).rgb * _EmissiveFreak1Color.rgb;
        float emissiveFreak1Breathing = sin(90+_EmissiveFreak1Breathing*time) * 0.5 + 0.5;
        float emissiveFreak1BlinkOut = 1 - ((_EmissiveFreak1BlinkOut*time) % 1.0);
        float emissiveFreak1BlinkIn = (_EmissiveFreak1BlinkIn*time) % 1.0;
        float emissiveFreak1Hue = (_EmissiveFreak1HueShift*time) % 1.0;
        _EmissiveFreak1Tex_var = CalculateHSV(_EmissiveFreak1Tex_var, emissiveFreak1Hue, 1.0, 1.0);
        float3 emissiveFreak1 = _EmissiveFreak1Tex_var * _EmissiveFreak1Mask_var;
        emissiveFreak1 = lerp(emissiveFreak1, emissiveFreak1 * emissiveFreak1Breathing, _EmissiveFreak1BreathingMix);
        emissiveFreak1 = lerp(emissiveFreak1, emissiveFreak1 * emissiveFreak1BlinkOut, _EmissiveFreak1BlinkOutMix);
        emissiveFreak1 = lerp(emissiveFreak1, emissiveFreak1 * emissiveFreak1BlinkIn, _EmissiveFreak1BlinkInMix);

        float2 emissiveFreak2uv = i.uv0 + float2(fmod(_EmissiveFreak2U * time, 1.0 / _EmissiveFreak2Tex_ST.x), fmod(time * _EmissiveFreak2V, 1.0 / _EmissiveFreak2Tex_ST.y));
        float _EmissiveFreak2DepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak2DepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak2DepthMask)).r;
        float2 emissiveFreak2Transform = _EmissiveFreak2Depth * (_EmissiveFreak2DepthMask_var - _EmissiveFreak2DepthMaskInvert) * mul(tangentTransform, viewDirection).xy + emissiveFreak2uv;
        float _EmissiveFreak2Mask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak2Mask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak2Mask)).r;
        float3 _EmissiveFreak2Tex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak2Tex, REF_MAINTEX, TRANSFORM_TEX(emissiveFreak2Transform, _EmissiveFreak2Tex)).rgb * _EmissiveFreak2Color.rgb;
        float emissiveFreak2Breathing = sin(90+_EmissiveFreak2Breathing*time) * 0.5 + 0.5;
        float emissiveFreak2BlinkOut = 1 - ((_EmissiveFreak2BlinkOut*time) % 1.0);
        float emissiveFreak2BlinkIn = (_EmissiveFreak2BlinkIn*time) % 1.0;
        float emissiveFreak2Hue = (_EmissiveFreak2HueShift*time) % 1.0;
        _EmissiveFreak2Tex_var = CalculateHSV(_EmissiveFreak2Tex_var, emissiveFreak2Hue, 1.0, 1.0);
        float3 emissiveFreak2 = _EmissiveFreak2Tex_var * _EmissiveFreak2Mask_var;
        emissiveFreak2 = lerp(emissiveFreak2, emissiveFreak2 * emissiveFreak2Breathing, _EmissiveFreak2BreathingMix);
        emissiveFreak2 = lerp(emissiveFreak2, emissiveFreak2 * emissiveFreak2BlinkOut, _EmissiveFreak2BlinkOutMix);
        emissiveFreak2 = lerp(emissiveFreak2, emissiveFreak2 * emissiveFreak2BlinkIn, _EmissiveFreak2BlinkInMix);

        float2 emissiveFreak3uv = i.uv0 + float2(fmod(_EmissiveFreak3U * time, 1.0 / _EmissiveFreak3Tex_ST.x), fmod(time * _EmissiveFreak3V, 1.0 / _EmissiveFreak3Tex_ST.y));
        float _EmissiveFreak3DepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak3DepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak3DepthMask)).r;
        float2 emissiveFreak3Transform = _EmissiveFreak3Depth * (_EmissiveFreak3DepthMask_var - _EmissiveFreak3DepthMaskInvert) * mul(tangentTransform, viewDirection).xy + emissiveFreak3uv;
        float _EmissiveFreak3Mask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak3Mask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak3Mask)).r;
        float3 _EmissiveFreak3Tex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak3Tex, REF_MAINTEX, TRANSFORM_TEX(emissiveFreak3Transform, _EmissiveFreak3Tex)).rgb * _EmissiveFreak3Color.rgb;
        float emissiveFreak3Breathing = sin(90+_EmissiveFreak3Breathing*time) * 0.5 + 0.5;
        float emissiveFreak3BlinkOut = 1 - ((_EmissiveFreak3BlinkOut*time) % 1.0);
        float emissiveFreak3BlinkIn = (_EmissiveFreak3BlinkIn*time) % 1.0;
        float emissiveFreak3Hue = (_EmissiveFreak3HueShift*time) % 1.0;
        _EmissiveFreak3Tex_var = CalculateHSV(_EmissiveFreak3Tex_var, emissiveFreak3Hue, 1.0, 1.0);
        float3 emissiveFreak3 = _EmissiveFreak3Tex_var * _EmissiveFreak3Mask_var;
        emissiveFreak3 = lerp(emissiveFreak3, emissiveFreak3 * emissiveFreak3Breathing, _EmissiveFreak3BreathingMix);
        emissiveFreak3 = lerp(emissiveFreak3, emissiveFreak3 * emissiveFreak3BlinkOut, _EmissiveFreak3BlinkOutMix);
        emissiveFreak3 = lerp(emissiveFreak3, emissiveFreak3 * emissiveFreak3BlinkIn, _EmissiveFreak3BlinkInMix);

        float2 emissiveFreak4uv = i.uv0 + float2(fmod(_EmissiveFreak4U * time, 1.0 / _EmissiveFreak4Tex_ST.x), fmod(time * _EmissiveFreak4V, 1.0 / _EmissiveFreak4Tex_ST.y));
        float _EmissiveFreak4DepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak4DepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak4DepthMask)).r;
        float2 emissiveFreak4Transform = _EmissiveFreak4Depth * (_EmissiveFreak4DepthMask_var - _EmissiveFreak4DepthMaskInvert) * mul(tangentTransform, viewDirection).xy + emissiveFreak4uv;
        float _EmissiveFreak4Mask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak4Mask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak4Mask)).r;
        float3 _EmissiveFreak4Tex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak4Tex, REF_MAINTEX, TRANSFORM_TEX(emissiveFreak4Transform, _EmissiveFreak4Tex)).rgb * _EmissiveFreak4Color.rgb;
        float emissiveFreak4Breathing = sin(90+_EmissiveFreak4Breathing*time) * 0.5 + 0.5;
        float emissiveFreak4BlinkOut = 1 - ((_EmissiveFreak4BlinkOut*time) % 1.0);
        float emissiveFreak4BlinkIn = (_EmissiveFreak4BlinkIn*time) % 1.0;
        float emissiveFreak4Hue = (_EmissiveFreak4HueShift*time) % 1.0;
        _EmissiveFreak4Tex_var = CalculateHSV(_EmissiveFreak4Tex_var, emissiveFreak4Hue, 1.0, 1.0);
        float3 emissiveFreak4 = _EmissiveFreak4Tex_var * _EmissiveFreak4Mask_var;
        emissiveFreak4 = lerp(emissiveFreak4, emissiveFreak4 * emissiveFreak4Breathing, _EmissiveFreak4BreathingMix);
        emissiveFreak4 = lerp(emissiveFreak4, emissiveFreak4 * emissiveFreak4BlinkOut, _EmissiveFreak4BlinkOutMix);
        emissiveFreak4 = lerp(emissiveFreak4, emissiveFreak4 * emissiveFreak4BlinkIn, _EmissiveFreak4BlinkInMix);

        float2 emissiveFreak5uv = i.uv0 + float2(fmod(_EmissiveFreak5U * time, 1.0 / _EmissiveFreak5Tex_ST.x), fmod(time * _EmissiveFreak5V, 1.0 / _EmissiveFreak5Tex_ST.y));
        float _EmissiveFreak5DepthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak5DepthMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak5DepthMask)).r;
        float2 emissiveFreak5Transform = _EmissiveFreak5Depth * (_EmissiveFreak5DepthMask_var - _EmissiveFreak5DepthMaskInvert) * mul(tangentTransform, viewDirection).xy + emissiveFreak5uv;
        float _EmissiveFreak5Mask_var =  UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak5Mask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _EmissiveFreak5Mask)).r;
        float3 _EmissiveFreak5Tex_var = UNITY_SAMPLE_TEX2D_SAMPLER(_EmissiveFreak5Tex, REF_MAINTEX, TRANSFORM_TEX(emissiveFreak5Transform, _EmissiveFreak5Tex)).rgb * _EmissiveFreak5Color.rgb;
        float emissiveFreak5Breathing = sin(90+_EmissiveFreak5Breathing*time) * 0.5 + 0.5;
        float emissiveFreak5BlinkOut = 1 - ((_EmissiveFreak5BlinkOut*time) % 1.0);
        float emissiveFreak5BlinkIn = (_EmissiveFreak5BlinkIn*time) % 1.0;
        float emissiveFreak5Hue = (_EmissiveFreak5HueShift*time) % 1.0;
        _EmissiveFreak5Tex_var = CalculateHSV(_EmissiveFreak5Tex_var, emissiveFreak5Hue, 1.0, 1.0);
        float3 emissiveFreak5 = _EmissiveFreak5Tex_var * _EmissiveFreak5Mask_var;
        emissiveFreak5 = lerp(emissiveFreak5, emissiveFreak5 * emissiveFreak5Breathing, _EmissiveFreak5BreathingMix);
        emissiveFreak5 = lerp(emissiveFreak5, emissiveFreak5 * emissiveFreak5BlinkOut, _EmissiveFreak5BlinkOutMix);
        emissiveFreak5 = lerp(emissiveFreak5, emissiveFreak5 * emissiveFreak5BlinkIn, _EmissiveFreak5BlinkInMix);

    #else
        float3 emissiveFreak1 = float3(0,0,0);
        float3 emissiveFreak2 = float3(0,0,0);
        float3 emissiveFreak3 = float3(0,0,0);
        float3 emissiveFreak4 = float3(0,0,0);
        float3 emissiveFreak5 = float3(0,0,0);
    #endif

    // Emissive合成・FinalColor計算
    float3 _Emission = tex2D(REF_EMISSIONMAP,TRANSFORM_TEX(i.uv0, REF_EMISSIONMAP)).rgb *REF_EMISSIONCOLOR.rgb;
    _Emission = _Emission + emissionParallax + emissiveFreak1 + emissiveFreak2 + emissiveFreak3 + emissiveFreak4 + emissiveFreak5;
    float3 emissive = max( lerp(_Emission.rgb, _Emission.rgb * i.color, _VertexColorBlendEmissive) , RimLight) * !i.isOutline;
    float3 finalColor = emissive + finalcolor2;

    #ifdef ARKTOON_FADE
        fixed _AlphaMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_AlphaMask, REF_MAINTEX, TRANSFORM_TEX(i.uv0, _AlphaMask)).r;
        #ifdef ARKTOON_REFRACTED
            fixed4 finalRGBA = fixed4(lerp(sceneColor, finalColor, (_MainTex_var.a*REF_COLOR.a*_AlphaMask_var)),1);
        #else
            fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*REF_COLOR.a*_AlphaMask_var));
        #endif
    #else
        fixed4 finalRGBA = fixed4(finalColor,1);
    #endif
    UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
    return finalRGBA;
}