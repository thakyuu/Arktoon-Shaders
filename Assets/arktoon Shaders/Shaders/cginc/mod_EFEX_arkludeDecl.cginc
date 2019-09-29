#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"


#if defined(ARKTOON_SECONDARY)
    // Secondary
    UNITY_DECLARE_TEX2D(_MainTexSecondary); uniform float4 _MainTexSecondary_ST;
    uniform float4 _ColorSecondary;
    uniform sampler2D _BumpMapSecondary; uniform float4 _BumpMapSecondary_ST;
    uniform float _BumpScaleSecondary;
    uniform sampler2D _EmissionMapSecondary; uniform float4 _EmissionMapSecondary_ST;
    uniform float4 _EmissionColorSecondary;
#   define REF_MAINTEX _MainTexSecondary
#   define REF_COLOR _ColorSecondary
#   define REF_BUMPMAP _BumpMapSecondary
#   define REF_BUMPSCALE _BumpScaleSecondary
#   define REF_EMISSIONMAP _EmissionMapSecondary
#   define REF_EMISSIONCOLOR _EmissionColorSecondary
#else
    // Main, Normal, Emission
    UNITY_DECLARE_TEX2D(_MainTex); uniform float4 _MainTex_ST;
    uniform float4 _Color;
    uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
    uniform float _BumpScale;
    uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
    uniform float4 _EmissionColor;
#   define REF_MAINTEX _MainTex
#   define REF_COLOR _Color
#   define REF_BUMPMAP _BumpMap
#   define REF_BUMPSCALE _BumpScale
#   define REF_EMISSIONMAP _EmissionMap
#   define REF_EMISSIONCOLOR _EmissionColor
#endif

// Alpha Mask
UNITY_DECLARE_TEX2D_NOSAMPLER(_AlphaMask); uniform float4 _AlphaMask_ST;

// Emission Parallax
uniform float _UseEmissionParallax;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionParallaxTex); uniform float4 _EmissionParallaxTex_ST;
uniform float4 _EmissionParallaxColor;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionParallaxMask); uniform float4 _EmissionParallaxMask_ST;
uniform float _EmissionParallaxDepth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissionParallaxDepthMask); uniform float4 _EmissionParallaxDepthMask_ST;
uniform float _EmissionParallaxDepthMaskInvert;

// Double Side
uniform int _UseDoubleSided;
uniform float _DoubleSidedBackfaceLightIntensity;
uniform int _DoubleSidedBackfaceUseColorShift;
uniform float _DoubleSidedBackfaceHueShiftFromBase;
uniform float _DoubleSidedBackfaceSaturationFromBase;
uniform float _DoubleSidedBackfaceValueFromBase;
uniform float _DoubleSidedFlipBackfaceNormal;

// Shadow
uniform float _Shadowborder;
uniform float _ShadowborderBlur;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ShadowborderBlurMask); uniform float4 _ShadowborderBlurMask_ST;
uniform float _ShadowStrength;
uniform float _ShadowIndirectIntensity;
uniform float _ShadowUseStep;
uniform int _ShadowSteps;
uniform sampler2D _ShadowStrengthMask; uniform float4 _ShadowStrengthMask_ST;

// Custom shade1
uniform int _ShadowPlanBUsePlanB;
uniform float _ShadowPlanBDefaultShadowMix;
uniform float _ShadowPlanBHueShiftFromBase;
uniform float _ShadowPlanBSaturationFromBase;
uniform float _ShadowPlanBValueFromBase;
uniform int _ShadowPlanBUseCustomShadowTexture;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ShadowPlanBCustomShadowTexture); uniform float4 _ShadowPlanBCustomShadowTexture_ST;
uniform float4 _ShadowPlanBCustomShadowTextureRGB;

// Cutsom shade2
uniform int _CustomShadow2nd;
uniform float _ShadowPlanB2border;
uniform float _ShadowPlanB2borderBlur;
uniform float _ShadowPlanB2HueShiftFromBase;
uniform float _ShadowPlanB2SaturationFromBase;
uniform float _ShadowPlanB2ValueFromBase;
uniform int _ShadowPlanB2UseCustomShadowTexture;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ShadowPlanB2CustomShadowTexture); uniform float4 _ShadowPlanB2CustomShadowTexture_ST;
uniform float4 _ShadowPlanB2CustomShadowTextureRGB;

// Outline
uniform int _UseOutline;
UNITY_DECLARE_TEX2D_NOSAMPLER(_OutlineMask); uniform float4 _OutlineMask_ST;
uniform float _OutlineCutoffRange;
uniform float _OutlineTextureColorRate;
uniform float _OutlineShadeMix;
uniform float _OutlineWidth;
uniform float4 _OutlineColor;
uniform sampler2D _OutlineTexture; uniform float4 _OutlineTexture_ST;
uniform sampler2D _OutlineWidthMask; uniform float4 _OutlineWidthMask_ST; // FIXME:tex2dLodはUNITY_SAMPLE_TEX2D_SAMPLERの代用が判らないためいったん保留
uniform float _OutlineUseColorShift;
uniform float _OutlineHueShiftFromBase;
uniform float _OutlineSaturationFromBase;
uniform float _OutlineValueFromBase;

// Gloss
uniform int _UseGloss;
uniform float _GlossBlend;
UNITY_DECLARE_TEX2D_NOSAMPLER(_GlossBlendMask); uniform float4 _GlossBlendMask_ST;
uniform float _GlossPower;
uniform float4 _GlossColor;
uniform float _CutoutCutoutAdjust;

// Point lights
uniform float _PointAddIntensity;
uniform float _PointShadowStrength;
uniform float _PointShadowborder;
uniform float _PointShadowborderBlur;
UNITY_DECLARE_TEX2D_NOSAMPLER(_PointShadowborderBlurMask); uniform float4 _PointShadowborderBlurMask_ST;
uniform float _PointShadowUseStep;
uniform int _PointShadowSteps;

// MatCap
uniform int _MatcapBlendMode;
UNITY_DECLARE_TEX2D_NOSAMPLER(_MatcapTexture); uniform float4 _MatcapTexture_ST;
uniform float _MatcapBlend;
UNITY_DECLARE_TEX2D_NOSAMPLER(_MatcapBlendMask); uniform float4 _MatcapBlendMask_ST;
uniform float4 _MatcapColor;
uniform float _MatcapNormalMix;
uniform float _MatcapShadeMix;

// Reflection
uniform int _UseReflection;
uniform int _UseReflectionProbe;
uniform float _ReflectionReflectionPower;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ReflectionReflectionMask); uniform float4 _ReflectionReflectionMask_ST;
uniform float _ReflectionNormalMix;
uniform float _ReflectionShadeMix;
uniform float _ReflectionSuppressBaseColorValue;
uniform samplerCUBE _ReflectionCubemap;
uniform half4  _ReflectionCubemap_HDR;

// Rim
uniform int _UseRim;
uniform float _RimFresnelPower;
uniform float _RimUpperSideWidth;
uniform float4 _RimColor;
uniform fixed _RimUseBaseTexture;
uniform float _RimBlend;
uniform float _RimShadeMix;
UNITY_DECLARE_TEX2D_NOSAMPLER(_RimBlendMask); uniform float4 _RimBlendMask_ST;
UNITY_DECLARE_TEX2D_NOSAMPLER(_RimTexture); uniform float4 _RimTexture_ST;

// Shade cap (Shadow cap)
uniform int _ShadowCapBlendMode;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ShadowCapTexture); uniform float4 _ShadowCapTexture_ST;
UNITY_DECLARE_TEX2D_NOSAMPLER(_ShadowCapBlendMask); uniform float4 _ShadowCapBlendMask_ST;
uniform float _ShadowCapBlend;
uniform float _ShadowCapNormalMix;

// Use vertexLight
uniform int _UseVertexLight;

// Vertex Color Blend
uniform float _VertexColorBlendDiffuse;
uniform float _VertexColorBlendEmissive;

// Shade from other objects.
uniform float _OtherShadowAdjust;
uniform float _OtherShadowBorderSharpness;

// Experimental Cap calculation
uniform int _UsePositionRelatedCalc;

// light sampling
uniform int _LightSampling;

// Refraction IF refracted
#ifdef ARKTOON_REFRACTED
uniform sampler2D _GrabTexture;
uniform float _RefractionFresnelExp;
uniform float _RefractionStrength;
#endif

// ScrolledEmission
#ifdef ARKTOON_EMISSIVE_FREAK
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak1Tex); uniform float4 _EmissiveFreak1Tex_ST;
uniform float4 _EmissiveFreak1Color;
uniform float4 _EmissiveFreak1Color2;
uniform float _EmissiveFreak1ColorChangeSpeed;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak1Mask); uniform float4 _EmissiveFreak1Mask_ST;
uniform float _EmissiveFreak1U;
uniform float _EmissiveFreak1V;
uniform float _EmissiveFreak1Depth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak1DepthMask); uniform float4 _EmissiveFreak1DepthMask_ST;
uniform float _EmissiveFreak1DepthMaskInvert;
uniform float _EmissiveFreak1Breathing;
uniform float _EmissiveFreak1BreathingMix;
uniform float _EmissiveFreak1BlinkOut;
uniform float _EmissiveFreak1BlinkOutMix;
uniform float _EmissiveFreak1BlinkIn;
uniform float _EmissiveFreak1BlinkInMix;
uniform float _EmissiveFreak1HueShift;

UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak2Tex); uniform float4 _EmissiveFreak2Tex_ST;
uniform float4 _EmissiveFreak2Color;
uniform float4 _EmissiveFreak2Color2;
uniform float _EmissiveFreak2ColorChangeSpeed;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak2Mask); uniform float4 _EmissiveFreak2Mask_ST;
uniform float _EmissiveFreak2U;
uniform float _EmissiveFreak2V;
uniform float _EmissiveFreak2Depth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak2DepthMask); uniform float4 _EmissiveFreak2DepthMask_ST;
uniform float _EmissiveFreak2DepthMaskInvert;
uniform float _EmissiveFreak2Breathing;
uniform float _EmissiveFreak2BreathingMix;
uniform float _EmissiveFreak2BlinkOut;
uniform float _EmissiveFreak2BlinkOutMix;
uniform float _EmissiveFreak2BlinkIn;
uniform float _EmissiveFreak2BlinkInMix;
uniform float _EmissiveFreak2HueShift;

UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak3Tex); uniform float4 _EmissiveFreak3Tex_ST;
uniform float4 _EmissiveFreak3Color;
uniform float4 _EmissiveFreak3Color2;
uniform float _EmissiveFreak3ColorChangeSpeed;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak3Mask); uniform float4 _EmissiveFreak3Mask_ST;
uniform float _EmissiveFreak3U;
uniform float _EmissiveFreak3V;
uniform float _EmissiveFreak3Depth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak3DepthMask); uniform float4 _EmissiveFreak3DepthMask_ST;
uniform float _EmissiveFreak3DepthMaskInvert;
uniform float _EmissiveFreak3Breathing;
uniform float _EmissiveFreak3BreathingMix;
uniform float _EmissiveFreak3BlinkOut;
uniform float _EmissiveFreak3BlinkOutMix;
uniform float _EmissiveFreak3BlinkIn;
uniform float _EmissiveFreak3BlinkInMix;
uniform float _EmissiveFreak3HueShift;

UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak4Tex); uniform float4 _EmissiveFreak4Tex_ST;
uniform float4 _EmissiveFreak4Color;
uniform float4 _EmissiveFreak4Color2;
uniform float _EmissiveFreak4ColorChangeSpeed;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak4Mask); uniform float4 _EmissiveFreak4Mask_ST;
uniform float _EmissiveFreak4U;
uniform float _EmissiveFreak4V;
uniform float _EmissiveFreak4Depth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak4DepthMask); uniform float4 _EmissiveFreak4DepthMask_ST;
uniform float _EmissiveFreak4DepthMaskInvert;
uniform float _EmissiveFreak4Breathing;
uniform float _EmissiveFreak4BreathingMix;
uniform float _EmissiveFreak4BlinkOut;
uniform float _EmissiveFreak4BlinkOutMix;
uniform float _EmissiveFreak4BlinkIn;
uniform float _EmissiveFreak4BlinkInMix;
uniform float _EmissiveFreak4HueShift;

UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak5Tex); uniform float4 _EmissiveFreak5Tex_ST;
uniform float4 _EmissiveFreak5Color;
uniform float4 _EmissiveFreak5Color2;
uniform float _EmissiveFreak5ColorChangeSpeed;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak5Mask); uniform float4 _EmissiveFreak5Mask_ST;
uniform float _EmissiveFreak5U;
uniform float _EmissiveFreak5V;
uniform float _EmissiveFreak5Depth;
UNITY_DECLARE_TEX2D_NOSAMPLER(_EmissiveFreak5DepthMask); uniform float4 _EmissiveFreak5DepthMask_ST;
uniform float _EmissiveFreak5DepthMaskInvert;
uniform float _EmissiveFreak5Breathing;
uniform float _EmissiveFreak5BreathingMix;
uniform float _EmissiveFreak5BlinkOut;
uniform float _EmissiveFreak5BlinkOutMix;
uniform float _EmissiveFreak5BlinkIn;
uniform float _EmissiveFreak5BlinkInMix;
uniform float _EmissiveFreak5HueShift;

#endif