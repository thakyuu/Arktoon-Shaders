using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

// Arktoon-Shaders
//
// Copyright (c) 2018 synqark
//
// This code and repos（https://github.com/synqark/Arktoon-Shader) is under MIT licence, see LICENSE
//
// 本コードおよびリポジトリ（https://github.com/synqark/Arktoon-Shader) は MIT License を使用して公開しています。
// 詳細はLICENSEか、https://opensource.org/licenses/mit-license.php を参照してください。
namespace ArktoonShadersEFEX
{
    public class ArktoonInspectorEFEX : ShaderGUI
    {
        #region MaterialProperties
        MaterialProperty BaseTexture;
        MaterialProperty BaseColor;
        MaterialProperty Normalmap;
        MaterialProperty BumpScale;
        MaterialProperty EmissionMap;
        MaterialProperty EmissionColor;
        MaterialProperty AlphaMask;
        MaterialProperty BaseTextureSecondary;
        MaterialProperty BaseColorSecondary;
        MaterialProperty NormalmapSecondary;
        MaterialProperty BumpScaleSecondary;
        MaterialProperty EmissionMapSecondary;
        MaterialProperty EmissionColorSecondary;
        MaterialProperty UseEmissionParallax;
        MaterialProperty EmissionParallaxColor;
        MaterialProperty EmissionParallaxTex;
        MaterialProperty EmissionParallaxMask;
        MaterialProperty EmissionParallaxDepth;
        MaterialProperty EmissionParallaxDepthMask;
        MaterialProperty EmissionParallaxDepthMaskInvert;
        MaterialProperty Shadowborder;
        MaterialProperty ShadowborderBlur;
        MaterialProperty ShadowborderBlurMask;
        MaterialProperty ShadowStrength;
        MaterialProperty ShadowStrengthMask;
        MaterialProperty ShadowIndirectIntensity;
        MaterialProperty ShadowUseStep;
        MaterialProperty ShadowSteps;
        MaterialProperty PointAddIntensity;
        MaterialProperty PointShadowStrength;
        MaterialProperty PointShadowborder;
        MaterialProperty PointShadowborderBlur;
        MaterialProperty PointShadowborderBlurMask;
        MaterialProperty PointShadowUseStep;
        MaterialProperty PointShadowSteps;
        MaterialProperty CutoutCutoutAdjust;
        MaterialProperty ShadowPlanBUsePlanB;
        MaterialProperty ShadowPlanBDefaultShadowMix;
        MaterialProperty ShadowPlanBUseCustomShadowTexture;
        MaterialProperty ShadowPlanBHueShiftFromBase;
        MaterialProperty ShadowPlanBSaturationFromBase;
        MaterialProperty ShadowPlanBValueFromBase;
        MaterialProperty ShadowPlanBCustomShadowTexture;
        MaterialProperty ShadowPlanBCustomShadowTextureRGB;
        MaterialProperty CustomShadow2nd;
        MaterialProperty ShadowPlanB2border;
        MaterialProperty ShadowPlanB2borderBlur;
        MaterialProperty ShadowPlanB2HueShiftFromBase;
        MaterialProperty ShadowPlanB2SaturationFromBase;
        MaterialProperty ShadowPlanB2ValueFromBase;
        MaterialProperty ShadowPlanB2UseCustomShadowTexture;
        MaterialProperty ShadowPlanB2CustomShadowTexture;
        MaterialProperty ShadowPlanB2CustomShadowTextureRGB;
        MaterialProperty UseGloss;
        MaterialProperty GlossBlend;
        MaterialProperty GlossBlendMask;
        MaterialProperty GlossPower;
        MaterialProperty GlossColor;
        MaterialProperty UseOutline;
        MaterialProperty OutlineWidth;
        MaterialProperty OutlineMask;
        MaterialProperty OutlineCutoffRange;
        MaterialProperty OutlineColor;
        MaterialProperty OutlineTexture;
        MaterialProperty OutlineShadeMix;
        MaterialProperty OutlineTextureColorRate;
        MaterialProperty OutlineWidthMask;
        MaterialProperty OutlineUseColorShift;
        MaterialProperty OutlineHueShiftFromBase;
        MaterialProperty OutlineSaturationFromBase;
        MaterialProperty OutlineValueFromBase;
        MaterialProperty MatcapBlendMode;
        MaterialProperty MatcapBlend;
        MaterialProperty MatcapTexture;
        MaterialProperty MatcapColor;
        MaterialProperty MatcapBlendMask;
        MaterialProperty MatcapNormalMix;
        MaterialProperty MatcapShadeMix;
        MaterialProperty UseReflection;
        MaterialProperty UseReflectionProbe;
        MaterialProperty ReflectionReflectionPower;
        MaterialProperty ReflectionReflectionMask;
        MaterialProperty ReflectionNormalMix;
        MaterialProperty ReflectionShadeMix;
        MaterialProperty ReflectionCubemap;
        MaterialProperty ReflectionSuppressBaseColorValue;
        MaterialProperty RefractionFresnelExp;
        MaterialProperty RefractionStrength;
        MaterialProperty UseRim;
        MaterialProperty RimBlend;
        MaterialProperty RimBlendMask;
        MaterialProperty RimShadeMix;
        MaterialProperty RimFresnelPower;
        MaterialProperty RimUpperSideWidth;
        MaterialProperty RimColor;
        MaterialProperty RimTexture;
        MaterialProperty RimUseBaseTexture;
        MaterialProperty ShadowCapBlendMode;
        MaterialProperty ShadowCapBlend;
        MaterialProperty ShadowCapBlendMask;
        MaterialProperty ShadowCapNormalMix;
        MaterialProperty ShadowCapTexture;
        MaterialProperty StencilNumber;
        MaterialProperty StencilCompareAction;
        MaterialProperty StencilNumberSecondary;
        MaterialProperty StencilCompareActionSecondary;
        MaterialProperty StencilMaskTex;
        MaterialProperty StencilMaskAdjust;
        MaterialProperty StencilMaskAlphaDither;
        MaterialProperty UseDoubleSided;
        MaterialProperty DoubleSidedFlipBackfaceNormal;
        MaterialProperty DoubleSidedBackfaceLightIntensity;
        MaterialProperty DoubleSidedBackfaceUseColorShift;
        MaterialProperty DoubleSidedBackfaceHueShiftFromBase;
        MaterialProperty DoubleSidedBackfaceSaturationFromBase;
        MaterialProperty DoubleSidedBackfaceValueFromBase;
        MaterialProperty ShadowCasterCulling;
        MaterialProperty ZWrite;
        MaterialProperty VertexColorBlendDiffuse;
        MaterialProperty VertexColorBlendEmissive;
        MaterialProperty OtherShadowBorderSharpness;
        MaterialProperty OtherShadowAdjust;
        MaterialProperty UseVertexLight;
        MaterialProperty BackfaceColorMultiply;
        MaterialProperty LightSampling;
        MaterialProperty UsePositionRelatedCalc;
        MaterialProperty EmissiveFreak1Tex;
        MaterialProperty EmissiveFreak1Mask;
        MaterialProperty EmissiveFreak1Color;
        MaterialProperty EmissiveFreak1Color2;
        MaterialProperty EmissiveFreak1ColorChangeSpeed;
        MaterialProperty EmissiveFreak1U;
        MaterialProperty EmissiveFreak1V;
        MaterialProperty EmissiveFreak1Depth;
        MaterialProperty EmissiveFreak1DepthMask;
        MaterialProperty EmissiveFreak1DepthMaskInvert;
        MaterialProperty EmissiveFreak1Breathing;
        MaterialProperty EmissiveFreak1BreathingMix;
        MaterialProperty EmissiveFreak1BlinkOut;
        MaterialProperty EmissiveFreak1BlinkOutMix;
        MaterialProperty EmissiveFreak1BlinkIn;
        MaterialProperty EmissiveFreak1BlinkInMix;
        MaterialProperty EmissiveFreak1HueShift;
        MaterialProperty EmissiveFreak2Tex;
        MaterialProperty EmissiveFreak2Mask;
        MaterialProperty EmissiveFreak2Color;
        MaterialProperty EmissiveFreak2Color2;
        MaterialProperty EmissiveFreak2ColorChangeSpeed;
        MaterialProperty EmissiveFreak2U;
        MaterialProperty EmissiveFreak2V;
        MaterialProperty EmissiveFreak2Depth;
        MaterialProperty EmissiveFreak2DepthMask;
        MaterialProperty EmissiveFreak2DepthMaskInvert;
        MaterialProperty EmissiveFreak2Breathing;
        MaterialProperty EmissiveFreak2BreathingMix;
        MaterialProperty EmissiveFreak2BlinkOut;
        MaterialProperty EmissiveFreak2BlinkOutMix;
        MaterialProperty EmissiveFreak2BlinkIn;
        MaterialProperty EmissiveFreak2BlinkInMix;
        MaterialProperty EmissiveFreak2HueShift;
        MaterialProperty EmissiveFreak3Tex;
        MaterialProperty EmissiveFreak3Mask;
        MaterialProperty EmissiveFreak3Color;
        MaterialProperty EmissiveFreak3Color2;
        MaterialProperty EmissiveFreak3ColorChangeSpeed;
        MaterialProperty EmissiveFreak3U;
        MaterialProperty EmissiveFreak3V;
        MaterialProperty EmissiveFreak3Depth;
        MaterialProperty EmissiveFreak3DepthMask;
        MaterialProperty EmissiveFreak3DepthMaskInvert;
        MaterialProperty EmissiveFreak3Breathing;
        MaterialProperty EmissiveFreak3BreathingMix;
        MaterialProperty EmissiveFreak3BlinkOut;
        MaterialProperty EmissiveFreak3BlinkOutMix;
        MaterialProperty EmissiveFreak3BlinkIn;
        MaterialProperty EmissiveFreak3BlinkInMix;
        MaterialProperty EmissiveFreak3HueShift;
        MaterialProperty EmissiveFreak4Tex;
        MaterialProperty EmissiveFreak4Mask;
        MaterialProperty EmissiveFreak4Color;
        MaterialProperty EmissiveFreak4Color2;
        MaterialProperty EmissiveFreak4ColorChangeSpeed;
        MaterialProperty EmissiveFreak4U;
        MaterialProperty EmissiveFreak4V;
        MaterialProperty EmissiveFreak4Depth;
        MaterialProperty EmissiveFreak4DepthMask;
        MaterialProperty EmissiveFreak4DepthMaskInvert;
        MaterialProperty EmissiveFreak4Breathing;
        MaterialProperty EmissiveFreak4BreathingMix;
        MaterialProperty EmissiveFreak4BlinkOut;
        MaterialProperty EmissiveFreak4BlinkOutMix;
        MaterialProperty EmissiveFreak4BlinkIn;
        MaterialProperty EmissiveFreak4BlinkInMix;
        MaterialProperty EmissiveFreak4HueShift;
        MaterialProperty EmissiveFreak5Tex;
        MaterialProperty EmissiveFreak5Mask;
        MaterialProperty EmissiveFreak5Color;
        MaterialProperty EmissiveFreak5Color2;
        MaterialProperty EmissiveFreak5ColorChangeSpeed;
        MaterialProperty EmissiveFreak5U;
        MaterialProperty EmissiveFreak5V;
        MaterialProperty EmissiveFreak5Depth;
        MaterialProperty EmissiveFreak5DepthMask;
        MaterialProperty EmissiveFreak5DepthMaskInvert;
        MaterialProperty EmissiveFreak5Breathing;
        MaterialProperty EmissiveFreak5BreathingMix;
        MaterialProperty EmissiveFreak5BlinkOut;
        MaterialProperty EmissiveFreak5BlinkOutMix;
        MaterialProperty EmissiveFreak5BlinkIn;
        MaterialProperty EmissiveFreak5BlinkInMix;
        MaterialProperty EmissiveFreak5HueShift;

        // TODO: そろそろShaderUtil.GetPropertiesで一括処理したい。
        // ただ、その場合は、カスタムインスペクタで定義していない追加のプロパティを、このファイルを弄らずに動的に表示できるようにしてあげたい（改変の負荷軽減のため）

        #endregion

        static bool IsShowAdvanced = false;
        static bool IsShowAlphaMask = false;
        GUIStyle style = new GUIStyle();


        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            Material material = materialEditor.target as Material;

            // バージョンを逐一確認して、アセットバージョンが高い場合はマテリアルをマイグレーション
            //if( material.GetInt("_Version") < ArktoonManager.AssetVersionInt) {
            //    ArktoonMigrator.MigrateArktoonMaterial(material);
            //}

            Shader shader = material.shader;

            // shader.nameによって調整可能なプロパティを制御する。
            bool isOpaque = shader.name.Contains("Opaque");
            bool isFade = shader.name.Contains("Fade");
            bool isCutout = shader.name.Contains("Cutout");
            bool isStencilWriter = shader.name.Contains("Stencil/Writer") || shader.name.Contains("StencilWriter");
            bool isStencilReader = shader.name.Contains("Stencil/Reader") || shader.name.Contains("StencilReader");
            bool isStencilReaderDouble = shader.name.Contains("Stencil/Reader/Double");
            bool isStencilWriterMask = shader.name.Contains("Stencil/WriterMask");
            bool isRefracted = shader.name.Contains("Refracted");
            bool isEmissiveFreak = shader.name.Contains("/EmissiveFreakExtend/");

            // FindProperties
            BaseTexture = FindProperty("_MainTex", props, false);
            BaseColor = FindProperty("_Color", props, false);
            Normalmap = FindProperty("_BumpMap", props, false);
            BumpScale = FindProperty("_BumpScale", props, false);
            EmissionMap = FindProperty("_EmissionMap", props, false);
            EmissionColor = FindProperty("_EmissionColor", props, false);
            AlphaMask = FindProperty("_AlphaMask", props, false);
            BaseTextureSecondary = FindProperty("_MainTexSecondary", props, false);
            BaseColorSecondary = FindProperty("_ColorSecondary", props, false);
            NormalmapSecondary = FindProperty("_BumpMapSecondary", props, false);
            BumpScaleSecondary = FindProperty("_BumpScaleSecondary", props, false);
            EmissionMapSecondary = FindProperty("_EmissionMapSecondary", props, false);
            EmissionColorSecondary = FindProperty("_EmissionColorSecondary", props, false);
            UseEmissionParallax = FindProperty("_UseEmissionParallax", props, false);
            EmissionParallaxColor = FindProperty("_EmissionParallaxColor", props, false);
            EmissionParallaxTex = FindProperty("_EmissionParallaxTex", props, false);
            EmissionParallaxMask = FindProperty("_EmissionParallaxMask", props, false);
            EmissionParallaxDepth = FindProperty("_EmissionParallaxDepth", props, false);
            EmissionParallaxDepthMask = FindProperty("_EmissionParallaxDepthMask", props, false);
            EmissionParallaxDepthMaskInvert = FindProperty("_EmissionParallaxDepthMaskInvert", props, false);
            CutoutCutoutAdjust = FindProperty("_CutoutCutoutAdjust", props, false);
            Shadowborder = FindProperty("_Shadowborder", props, false);
            ShadowborderBlur = FindProperty("_ShadowborderBlur", props, false);
            ShadowborderBlurMask = FindProperty("_ShadowborderBlurMask", props, false);
            ShadowStrength = FindProperty("_ShadowStrength", props, false);
            ShadowStrengthMask = FindProperty("_ShadowStrengthMask", props, false);
            ShadowIndirectIntensity = FindProperty("_ShadowIndirectIntensity", props, false);
            ShadowUseStep = FindProperty("_ShadowUseStep", props, false);
            ShadowSteps = FindProperty("_ShadowSteps", props, false);
            PointAddIntensity = FindProperty("_PointAddIntensity", props, false);
            PointShadowStrength = FindProperty("_PointShadowStrength", props, false);
            PointShadowborder = FindProperty("_PointShadowborder", props, false);
            PointShadowborderBlur = FindProperty("_PointShadowborderBlur", props, false);
            PointShadowborderBlurMask= FindProperty("_PointShadowborderBlurMask", props, false);
            PointShadowUseStep = FindProperty("_PointShadowUseStep", props, false);
            PointShadowSteps = FindProperty("_PointShadowSteps", props, false);
            ShadowPlanBUsePlanB = FindProperty("_ShadowPlanBUsePlanB", props, false);
            ShadowPlanBDefaultShadowMix = FindProperty("_ShadowPlanBDefaultShadowMix", props, false);
            ShadowPlanBUseCustomShadowTexture = FindProperty("_ShadowPlanBUseCustomShadowTexture", props, false);
            ShadowPlanBHueShiftFromBase = FindProperty("_ShadowPlanBHueShiftFromBase", props, false);
            ShadowPlanBSaturationFromBase = FindProperty("_ShadowPlanBSaturationFromBase", props, false);
            ShadowPlanBValueFromBase = FindProperty("_ShadowPlanBValueFromBase", props, false);
            ShadowPlanBCustomShadowTexture = FindProperty("_ShadowPlanBCustomShadowTexture", props, false);
            ShadowPlanBCustomShadowTextureRGB = FindProperty("_ShadowPlanBCustomShadowTextureRGB", props, false);
            CustomShadow2nd = FindProperty("_CustomShadow2nd", props, false);
            ShadowPlanB2border = FindProperty("_ShadowPlanB2border", props, false);
            ShadowPlanB2borderBlur = FindProperty("_ShadowPlanB2borderBlur", props, false);
            ShadowPlanB2HueShiftFromBase = FindProperty("_ShadowPlanB2HueShiftFromBase", props, false);
            ShadowPlanB2SaturationFromBase = FindProperty("_ShadowPlanB2SaturationFromBase", props, false);
            ShadowPlanB2ValueFromBase = FindProperty("_ShadowPlanB2ValueFromBase", props, false);
            ShadowPlanB2UseCustomShadowTexture = FindProperty("_ShadowPlanB2UseCustomShadowTexture", props, false);
            ShadowPlanB2CustomShadowTexture = FindProperty("_ShadowPlanB2CustomShadowTexture", props, false);
            ShadowPlanB2CustomShadowTextureRGB = FindProperty("_ShadowPlanB2CustomShadowTextureRGB", props, false);
            UseGloss = FindProperty("_UseGloss", props, false);
            GlossBlend = FindProperty("_GlossBlend", props, false);
            GlossBlendMask = FindProperty("_GlossBlendMask", props, false);
            GlossPower = FindProperty("_GlossPower", props, false);
            GlossColor = FindProperty("_GlossColor", props, false);
            UseOutline = FindProperty("_UseOutline", props, false);
            OutlineWidth = FindProperty("_OutlineWidth", props, false);
            OutlineMask = FindProperty("_OutlineMask", props, false);
            OutlineCutoffRange = FindProperty("_OutlineCutoffRange", props, false);
            OutlineColor = FindProperty("_OutlineColor", props, false);
            OutlineTexture = FindProperty("_OutlineTexture", props, false);
            OutlineShadeMix = FindProperty("_OutlineShadeMix", props, false);
            OutlineTextureColorRate = FindProperty("_OutlineTextureColorRate", props, false);
            OutlineWidthMask = FindProperty("_OutlineWidthMask", props, false);
            OutlineUseColorShift = FindProperty("_OutlineUseColorShift", props, false);
            OutlineHueShiftFromBase = FindProperty("_OutlineHueShiftFromBase", props, false);
            OutlineSaturationFromBase = FindProperty("_OutlineSaturationFromBase", props, false);
            OutlineValueFromBase = FindProperty("_OutlineValueFromBase", props, false);
            MatcapBlendMode = FindProperty("_MatcapBlendMode", props, false);
            MatcapBlend = FindProperty("_MatcapBlend", props, false);
            MatcapTexture = FindProperty("_MatcapTexture", props, false);
            MatcapColor = FindProperty("_MatcapColor", props, false);
            MatcapBlendMask = FindProperty("_MatcapBlendMask", props, false);
            MatcapNormalMix = FindProperty("_MatcapNormalMix", props, false);
            MatcapShadeMix = FindProperty("_MatcapShadeMix", props, false);
            UseReflection = FindProperty("_UseReflection", props, false);
            UseReflectionProbe = FindProperty("_UseReflectionProbe", props, false);
            ReflectionReflectionPower = FindProperty("_ReflectionReflectionPower", props, false);
            ReflectionReflectionMask = FindProperty("_ReflectionReflectionMask", props, false);
            ReflectionNormalMix = FindProperty("_ReflectionNormalMix", props, false);
            ReflectionShadeMix = FindProperty("_ReflectionShadeMix", props, false);
            ReflectionCubemap = FindProperty("_ReflectionCubemap", props, false);
            ReflectionSuppressBaseColorValue = FindProperty("_ReflectionSuppressBaseColorValue", props, false);
            RefractionFresnelExp = FindProperty("_RefractionFresnelExp", props, false);
            RefractionStrength = FindProperty("_RefractionStrength", props, false);
            UseRim = FindProperty("_UseRim", props, false);
            RimBlend = FindProperty("_RimBlend", props, false);
            RimBlendMask = FindProperty("_RimBlendMask", props, false);
            RimShadeMix = FindProperty("_RimShadeMix", props, false);
            RimFresnelPower = FindProperty("_RimFresnelPower", props, false);
            RimUpperSideWidth = FindProperty("_RimUpperSideWidth", props, false);
            RimColor = FindProperty("_RimColor", props, false);
            RimTexture = FindProperty("_RimTexture", props, false);
            RimUseBaseTexture = FindProperty("_RimUseBaseTexture", props, false);
            ShadowCapBlendMode = FindProperty("_ShadowCapBlendMode", props, false);
            ShadowCapBlend = FindProperty("_ShadowCapBlend", props, false);
            ShadowCapBlendMask = FindProperty("_ShadowCapBlendMask", props, false);
            ShadowCapNormalMix = FindProperty("_ShadowCapNormalMix", props, false);
            ShadowCapTexture = FindProperty("_ShadowCapTexture", props, false);
            StencilNumber = FindProperty("_StencilNumber", props, false);
            StencilMaskTex = FindProperty("_StencilMaskTex", props, false);
            StencilMaskAdjust = FindProperty("_StencilMaskAdjust", props, false);
            StencilMaskAlphaDither = FindProperty("_StencilMaskAlphaDither", props, false);
            StencilCompareAction = FindProperty("_StencilCompareAction", props, false);
            StencilNumberSecondary = FindProperty("_StencilNumberSecondary", props, false);
            StencilCompareActionSecondary = FindProperty("_StencilCompareActionSecondary", props, false);
            UseDoubleSided = FindProperty("_UseDoubleSided", props, false);
            DoubleSidedFlipBackfaceNormal = FindProperty("_DoubleSidedFlipBackfaceNormal", props, false);
            DoubleSidedBackfaceLightIntensity = FindProperty("_DoubleSidedBackfaceLightIntensity", props, false);
            DoubleSidedBackfaceUseColorShift = FindProperty("_DoubleSidedBackfaceUseColorShift", props, false);
            DoubleSidedBackfaceHueShiftFromBase = FindProperty("_DoubleSidedBackfaceHueShiftFromBase", props, false);
            DoubleSidedBackfaceSaturationFromBase = FindProperty("_DoubleSidedBackfaceSaturationFromBase", props, false);
            DoubleSidedBackfaceValueFromBase = FindProperty("_DoubleSidedBackfaceValueFromBase", props, false);
            ShadowCasterCulling = FindProperty("_ShadowCasterCulling", props, false);
            VertexColorBlendDiffuse = FindProperty("_VertexColorBlendDiffuse", props, false);
            VertexColorBlendEmissive = FindProperty("_VertexColorBlendEmissive", props, false);
            OtherShadowBorderSharpness = FindProperty("_OtherShadowBorderSharpness", props, false);
            OtherShadowAdjust = FindProperty("_OtherShadowAdjust", props, false);
            UseVertexLight = FindProperty("_UseVertexLight", props, false);
            LightSampling = FindProperty("_LightSampling", props, false);
            UsePositionRelatedCalc = FindProperty("_UsePositionRelatedCalc", props, false);
            ZWrite = FindProperty("_ZWrite", props, false);

            EmissiveFreak1Tex = FindProperty("_EmissiveFreak1Tex", props, false);
            EmissiveFreak1Mask = FindProperty("_EmissiveFreak1Mask", props, false);
            EmissiveFreak1Color = FindProperty("_EmissiveFreak1Color", props, false);
            EmissiveFreak1Color2 = FindProperty("_EmissiveFreak1Color2", props, false);
            EmissiveFreak1ColorChangeSpeed = FindProperty("_EmissiveFreak1ColorChangeSpeed", props, false);
            EmissiveFreak1U = FindProperty("_EmissiveFreak1U", props, false);
            EmissiveFreak1V = FindProperty("_EmissiveFreak1V", props, false);
            EmissiveFreak1Depth = FindProperty("_EmissiveFreak1Depth", props, false);
            EmissiveFreak1DepthMask = FindProperty("_EmissiveFreak1DepthMask", props, false);
            EmissiveFreak1DepthMaskInvert = FindProperty("_EmissiveFreak1DepthMaskInvert", props, false);
            EmissiveFreak1Breathing = FindProperty("_EmissiveFreak1Breathing", props, false);
            EmissiveFreak1BreathingMix = FindProperty("_EmissiveFreak1BreathingMix", props, false);
            EmissiveFreak1BlinkOut = FindProperty("_EmissiveFreak1BlinkOut", props, false);
            EmissiveFreak1BlinkOutMix = FindProperty("_EmissiveFreak1BlinkOutMix", props, false);
            EmissiveFreak1BlinkIn = FindProperty("_EmissiveFreak1BlinkIn", props, false);
            EmissiveFreak1BlinkInMix = FindProperty("_EmissiveFreak1BlinkInMix", props, false);
            EmissiveFreak1HueShift = FindProperty("_EmissiveFreak1HueShift", props, false);

            EmissiveFreak2Tex = FindProperty("_EmissiveFreak2Tex", props, false);
            EmissiveFreak2Mask = FindProperty("_EmissiveFreak2Mask", props, false);
            EmissiveFreak2Color = FindProperty("_EmissiveFreak2Color", props, false);
            EmissiveFreak2Color2 = FindProperty("_EmissiveFreak2Color2", props, false);
            EmissiveFreak2ColorChangeSpeed = FindProperty("_EmissiveFreak2ColorChangeSpeed", props, false);
            EmissiveFreak2U = FindProperty("_EmissiveFreak2U", props, false);
            EmissiveFreak2V = FindProperty("_EmissiveFreak2V", props, false);
            EmissiveFreak2Depth = FindProperty("_EmissiveFreak2Depth", props, false);
            EmissiveFreak2DepthMask = FindProperty("_EmissiveFreak2DepthMask", props, false);
            EmissiveFreak2DepthMaskInvert = FindProperty("_EmissiveFreak2DepthMaskInvert", props, false);
            EmissiveFreak2Breathing = FindProperty("_EmissiveFreak2Breathing", props, false);
            EmissiveFreak2BreathingMix = FindProperty("_EmissiveFreak2BreathingMix", props, false);
            EmissiveFreak2BlinkOut = FindProperty("_EmissiveFreak2BlinkOut", props, false);
            EmissiveFreak2BlinkOutMix = FindProperty("_EmissiveFreak2BlinkOutMix", props, false);
            EmissiveFreak2BlinkIn = FindProperty("_EmissiveFreak2BlinkIn", props, false);
            EmissiveFreak2BlinkInMix = FindProperty("_EmissiveFreak2BlinkInMix", props, false);
            EmissiveFreak2HueShift = FindProperty("_EmissiveFreak2HueShift", props, false);

            EmissiveFreak3Tex = FindProperty("_EmissiveFreak3Tex", props, false);
            EmissiveFreak3Mask = FindProperty("_EmissiveFreak3Mask", props, false);
            EmissiveFreak3Color = FindProperty("_EmissiveFreak3Color", props, false);
            EmissiveFreak3Color2 = FindProperty("_EmissiveFreak3Color2", props, false);
            EmissiveFreak3ColorChangeSpeed = FindProperty("_EmissiveFreak3ColorChangeSpeed", props, false);
            EmissiveFreak3U = FindProperty("_EmissiveFreak3U", props, false);
            EmissiveFreak3V = FindProperty("_EmissiveFreak3V", props, false);
            EmissiveFreak3Depth = FindProperty("_EmissiveFreak3Depth", props, false);
            EmissiveFreak3DepthMask = FindProperty("_EmissiveFreak3DepthMask", props, false);
            EmissiveFreak3DepthMaskInvert = FindProperty("_EmissiveFreak3DepthMaskInvert", props, false);
            EmissiveFreak3Breathing = FindProperty("_EmissiveFreak3Breathing", props, false);
            EmissiveFreak3BreathingMix = FindProperty("_EmissiveFreak3BreathingMix", props, false);
            EmissiveFreak3BlinkOut = FindProperty("_EmissiveFreak3BlinkOut", props, false);
            EmissiveFreak3BlinkOutMix = FindProperty("_EmissiveFreak3BlinkOutMix", props, false);
            EmissiveFreak3BlinkIn = FindProperty("_EmissiveFreak3BlinkIn", props, false);
            EmissiveFreak3BlinkInMix = FindProperty("_EmissiveFreak3BlinkInMix", props, false);
            EmissiveFreak3HueShift = FindProperty("_EmissiveFreak3HueShift", props, false);

            EmissiveFreak4Tex = FindProperty("_EmissiveFreak4Tex", props, false);
            EmissiveFreak4Mask = FindProperty("_EmissiveFreak4Mask", props, false);
            EmissiveFreak4Color = FindProperty("_EmissiveFreak4Color", props, false);
            EmissiveFreak4Color2 = FindProperty("_EmissiveFreak4Color2", props, false);
            EmissiveFreak4ColorChangeSpeed = FindProperty("_EmissiveFreak4ColorChangeSpeed", props, false);
            EmissiveFreak4U = FindProperty("_EmissiveFreak4U", props, false);
            EmissiveFreak4V = FindProperty("_EmissiveFreak4V", props, false);
            EmissiveFreak4Depth = FindProperty("_EmissiveFreak4Depth", props, false);
            EmissiveFreak4DepthMask = FindProperty("_EmissiveFreak4DepthMask", props, false);
            EmissiveFreak4DepthMaskInvert = FindProperty("_EmissiveFreak4DepthMaskInvert", props, false);
            EmissiveFreak4Breathing = FindProperty("_EmissiveFreak4Breathing", props, false);
            EmissiveFreak4BreathingMix = FindProperty("_EmissiveFreak4BreathingMix", props, false);
            EmissiveFreak4BlinkOut = FindProperty("_EmissiveFreak4BlinkOut", props, false);
            EmissiveFreak4BlinkOutMix = FindProperty("_EmissiveFreak4BlinkOutMix", props, false);
            EmissiveFreak4BlinkIn = FindProperty("_EmissiveFreak4BlinkIn", props, false);
            EmissiveFreak4BlinkInMix = FindProperty("_EmissiveFreak4BlinkInMix", props, false);
            EmissiveFreak4HueShift = FindProperty("_EmissiveFreak4HueShift", props, false);

            EmissiveFreak5Tex = FindProperty("_EmissiveFreak5Tex", props, false);
            EmissiveFreak5Mask = FindProperty("_EmissiveFreak5Mask", props, false);
            EmissiveFreak5Color = FindProperty("_EmissiveFreak5Color", props, false);
            EmissiveFreak5Color2 = FindProperty("_EmissiveFreak5Color2", props, false);
            EmissiveFreak5ColorChangeSpeed = FindProperty("_EmissiveFreak5ColorChangeSpeed", props, false);
            EmissiveFreak5U = FindProperty("_EmissiveFreak5U", props, false);
            EmissiveFreak5V = FindProperty("_EmissiveFreak5V", props, false);
            EmissiveFreak5Depth = FindProperty("_EmissiveFreak5Depth", props, false);
            EmissiveFreak5DepthMask = FindProperty("_EmissiveFreak5DepthMask", props, false);
            EmissiveFreak5DepthMaskInvert = FindProperty("_EmissiveFreak5DepthMaskInvert", props, false);
            EmissiveFreak5Breathing = FindProperty("_EmissiveFreak5Breathing", props, false);
            EmissiveFreak5BreathingMix = FindProperty("_EmissiveFreak5BreathingMix", props, false);
            EmissiveFreak5BlinkOut = FindProperty("_EmissiveFreak5BlinkOut", props, false);
            EmissiveFreak5BlinkOutMix = FindProperty("_EmissiveFreak5BlinkOutMix", props, false);
            EmissiveFreak5BlinkIn = FindProperty("_EmissiveFreak5BlinkIn", props, false);
            EmissiveFreak5BlinkInMix = FindProperty("_EmissiveFreak5BlinkInMix", props, false);
            EmissiveFreak5HueShift = FindProperty("_EmissiveFreak5HueShift", props, false);

            EditorGUIUtility.labelWidth = 0f;

            EditorGUI.BeginChangeCheck();
            {
                // Common
                UIHelper.ShurikenHeader("Common");
                UIHelper.DrawWithGroup(() => {
					UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture", "Base Color Texture (RGB)"), BaseTexture, BaseColor);
                        materialEditor.TextureScaleOffsetPropertyIndent(BaseTexture);
					});
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal Map", "Normal Map (RGB)"), Normalmap, BumpScale);
                        materialEditor.TextureScaleOffsetPropertyIndent(Normalmap);
                    });
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Emission", "Emission (RGB)"), EmissionMap, EmissionColor);
                        materialEditor.TextureScaleOffsetPropertyIndent(EmissionMap);
                    });

                    // materialEditor.ShaderProperty(Cull, "Cull");
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(UseDoubleSided, "Is Double Sided");
                        var doublesided = UseDoubleSided.floatValue;
                        if(doublesided > 0){
                            ShadowCasterCulling.floatValue = 0;
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(DoubleSidedFlipBackfaceNormal, "Flip backface normal");
                            materialEditor.ShaderProperty(DoubleSidedBackfaceLightIntensity, "Backface Light Intensity");
                            materialEditor.ShaderProperty(DoubleSidedBackfaceUseColorShift, "Use Backface Color Shift");
                            var backfaceColorShift = DoubleSidedBackfaceUseColorShift.floatValue;
                            if(backfaceColorShift > 0) {
                                EditorGUI.indentLevel ++;
                                materialEditor.ShaderProperty(DoubleSidedBackfaceHueShiftFromBase, "Hue Shift");
                                materialEditor.ShaderProperty(DoubleSidedBackfaceSaturationFromBase, "Saturation");
                                materialEditor.ShaderProperty(DoubleSidedBackfaceValueFromBase, "Value");
                                EditorGUI.indentLevel --;
                            }

                            EditorGUI.indentLevel --;
                        } else {
                            ShadowCasterCulling.floatValue = 2;
                        }
                        if(isFade) materialEditor.ShaderProperty(ZWrite, "ZWrite");
                    });
                });

                // Secondary Common
                if(isStencilReaderDouble) {
                    UIHelper.ShurikenHeader("Secondary Common");
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Main Texture", "Base Color Texture (RGB)"), BaseTextureSecondary, BaseColorSecondary);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal Map", "Normal Map (RGB)"), NormalmapSecondary, BumpScaleSecondary);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Emission", "Emission (RGB)"), EmissionMapSecondary, EmissionColorSecondary);
                    });
                }

                // AlphaMask
                if(isFade){
                    IsShowAlphaMask = UIHelper.ShurikenFoldout("AlphaMask", IsShowAlphaMask);
                    if (IsShowAlphaMask) {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.ShaderProperty(AlphaMask, "Alpha Mask");
                        });
                    }
                }

                // Refraction
                if(isRefracted){
                    UIHelper.ShurikenHeader("Refraction");
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(RefractionFresnelExp, "Fresnel Exp");
                        materialEditor.ShaderProperty(RefractionStrength, "Strength");
                    });
                }

                // Alpha Cutout
                if(isCutout){
                    UIHelper.ShurikenHeader("Alpha Cutout");
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(CutoutCutoutAdjust, "Cutoff Adjust");
                    });
                }

                // Shadow
                UIHelper.ShurikenHeader("Shadow");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.ShaderProperty(Shadowborder, "Border");

                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Strength & Mask", "Strength and Mask Texture"), ShadowStrengthMask, ShadowStrength);
                        materialEditor.TextureScaleOffsetPropertyIndent(ShadowStrengthMask);
                    });

                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Blur & Mask", "Blur and Mask Texture"), ShadowborderBlurMask, ShadowborderBlur);
                        materialEditor.TextureScaleOffsetPropertyIndent(ShadowborderBlurMask);
                    });

                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(ShadowUseStep, "Use Steps");
                        var useStep = ShadowUseStep.floatValue;
                        if(useStep > 0)
                        {
                            EditorGUI.indentLevel ++;
                            ShadowSteps.floatValue = EditorGUILayout.IntSlider(
                                new GUIContent("Steps"),
                                (int)ShadowSteps.floatValue,
                                (int)ShadowSteps.rangeLimits.x,
                                (int)ShadowSteps.rangeLimits.y)
                            ;
                            EditorGUI.indentLevel --;
                        }
                    });

                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(ShadowPlanBUsePlanB, "Use Custom Shade");
                        var usePlanB = ShadowPlanBUsePlanB.floatValue;
                        if(usePlanB > 0)
                        {
                            EditorGUILayout.HelpBox(
                                "[Strength] max is recommended for using custom shade." + Environment.NewLine + "Custom Shadeの使用時は[Strength]を最大値に設定することを推奨", MessageType.Info);
                            materialEditor.ShaderProperty(ShadowPlanBDefaultShadowMix, "Mix Default Shade");
                            UIHelper.DrawWithGroup(() => {
                                EditorGUILayout.LabelField("1st shade", EditorStyles.boldLabel);
                                EditorGUI.indentLevel ++;
                                materialEditor.ShaderProperty(ShadowPlanBUseCustomShadowTexture, "Use Shade Texture");
                                var useShadeTexture = ShadowPlanBUseCustomShadowTexture.floatValue;
                                if(useShadeTexture > 0)
                                {
                                    materialEditor.ShaderProperty(ShadowPlanBCustomShadowTexture, "Shade Texture");
                                    materialEditor.ShaderProperty(ShadowPlanBCustomShadowTextureRGB, "Shade Texture RGB");
                                }
                                else
                                {
                                    materialEditor.ShaderProperty(ShadowPlanBHueShiftFromBase, "Hue Shift");
                                    materialEditor.ShaderProperty(ShadowPlanBSaturationFromBase, "Saturation");
                                    materialEditor.ShaderProperty(ShadowPlanBValueFromBase, "Value");
                                }
                                EditorGUI.indentLevel --;
                            });

                            UIHelper.DrawWithGroup(() => {
                                EditorGUILayout.LabelField("2nd shade", EditorStyles.boldLabel);
                                EditorGUI.indentLevel ++;
                                materialEditor.ShaderProperty(CustomShadow2nd, "Use");
                                var customshadow2nd = CustomShadow2nd.floatValue;
                                if(customshadow2nd > 0)
                                {
                                    materialEditor.ShaderProperty(ShadowPlanB2border, "Border");
                                    materialEditor.ShaderProperty(ShadowPlanB2borderBlur, "Blur");
                                    materialEditor.ShaderProperty(ShadowPlanB2UseCustomShadowTexture, "Use Shade Texture");
                                    var useShadeTexture2 = ShadowPlanB2UseCustomShadowTexture.floatValue;
                                    if(useShadeTexture2 > 0)
                                    {
                                        materialEditor.ShaderProperty(ShadowPlanB2CustomShadowTexture,  "Shade Texture");
                                        materialEditor.ShaderProperty(ShadowPlanB2CustomShadowTextureRGB,  "Shade Texture RGB");
                                    }else{
                                        materialEditor.ShaderProperty(ShadowPlanB2HueShiftFromBase, "Hue Shift");
                                        materialEditor.ShaderProperty(ShadowPlanB2SaturationFromBase, "Saturation");
                                        materialEditor.ShaderProperty(ShadowPlanB2ValueFromBase, "Value");
                                    }
                                }
                                EditorGUI.indentLevel --;
                            });
                        }
                    });
                });

                // Gloss
                UIHelper.ShurikenHeader("Gloss");
                materialEditor.DrawShaderPropertySameLIne(UseGloss);
                var isEnabledGloss = UseGloss.floatValue;
                if(isEnabledGloss > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Smoothness & Mask", "Smoothness and Mask Texture"), GlossBlendMask, GlossBlend);
                            materialEditor.TextureScaleOffsetPropertyIndent(GlossBlendMask);
                        });
                        materialEditor.ShaderProperty(GlossPower, "Metallic");
                        materialEditor.ShaderProperty(GlossColor, "Color");
                    });
                }

                // Outline
                if(!isRefracted) {
                    UIHelper.ShurikenHeader("Outline");
                    materialEditor.DrawShaderPropertySameLIne(UseOutline);
                    var useOutline = UseOutline.floatValue;
                    if(useOutline > 0)
                    {
                        UIHelper.DrawWithGroup(() => {
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Width & Mask", "Width and Mask Texture"), OutlineWidthMask, OutlineWidth);
                                materialEditor.TextureScaleOffsetPropertyIndent(OutlineWidthMask);
                            });
                            UIHelper.DrawWithGroup(() => {
                                if(!isOpaque) {
                                        materialEditor.TexturePropertySingleLine(new GUIContent("Cutoff Mask & Range", "Cutoff Mask Texture & Range"), OutlineMask, OutlineCutoffRange);
                                        materialEditor.TextureScaleOffsetPropertyIndent(OutlineMask);
                                }else{
                                    EditorGUILayout.LabelField("Cutoff Mask & Range","Unavailable in Opaque", EditorStyles.centeredGreyMiniLabel);
                                }
                            });
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), OutlineTexture, OutlineColor);
                                materialEditor.TextureScaleOffsetPropertyIndent(OutlineTexture);
                                materialEditor.ShaderProperty(OutlineTextureColorRate,"Base Color Mix");
                                materialEditor.ShaderProperty(OutlineUseColorShift, "Use Color Shift");
                                var isEnabledOutlineColorShift = OutlineUseColorShift.floatValue;
                                if(isEnabledOutlineColorShift > 0) {
                                    EditorGUI.indentLevel ++;
                                    materialEditor.ShaderProperty(OutlineHueShiftFromBase, "Hue Shift");
                                    materialEditor.ShaderProperty(OutlineSaturationFromBase, "Saturation");
                                    materialEditor.ShaderProperty(OutlineValueFromBase, "Value");
                                    EditorGUI.indentLevel --;
                                }
                            });
                            materialEditor.ShaderProperty(OutlineShadeMix,"Shadow mix");
                        });
                    }
                }

                // MatCap
                UIHelper.ShurikenHeader("MatCap");
                materialEditor.DrawShaderPropertySameLIne(MatcapBlendMode);
                var useMatcap = MatcapBlendMode.floatValue;
                if(useMatcap != 3) // Not 'Unused'
                {
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Blend & Mask", "Blend and Mask Texture"), MatcapBlendMask, MatcapBlend);
                            materialEditor.TextureScaleOffsetPropertyIndent(MatcapBlendMask);
                        });
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Color and Texture"), MatcapTexture, MatcapColor);
                            materialEditor.TextureScaleOffsetPropertyIndent(MatcapTexture);
                        });
                        materialEditor.ShaderProperty(MatcapNormalMix, "Normal Map mix");
                        materialEditor.ShaderProperty(MatcapShadeMix,"Shadow mix");
                    });
                }

                // Reflection
                UIHelper.ShurikenHeader("Reflection");
                materialEditor.DrawShaderPropertySameLIne(UseReflection);
                var useReflection = UseReflection.floatValue;
                if(useReflection > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(UseReflectionProbe,"Use Reflection Probe");
                        var useProbe = UseReflectionProbe.floatValue;
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Smoothness & Mask", "Smoothness and Mask Texture"), ReflectionReflectionMask, ReflectionReflectionPower);
                            materialEditor.TextureScaleOffsetPropertyIndent(ReflectionReflectionMask);
                        });
                        UIHelper.DrawWithGroup(() => {
                            var cubemapLabel = "Cubemap";
                            if(useProbe > 0) {
                                cubemapLabel += "(fallback)";
                            }
                            materialEditor.TexturePropertySingleLine(new GUIContent(cubemapLabel, cubemapLabel), ReflectionCubemap);
                            materialEditor.TextureScaleOffsetPropertyIndent(ReflectionCubemap);
                        });
                        materialEditor.ShaderProperty(ReflectionNormalMix,"Normal Map mix");
                        materialEditor.ShaderProperty(ReflectionShadeMix, "Shadow mix");
                        materialEditor.ShaderProperty(ReflectionSuppressBaseColorValue,"Suppress Base Color");
                    });
                }

                // Rim Light
                UIHelper.ShurikenHeader("Rim");
                materialEditor.DrawShaderPropertySameLIne(UseRim);
                var useRim = UseRim.floatValue;
                if(useRim > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Blend & Mask", "Blend and Mask Texture"), RimBlendMask, RimBlend);
                            materialEditor.TextureScaleOffsetPropertyIndent(RimBlendMask);
                        });
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), RimTexture, RimColor);
                            materialEditor.TextureScaleOffsetPropertyIndent(RimTexture);
                            materialEditor.ShaderProperty(RimUseBaseTexture,"Use Base Color");
                        });
                        materialEditor.ShaderProperty(RimShadeMix,"Shadow mix");
                        materialEditor.ShaderProperty(RimFresnelPower,"Fresnel Power");
                        materialEditor.ShaderProperty(RimUpperSideWidth,"Upper side width");
                    });
                }

                // Shade Cap
                UIHelper.ShurikenHeader("Shade Cap");
                materialEditor.DrawShaderPropertySameLIne(ShadowCapBlendMode);
                var useShadowCap = ShadowCapBlendMode.floatValue;
                if(useShadowCap != 3) // Not 'Unused'
                {
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Blend & Mask", "Blend and Mask Texture"), ShadowCapBlendMask, ShadowCapBlend);
                            materialEditor.TextureScaleOffsetPropertyIndent(ShadowCapBlendMask);
                        });
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Texture", "Texture"), ShadowCapTexture);
                            materialEditor.TextureScaleOffsetPropertyIndent(ShadowCapTexture);
                        });
                        materialEditor.ShaderProperty(ShadowCapNormalMix,"Normal Map mix");
                    });
                }

                // Stencil Writer
                if(isStencilWriter)
                {
                    UIHelper.ShurikenHeader("Stencil Writer");
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.ShaderProperty(StencilNumber,"Stencil Number");
                        if(isStencilWriterMask) {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Stencil Mask & Range", "Stencil Mask and Range"), StencilMaskTex, StencilMaskAdjust);
                            materialEditor.TextureScaleOffsetPropertyIndent(StencilMaskTex);
                        }
                        if(isStencilWriterMask) materialEditor.ShaderProperty(StencilMaskAlphaDither, "Alpha(Dither)");
                    });
                }

                // Stencil Reader
                if(isStencilReader)
                {
                    UIHelper.ShurikenHeader("Stencil Reader");
                    if(isStencilReaderDouble) {
                        UIHelper.DrawWithGroup(() => {
                            UIHelper.DrawWithGroup(() => {
                                EditorGUILayout.LabelField("Primary", EditorStyles.boldLabel);
                                EditorGUI.indentLevel++;
                                materialEditor.ShaderProperty(StencilNumber,"Number");
                                materialEditor.ShaderProperty(StencilCompareAction,"Compare Action");
                                EditorGUI.indentLevel--;
                            });
                            UIHelper.DrawWithGroup(() => {
                                EditorGUILayout.LabelField("Secondary", EditorStyles.boldLabel);
                                EditorGUI.indentLevel++;
                                materialEditor.ShaderProperty(StencilNumberSecondary,"Number");
                                materialEditor.ShaderProperty(StencilCompareActionSecondary,"Compare Action");
                                EditorGUI.indentLevel--;
                            });
                        });
                    } else {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.ShaderProperty(StencilNumber,"Number");
                            materialEditor.ShaderProperty(StencilCompareAction,"Compare Action");
                        });
                    }
                }

                // Parallax Emission
                UIHelper.ShurikenHeader("Parallaxed Emission");
                materialEditor.DrawShaderPropertySameLIne(UseEmissionParallax);
                var useEmissionPara = UseEmissionParallax.floatValue;
                if(useEmissionPara > 0){
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissionParallaxTex, EmissionParallaxColor);
                            materialEditor.TextureScaleOffsetPropertyIndent(EmissionParallaxTex);
                        });
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissionParallaxMask);
                            materialEditor.TextureScaleOffsetPropertyIndent(EmissionParallaxMask);
                        });
                        UIHelper.DrawWithGroup(() => {
                            materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissionParallaxDepthMask, EmissionParallaxDepth);
                            materialEditor.TextureScaleOffsetPropertyIndent(EmissionParallaxDepthMask);
                            materialEditor.ShaderProperty(EmissionParallaxDepthMaskInvert, "Invert Depth Mask");
                        });
                    });
                }

                // Scrolled Emission
                if(isEmissiveFreak)
                {
                    UIHelper.ShurikenHeader("Emissive Freak");
                    UIHelper.DrawWithGroup(() => {
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("1st", EditorStyles.boldLabel);
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissiveFreak1Tex, EmissiveFreak1Color, EmissiveFreak1Color2);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak1Tex);
                                materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissiveFreak1Mask);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak1Mask);
                                materialEditor.ShaderProperty(EmissiveFreak1ColorChangeSpeed, "Color Change Speed");
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak1U, "Scroll U");
                                    materialEditor.ShaderProperty(EmissiveFreak1V, "Scroll V");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissiveFreak1DepthMask, EmissiveFreak1Depth);
                                    materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak1DepthMask);
                                    materialEditor.ShaderProperty(EmissiveFreak1DepthMaskInvert, "Invert Depth Mask");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak1Breathing, "Breathing");
                                    materialEditor.ShaderProperty(EmissiveFreak1BreathingMix, "Breathing Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak1BlinkOut, "Blink Out");
                                    materialEditor.ShaderProperty(EmissiveFreak1BlinkOutMix, "Blink Out Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak1BlinkIn, "Blink In");
                                    materialEditor.ShaderProperty(EmissiveFreak1BlinkInMix, "Blink In Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak1HueShift, "Hue Shift");
                                });
                            });
                        });

                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("2nd", EditorStyles.boldLabel);
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissiveFreak2Tex, EmissiveFreak2Color, EmissiveFreak2Color2);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak2Tex);
                                materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissiveFreak2Mask);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak2Mask);
                                materialEditor.ShaderProperty(EmissiveFreak2ColorChangeSpeed, "Color Change Speed");
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak2U, "Scroll U");
                                    materialEditor.ShaderProperty(EmissiveFreak2V, "Scroll V");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissiveFreak2DepthMask, EmissiveFreak2Depth);
                                    materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak2DepthMask);
                                    materialEditor.ShaderProperty(EmissiveFreak2DepthMaskInvert, "Invert Depth Mask");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak2Breathing, "Breathing");
                                    materialEditor.ShaderProperty(EmissiveFreak2BreathingMix, "Breathing Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak2BlinkOut, "Blink Out");
                                    materialEditor.ShaderProperty(EmissiveFreak2BlinkOutMix, "Blink Out Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak2BlinkIn, "Blink In");
                                    materialEditor.ShaderProperty(EmissiveFreak2BlinkInMix, "Blink In Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak2HueShift, "Hue Shift");
                                });
                            });
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("3rd", EditorStyles.boldLabel);
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissiveFreak3Tex, EmissiveFreak3Color, EmissiveFreak3Color2);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak3Tex);
                                materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissiveFreak3Mask);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak3Mask);
                                materialEditor.ShaderProperty(EmissiveFreak3ColorChangeSpeed, "Color Change Speed");
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak3U, "Scroll U");
                                    materialEditor.ShaderProperty(EmissiveFreak3V, "Scroll V");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissiveFreak3DepthMask, EmissiveFreak3Depth);
                                    materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak3DepthMask);
                                    materialEditor.ShaderProperty(EmissiveFreak3DepthMaskInvert, "Invert Depth Mask");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak3Breathing, "Breathing");
                                    materialEditor.ShaderProperty(EmissiveFreak3BreathingMix, "Breathing Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak3BlinkOut, "Blink Out");
                                    materialEditor.ShaderProperty(EmissiveFreak3BlinkOutMix, "Blink Out Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak3BlinkIn, "Blink In");
                                    materialEditor.ShaderProperty(EmissiveFreak3BlinkInMix, "Blink In Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak3HueShift, "Hue Shift");
                                });
                            });
                        });
                        
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("4th", EditorStyles.boldLabel);
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissiveFreak4Tex, EmissiveFreak4Color, EmissiveFreak4Color2);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak4Tex);
                                materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissiveFreak4Mask);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak4Mask);
                                materialEditor.ShaderProperty(EmissiveFreak4ColorChangeSpeed, "Color Change Speed");
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak4U, "Scroll U");
                                    materialEditor.ShaderProperty(EmissiveFreak4V, "Scroll V");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissiveFreak4DepthMask, EmissiveFreak4Depth);
                                    materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak4DepthMask);
                                    materialEditor.ShaderProperty(EmissiveFreak4DepthMaskInvert, "Invert Depth Mask");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak4Breathing, "Breathing");
                                    materialEditor.ShaderProperty(EmissiveFreak4BreathingMix, "Breathing Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak4BlinkOut, "Blink Out");
                                    materialEditor.ShaderProperty(EmissiveFreak4BlinkOutMix, "Blink Out Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak4BlinkIn, "Blink In");
                                    materialEditor.ShaderProperty(EmissiveFreak4BlinkInMix, "Blink In Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak4HueShift, "Hue Shift");
                                });
                            });
                        });
                        
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("5th", EditorStyles.boldLabel);
                            UIHelper.DrawWithGroup(() => {
                                materialEditor.TexturePropertySingleLine(new GUIContent("Texture & Color", "Texture and Color"), EmissiveFreak5Tex, EmissiveFreak5Color, EmissiveFreak5Color2);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak5Tex);
                                materialEditor.TexturePropertySingleLine(new GUIContent("TexCol Mask", "Texture and Color Mask"), EmissiveFreak5Mask);
                                materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak5Mask);
                                materialEditor.ShaderProperty(EmissiveFreak5ColorChangeSpeed, "Color Change Speed");
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak5U, "Scroll U");
                                    materialEditor.ShaderProperty(EmissiveFreak5V, "Scroll V");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.TexturePropertySingleLine(new GUIContent("Depth & Mask", "Depth and Mask Texture"), EmissiveFreak5DepthMask, EmissiveFreak5Depth);
                                    materialEditor.TextureScaleOffsetPropertyIndent(EmissiveFreak5DepthMask);
                                    materialEditor.ShaderProperty(EmissiveFreak5DepthMaskInvert, "Invert Depth Mask");
                                });
                                UIHelper.DrawWithGroup(() => {
                                    materialEditor.ShaderProperty(EmissiveFreak5Breathing, "Breathing");
                                    materialEditor.ShaderProperty(EmissiveFreak5BreathingMix, "Breathing Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak5BlinkOut, "Blink Out");
                                    materialEditor.ShaderProperty(EmissiveFreak5BlinkOutMix, "Blink Out Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak5BlinkIn, "Blink In");
                                    materialEditor.ShaderProperty(EmissiveFreak5BlinkInMix, "Blink In Mix");
                                    materialEditor.ShaderProperty(EmissiveFreak5HueShift, "Hue Shift");
                                });
                            });
                        });
                    });
                }


                // Advanced / Experimental
                IsShowAdvanced = UIHelper.ShurikenFoldout("Advanced / Experimental (Click to Open)", IsShowAdvanced);
                if (IsShowAdvanced) {
                    UIHelper.DrawWithGroup(() => {
                        EditorGUILayout.HelpBox("These are some shade tweaking. no need to change usually." + Environment.NewLine + "ほとんどのケースで触る必要のないやつら。",MessageType.Info);
                        if (GUILayout.Button("Revert advanced params.")){
                            PointAddIntensity.floatValue = 1f;
                            PointShadowStrength.floatValue = 0.5f;
                            PointShadowborder.floatValue = 0.5f;
                            PointShadowborderBlur.floatValue = 0.01f;
                            PointShadowborderBlurMask.textureValue = null;
                            OtherShadowAdjust.floatValue = -0.1f;
                            OtherShadowBorderSharpness.floatValue = 3;
                            PointShadowUseStep.floatValue = 0;
                            PointShadowSteps.floatValue = 2;
                            ShadowIndirectIntensity.floatValue = 0.25f;
                            VertexColorBlendDiffuse.floatValue = 0f;
                            VertexColorBlendEmissive.floatValue = 0f;
                            UseVertexLight.floatValue = 1f;
                            LightSampling.floatValue = 0f;
                            UsePositionRelatedCalc.floatValue = 0f;
                        }
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("Lights", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(LightSampling, "Sampling Style (def:arktoon)");
                            EditorGUI.indentLevel --;
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("Directional Shadow", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(ShadowIndirectIntensity, "Indirect face Intensity (0.25)");
                            EditorGUI.indentLevel --;
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("Vertex Colors", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(VertexColorBlendDiffuse, "Color blend to diffuse (def:0) ");
                            materialEditor.ShaderProperty(VertexColorBlendEmissive, "Color blend to emissive (def:0) ");
                            EditorGUI.indentLevel --;
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("PointLights", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(PointAddIntensity, "Intensity (def:1)");
                            materialEditor.ShaderProperty(PointShadowStrength, "Shadow Strength (def:0.5)");
                            materialEditor.ShaderProperty(PointShadowborder, "Shadow Border (def:0.5)");
                            materialEditor.ShaderProperty(PointShadowborderBlur, "Shadow Border blur (def:0.01)");
                            materialEditor.ShaderProperty(PointShadowborderBlurMask, "Shadow Border blur Mask(def:none)");
                            materialEditor.ShaderProperty(PointShadowUseStep, "Use Shadow Steps");
                            var usePointStep = PointShadowUseStep.floatValue;
                            if(usePointStep > 0)
                            {
                                materialEditor.ShaderProperty(PointShadowSteps, " ");
                            }
                            materialEditor.ShaderProperty(UseVertexLight, "Use Per-vertex Light");
                            EditorGUI.indentLevel --;
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("Shade from other meshes", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(OtherShadowAdjust, "Adjust (def:-0.1)");
                            materialEditor.ShaderProperty(OtherShadowBorderSharpness, "Sharpness(def:3)");
                            EditorGUI.indentLevel --;
                        });
                        UIHelper.DrawWithGroup(() => {
                            EditorGUILayout.LabelField("MatCap / ShadeCap", EditorStyles.boldLabel);
                            EditorGUI.indentLevel ++;
                            materialEditor.ShaderProperty(UsePositionRelatedCalc, "Use Position Related Calc(def: no)");
                            EditorGUI.indentLevel --;
                        });
                    });
                }

                // Arktoon version info
                string localVersion =  EditorUserSettings.GetConfigValue ("arktoon_version_local");
                string remoteVersion = EditorUserSettings.GetConfigValue ("arktoon_version_remote");

                UIHelper.ShurikenHeader("Arktoon-Shaders");
                style.alignment = TextAnchor.MiddleRight;
                style.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                EditorGUILayout.LabelField("Your Version : " + localVersion, style);

                if (!string.IsNullOrEmpty(remoteVersion))
                {
                    Version local_v = new Version(localVersion);
                    Version remote_v = new Version(remoteVersion);

                    if(remote_v > local_v)  {
                        style.normal.textColor = EditorGUIUtility.isProSkin ? Color.cyan : Color.blue;
                        EditorGUILayout.LabelField("Remote Version : " + remoteVersion, style);
                        EditorGUILayout.BeginHorizontal( GUI.skin.box );
                        {
                            style.alignment = TextAnchor.MiddleLeft;
                            EditorGUILayout.LabelField("New version available : ", style);
                            if(GUILayout.Button("Open download page."))
                            {
                                System.Diagnostics.Process.Start("https://github.com/synqark/Arktoon-Shaders/releases");
                            }
                        }
                        GUILayout.EndHorizontal();
                    } else {
                        EditorGUILayout.LabelField("Remote Version : " + remoteVersion, style);
                    }
                }

                // Docs
                UIHelper.DrawWithGroupHorizontal(() => {
                    if(GUILayout.Button("How to use (Japanese)"))
                    {
                        System.Diagnostics.Process.Start("https://synqark.github.io/Arktoon-Shaders-Doc/");
                    }
                    if(GUILayout.Button("README.md (English)"))
                    {
                        System.Diagnostics.Process.Start("https://github.com/synqark/Arktoon-Shaders/blob/master/README.md");
                    }
                });
            }
            EditorGUI.EndChangeCheck();
        }
    }

    static class UIHelper
    {
        static int HEADER_HEIGHT = 22;

        public static void DrawShaderPropertySameLIne(this MaterialEditor editor, MaterialProperty property) {
            Rect r = EditorGUILayout.GetControlRect(true,0,EditorStyles.layerMaskField);
            r.y -= HEADER_HEIGHT;
            r.height = MaterialEditor.GetDefaultPropertyHeight(property);
            editor.ShaderProperty(r, property, " ");
        }

        private static Rect DrawShuriken(string title, Vector2 contentOffset) {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.margin = new RectOffset(0, 0, 8, 0);
            style.font = new GUIStyle(EditorStyles.boldLabel).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = HEADER_HEIGHT;
            style.contentOffset = contentOffset;
            var rect = GUILayoutUtility.GetRect(16f, HEADER_HEIGHT, style);
            GUI.Box(rect, title, style);
            return rect;
        }
        public static void ShurikenHeader(string title)
        {
            DrawShuriken(title,new Vector2(6f, -2f));
        }
        public static bool ShurikenFoldout(string title, bool display)
        {
            var rect = DrawShuriken(title,new Vector2(20f, -2f));
            var e = Event.current;
            var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint) {
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            }
            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
                display = !display;
                e.Use();
            }
            return display;
        }
        public static void Vector2Property(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 vector2 = EditorGUILayout.Vector2Field(name,new Vector2(property.vectorValue.x, property.vectorValue.y),null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = new Vector4(vector2.x, vector2.y);
        }
        public static void Vector4Property(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector4 vector4 = EditorGUILayout.Vector2Field(name,property.vectorValue,null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = vector4;
        }
        public static void Vector2PropertyZW(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 vector2 = EditorGUILayout.Vector2Field(name,new Vector2(property.vectorValue.x, property.vectorValue.y),null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = new Vector4(vector2.x, vector2.y);
        }
        public static void TextureScaleOffsetPropertyIndent(this MaterialEditor editor, MaterialProperty property)
        {
            EditorGUI.indentLevel ++;
            editor.TextureScaleOffsetProperty(property);
            EditorGUI.indentLevel --;
        }
        public static void DrawWithGroup(Action action)
        {
            EditorGUILayout.BeginVertical( GUI.skin.box );
            action();
            EditorGUILayout.EndVertical();
        }
        public static void DrawWithGroupHorizontal(Action action)
        {
            EditorGUILayout.BeginHorizontal( GUI.skin.box );
            action();
            EditorGUILayout.EndHorizontal();
        }
    }

    // シェーダーキーワードを作らないToggle (Unity 2018.2以降で存在するUIToggleと同じ。)
    internal class MaterialATSToggleDrawer : MaterialPropertyDrawer
    {
        public MaterialATSToggleDrawer()
        {
        }

        public MaterialATSToggleDrawer(string keyword)
        {
        }

        protected virtual void SetKeyword(MaterialProperty prop, bool on)
        {
        }

        static bool IsPropertyTypeSuitable(MaterialProperty prop)
        {
            return prop.type == MaterialProperty.PropType.Float || prop.type == MaterialProperty.PropType.Range;
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return base.GetPropertyHeight(prop, label, editor);
        }

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            EditorGUI.BeginChangeCheck();

            bool value = (Math.Abs(prop.floatValue) > 0.001f);
            EditorGUI.showMixedValue = prop.hasMixedValue;
            value = EditorGUI.Toggle(position, label, value);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.floatValue = value ? 1.0f : 0.0f;
                SetKeyword(prop, value);
            }
        }

        public override void Apply(MaterialProperty prop)
        {
            base.Apply(prop);
            if (!IsPropertyTypeSuitable(prop))
                return;

            if (prop.hasMixedValue)
                return;

            SetKeyword(prop, (Math.Abs(prop.floatValue) > 0.001f));
        }
    }

}