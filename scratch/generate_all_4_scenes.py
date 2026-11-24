import os

header = """%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!29 &1
OcclusionCullingSettings:
  m_ObjectHideFlags: 0
  serializedVersion: 2
  m_OcclusionBakeSettings:
    smallestOccluder: 5
    smallestHole: 0.25
    backfaceThreshold: 100
  m_SceneGUID: 00000000000000000000000000000000
  m_OcclusionCullingData: {fileID: 0}
--- !u!104 &2
RenderSettings:
  m_ObjectHideFlags: 0
  serializedVersion: 10
  m_Fog: 0
  m_FogColor: {r: 0.5, g: 0.5, b: 0.5, a: 1}
  m_FogMode: 3
  m_FogDensity: 0.01
  m_LinearFogStart: 0
  m_LinearFogEnd: 300
  m_AmbientSkyColor: {r: 0.212, g: 0.227, b: 0.259, a: 1}
  m_AmbientEquatorColor: {r: 0.114, g: 0.125, b: 0.133, a: 1}
  m_AmbientGroundColor: {r: 0.047, g: 0.043, b: 0.035, a: 1}
  m_AmbientIntensity: 1
  m_AmbientMode: 3
  m_SubtractiveShadowColor: {r: 0.42, g: 0.478, b: 0.627, a: 1}
  m_SkyboxMaterial: {fileID: 0}
  m_HaloStrength: 0.5
  m_FlareStrength: 1
  m_FlareFadeSpeed: 3
  m_HaloTexture: {fileID: 0}
  m_SpotCookie: {fileID: 10001, guid: 0000000000000000e000000000000000, type: 0}
  m_DefaultReflectionMode: 0
  m_DefaultReflectionResolution: 128
  m_ReflectionBounces: 1
  m_ReflectionIntensity: 1
  m_CustomReflection: {fileID: 0}
  m_Sun: {fileID: 0}
  m_UseRadianceAmbientProbe: 0
--- !u!157 &3
LightmapSettings:
  m_ObjectHideFlags: 0
  serializedVersion: 13
  m_BakeOnSceneLoad: 0
  m_GISettings:
    serializedVersion: 2
    m_BounceScale: 1
    m_IndirectOutputScale: 1
    m_AlbedoBoost: 1
    m_EnvironmentLightingMode: 0
    m_EnableBakedLightmaps: 0
    m_EnableRealtimeLightmaps: 0
  m_LightmapEditorSettings:
    serializedVersion: 12
    m_Resolution: 2
    m_BakeResolution: 40
    m_AtlasSize: 1024
    m_AO: 0
    m_AOMaxDistance: 1
    m_CompAOExponent: 1
    m_CompAOExponentDirect: 0
    m_ExtractAmbientOcclusion: 0
    m_Padding: 2
    m_LightmapParameters: {fileID: 0}
    m_LightmapsBakeMode: 1
    m_TextureCompression: 1
    m_ReflectionCompression: 2
    m_MixedBakeMode: 2
--- !u!196 &4
NavMeshSettings:
  serializedVersion: 2
  m_ObjectHideFlags: 0
  m_BuildSettings:
    serializedVersion: 3
    agentTypeID: 0
    agentRadius: 0.5
    agentHeight: 2
    agentSlope: 45
    agentClimb: 0.4
    ledgeDropHeight: 0
    maxJumpAcrossDistance: 0
    minRegionArea: 2
    manualCellSize: 0
    cellSize: 0.16666667
    manualTileSize: 0
    tileSize: 256
    buildHeightMesh: 0
    maxJobWorkers: 0
    preserveTilesOutsideBounds: 0
    debug:
      m_Flags: 0
  m_NavMeshData: {fileID: 0}
"""

def make_cam_es(start_id):
    cid = start_id
    cam_go = cid; cid += 1
    cam_tr = cid; cid += 1
    cam_c  = cid; cid += 1
    cam_al = cid; cid += 1
    es_go  = cid; cid += 1
    es_tr  = cid; cid += 1
    es_c   = cid; cid += 1
    es_im  = cid; cid += 1

    s = f"""--- !u!1 &{cam_go}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {cam_tr}}}
  - component: {{fileID: {cam_c}}}
  - component: {{fileID: {cam_al}}}
  m_Layer: 0
  m_Name: Main Camera
  m_TagString: MainCamera
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!4 &{cam_tr}
Transform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cam_go}}}
  serializedVersion: 2
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: -10}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children: []
  m_Father: {{fileID: 0}}
  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}
--- !u!20 &{cam_c}
Camera:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cam_go}}}
  m_Enabled: 1
  serializedVersion: 2
  m_ClearFlags: 2
  m_BackGroundColor: {{r: 0.05, g: 0.04, b: 0.09, a: 1}}
  m_projectionMatrixMode: 1
  m_GateFitMode: 2
  m_FOVAxisMode: 0
  m_Iso: 200
  m_ShutterSpeed: 0.005
  m_Aperture: 16
  m_FocusDistance: 10
  m_FocalLength: 50
  m_BladeCount: 5
  m_Curvature: {{x: 2, y: 11}}
  m_BarrelClipping: 0.25
  m_Anamorphism: 0
  m_SensorSize: {{x: 36, y: 24}}
  m_LensShift: {{x: 0, y: 0}}
  m_NormalizedViewPortRect:
    serializedVersion: 2
    x: 0
    y: 0
    width: 1
    height: 1
  near clip plane: 0.3
  far clip plane: 1000
  field of view: 60
  orthographic: 1
  orthographic size: 5
  m_Depth: -1
  m_CullingMask:
    serializedVersion: 2
    m_Bits: 4294967295
  m_RenderingPath: -1
  m_TargetTexture: {{fileID: 0}}
  m_TargetDisplay: 0
  m_TargetEye: 3
  m_HDR: 1
  m_AllowMSAA: 1
  m_AllowDynamicResolution: 0
  m_ForceIntoRT: 0
  m_OcclusionCulling: 1
  m_StereoConvergence: 10
  m_StereoSeparation: 0.022
--- !u!81 &{cam_al}
AudioListener:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cam_go}}}
  m_Enabled: 1
--- !u!1 &{es_go}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {es_tr}}}
  - component: {{fileID: {es_c}}}
  - component: {{fileID: {es_im}}}
  m_Layer: 0
  m_Name: EventSystem
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!4 &{es_tr}
Transform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {es_go}}}
  serializedVersion: 2
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children: []
  m_Father: {{fileID: 0}}
  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}
--- !u!114 &{es_c}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {es_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 76c392e42b5098c458856cdf6ecaaaa1, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_FirstSelected: {{fileID: 0}}
  m_sendNavigationEvents: 1
  m_DragThreshold: 10
--- !u!114 &{es_im}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {es_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 4f231c4fb786f3946a6b90b886c48677, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_SendPointerHoverToParent: 1
  m_HorizontalAxis: Horizontal
  m_VerticalAxis: Vertical
  m_SubmitButton: Submit
  m_CancelButton: Cancel
  m_InputActionsPerSecond: 10
  m_RepeatDelay: 0.5
  m_ForceModuleActive: 0"""
    return s, cid

def make_canvas(cid, name, children_rt_ids, mb_block=""):
    cv_go = cid; cid += 1
    cv_rt = cid; cid += 1
    cv_c  = cid; cid += 1
    cv_cs = cid; cid += 1
    cv_gr = cid; cid += 1
    cv_mb = cid; cid += 1

    ch_yaml = "".join(["  - {fileID: " + str(ch) + "}\n" for ch in children_rt_ids])

    s = f"""--- !u!1 &{cv_go}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {cv_rt}}}
  - component: {{fileID: {cv_c}}}
  - component: {{fileID: {cv_cs}}}
  - component: {{fileID: {cv_gr}}}
  - component: {{fileID: {cv_mb}}}
  m_Layer: 5
  m_Name: {name}
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!224 &{cv_rt}
RectTransform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cv_go}}}
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 0, y: 0, z: 0}}
  m_ConstrainProportionsScale: 0
  m_Children:
{ch_yaml}  m_Father: {{fileID: 0}}
  m_AnchorMin: {{x: 0, y: 0}}
  m_AnchorMax: {{x: 0, y: 0}}
  m_AnchoredPosition: {{x: 0, y: 0}}
  m_SizeDelta: {{x: 0, y: 0}}
  m_Pivot: {{x: 0, y: 0}}
--- !u!223 &{cv_c}
Canvas:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cv_go}}}
  m_Enabled: 1
  serializedVersion: 3
  m_RenderMode: 0
  m_Camera: {{fileID: 0}}
  m_PlaneDistance: 100
  m_PixelPerfect: 0
  m_ReceivesEvents: 1
  m_OverrideSorting: 0
  m_OverridePixelPerfect: 0
  m_SortingBucketNormalizedSize: 0
  m_VertexColorAlwaysGammaSpace: 0
  m_AdditionalShaderChannelsFlag: 25
  m_UpdateRectTransformForStandalone: 0
  m_SortingLayerID: 0
  m_SortingOrder: 0
  m_TargetDisplay: 0
--- !u!114 &{cv_cs}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cv_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 0cd44c1031e13a943bb63640046fad76, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_UiScaleMode: 1
  m_ReferencePixelsPerUnit: 100
  m_ScaleFactor: 1
  m_ReferenceResolution: {{x: 1920, y: 1080}}
  m_ScreenMatchMode: 0
  m_MatchWidthOrHeight: 0.5
  m_PhysicalUnit: 3
  m_FallbackScreenDPI: 96
  m_DefaultSpriteDPI: 96
  m_DynamicPixelsPerUnit: 1
  m_PresetInfoIsWorld: 0
--- !u!114 &{cv_gr}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {cv_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: dc42784cf147c0c48a680349fa168899, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_IgnoreReversedGraphics: 1
  m_BlockingObjects: 0
  m_BlockingMask:
    serializedVersion: 2
    m_Bits: 4294967295"""

    if mb_block:
        s += f"\n--- !u!114 &{cv_mb}\nMonoBehaviour:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: {cv_go}}}\n  m_Enabled: 1\n  m_EditorHideFlags: 0\n" + mb_block
    else:
        s += f"\n--- !u!114 &{cv_mb}\nMonoBehaviour:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: {cv_go}}}\n  m_Enabled: 1\n  m_EditorHideFlags: 0\n  m_Script: {{fileID: 0}}"

    return s, cv_rt, cv_mb, cid

def make_image(cid, name, parent_rt, amin, amax, pos, size, r, g, b, a, sprite_guid=None, raycast=1):
    go_id = cid; cid += 1
    rt_id = cid; cid += 1
    cr_id = cid; cid += 1
    im_id = cid; cid += 1

    sp_yaml = f"{{fileID: 21300000, guid: {sprite_guid}, type: 3}}" if sprite_guid else "{fileID: 0}"

    s = f"""--- !u!1 &{go_id}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {rt_id}}}
  - component: {{fileID: {cr_id}}}
  - component: {{fileID: {im_id}}}
  m_Layer: 5
  m_Name: {name}
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!224 &{rt_id}
RectTransform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children: []
  m_Father: {{fileID: {parent_rt}}}
  m_AnchorMin: {{x: {amin[0]}, y: {amin[1]}}}
  m_AnchorMax: {{x: {amax[0]}, y: {amax[1]}}}
  m_AnchoredPosition: {{x: {pos[0]}, y: {pos[1]}}}
  m_SizeDelta: {{x: {size[0]}, y: {size[1]}}}
  m_Pivot: {{x: 0.5, y: 0.5}}
--- !u!222 &{cr_id}
CanvasRenderer:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_CullTransparentMesh: 1
--- !u!114 &{im_id}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: fe87c0e1cc204ed48ad3b37840f39efc, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_Material: {{fileID: 0}}
  m_Color: {{r: {r}, g: {g}, b: {b}, a: {a}}}
  m_RaycastTarget: {raycast}
  m_RaycastPadding: {{x: 0, y: 0, z: 0, w: 0}}
  m_Maskable: 1
  m_OnCullStateChanged:
    m_PersistentCalls:
      m_Calls: []
  m_Sprite: {sp_yaml}
  m_Type: 0
  m_PreserveAspect: {1 if sprite_guid else 0}
  m_FillCenter: 1
  m_FillMethod: 4
  m_FillAmount: 1
  m_FillClockwise: 1
  m_FillOrigin: 0
  m_UseSpriteMesh: 0
  m_PixelsPerUnitMultiplier: 1"""
    return s, rt_id, im_id, cid

def make_text(cid, name, parent_rt, amin, amax, pos, size, text_str, font_size, r, g, b, align=4, bold=1):
    go_id = cid; cid += 1
    rt_id = cid; cid += 1
    cr_id = cid; cid += 1
    tx_id = cid; cid += 1

    clean_text = text_str.replace('"', '\\"')

    s = f"""--- !u!1 &{go_id}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {rt_id}}}
  - component: {{fileID: {cr_id}}}
  - component: {{fileID: {tx_id}}}
  m_Layer: 5
  m_Name: {name}
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!224 &{rt_id}
RectTransform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children: []
  m_Father: {{fileID: {parent_rt}}}
  m_AnchorMin: {{x: {amin[0]}, y: {amin[1]}}}
  m_AnchorMax: {{x: {amax[0]}, y: {amax[1]}}}
  m_AnchoredPosition: {{x: {pos[0]}, y: {pos[1]}}}
  m_SizeDelta: {{x: {size[0]}, y: {size[1]}}}
  m_Pivot: {{x: 0.5, y: 0.5}}
--- !u!222 &{cr_id}
CanvasRenderer:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_CullTransparentMesh: 1
--- !u!114 &{tx_id}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {go_id}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 5f7201a12d95ffc409449d95f23cf332, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_Material: {{fileID: 0}}
  m_Color: {{r: {r}, g: {g}, b: {b}, a: 1}}
  m_RaycastTarget: 0
  m_RaycastPadding: {{x: 0, y: 0, z: 0, w: 0}}
  m_Maskable: 1
  m_OnCullStateChanged:
    m_PersistentCalls:
      m_Calls: []
  m_FontData:
    m_Font: {{fileID: 10102, guid: 0000000000000000e000000000000000, type: 0}}
    m_FontSize: {font_size}
    m_FontStyle: {bold}
    m_BestFit: 0
    m_MinSize: 10
    m_MaxSize: 70
    m_Alignment: {align}
    m_AlignByGeometry: 0
    m_RichText: 1
    m_HorizontalOverflow: 0
    m_VerticalOverflow: 0
    m_LineSpacing: 1
  m_Text: "{clean_text}" """
    return s, rt_id, tx_id, cid

def make_button(cid, name, parent_rt, amin, amax, pos, size, label_str, br, bg, bb, font_size=20):
    btn_go = cid; cid += 1
    btn_rt = cid; cid += 1
    btn_cr = cid; cid += 1
    btn_im = cid; cid += 1
    btn_bt = cid; cid += 1

    lbl_go = cid; cid += 1
    lbl_rt = cid; cid += 1
    lbl_cr = cid; cid += 1
    lbl_tx = cid; cid += 1

    s = f"""--- !u!1 &{btn_go}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {btn_rt}}}
  - component: {{fileID: {btn_cr}}}
  - component: {{fileID: {btn_im}}}
  - component: {{fileID: {btn_bt}}}
  m_Layer: 5
  m_Name: {name}
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!224 &{btn_rt}
RectTransform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {btn_go}}}
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children:
  - {{fileID: {lbl_rt}}}
  m_Father: {{fileID: {parent_rt}}}
  m_AnchorMin: {{x: {amin[0]}, y: {amin[1]}}}
  m_AnchorMax: {{x: {amax[0]}, y: {amax[1]}}}
  m_AnchoredPosition: {{x: {pos[0]}, y: {pos[1]}}}
  m_SizeDelta: {{x: {size[0]}, y: {size[1]}}}
  m_Pivot: {{x: 0.5, y: 0.5}}
--- !u!222 &{btn_cr}
CanvasRenderer:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {btn_go}}}
  m_CullTransparentMesh: 1
--- !u!114 &{btn_im}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {btn_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: fe87c0e1cc204ed48ad3b37840f39efc, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_Material: {{fileID: 0}}
  m_Color: {{r: {br}, g: {bg}, b: {bb}, a: 1}}
  m_RaycastTarget: 1
  m_RaycastPadding: {{x: 0, y: 0, z: 0, w: 0}}
  m_Maskable: 1
  m_OnCullStateChanged:
    m_PersistentCalls:
      m_Calls: []
  m_Sprite: {{fileID: 0}}
  m_Type: 0
  m_PreserveAspect: 0
  m_FillCenter: 1
  m_FillMethod: 4
  m_FillAmount: 1
  m_FillClockwise: 1
  m_FillOrigin: 0
  m_UseSpriteMesh: 0
  m_PixelsPerUnitMultiplier: 1
--- !u!114 &{btn_bt}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {btn_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 4e29b1a8efbd4b44bb3f3716e73f07ff, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_Navigation:
    m_Mode: 3
    m_WrapAround: 0
    m_SelectOnUp: {{fileID: 0}}
    m_SelectOnDown: {{fileID: 0}}
    m_SelectOnLeft: {{fileID: 0}}
    m_SelectOnRight: {{fileID: 0}}
  m_Transition: 1
  m_Colors:
    m_NormalColor: {{r: {br}, g: {bg}, b: {bb}, a: 1}}
    m_HighlightedColor: {{r: {min(1.0, br*1.2)}, g: {min(1.0, bg*1.2)}, b: {min(1.0, bb*1.2)}, a: 1}}
    m_PressedColor: {{r: {br*0.7}, g: {bg*0.7}, b: {bb*0.7}, a: 1}}
    m_SelectedColor: {{r: {min(1.0, br*1.2)}, g: {min(1.0, bg*1.2)}, b: {min(1.0, bb*1.2)}, a: 1}}
    m_DisabledColor: {{r: 0.5, g: 0.5, b: 0.5, a: 0.5}}
    m_ColorMultiplier: 1
    m_FadeDuration: 0.1
  m_SpriteState:
    m_HighlightedSprite: {{fileID: 0}}
    m_PressedSprite: {{fileID: 0}}
    m_SelectedSprite: {{fileID: 0}}
    m_DisabledSprite: {{fileID: 0}}
  m_AnimationTriggers:
    m_NormalTrigger: Normal
    m_HighlightedTrigger: Highlighted
    m_PressedTrigger: Pressed
    m_SelectedTrigger: Selected
    m_DisabledTrigger: Disabled
  m_Interactable: 1
  m_TargetGraphic: {{fileID: {btn_im}}}
  m_OnClick:
    m_PersistentCalls:
      m_Calls: []
--- !u!1 &{lbl_go}
GameObject:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  serializedVersion: 6
  m_Component:
  - component: {{fileID: {lbl_rt}}}
  - component: {{fileID: {lbl_cr}}}
  - component: {{fileID: {lbl_tx}}}
  m_Layer: 5
  m_Name: Text
  m_TagString: Untagged
  m_Icon: {{fileID: 0}}
  m_NavMeshLayer: 0
  m_StaticEditorFlags: 0
  m_IsActive: 1
--- !u!224 &{lbl_rt}
RectTransform:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {lbl_go}}}
  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}
  m_LocalPosition: {{x: 0, y: 0, z: 0}}
  m_LocalScale: {{x: 1, y: 1, z: 1}}
  m_ConstrainProportionsScale: 0
  m_Children: []
  m_Father: {{fileID: {btn_rt}}}
  m_AnchorMin: {{x: 0, y: 0}}
  m_AnchorMax: {{x: 1, y: 1}}
  m_AnchoredPosition: {{x: 0, y: 0}}
  m_SizeDelta: {{x: 0, y: 0}}
  m_Pivot: {{x: 0.5, y: 0.5}}
--- !u!222 &{lbl_cr}
CanvasRenderer:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {lbl_go}}}
  m_CullTransparentMesh: 1
--- !u!114 &{lbl_tx}
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {{fileID: 0}}
  m_PrefabInstance: {{fileID: 0}}
  m_PrefabAsset: {{fileID: 0}}
  m_GameObject: {{fileID: {lbl_go}}}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {{fileID: 11500000, guid: 5f7201a12d95ffc409449d95f23cf332, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  m_Material: {{fileID: 0}}
  m_Color: {{r: 1, g: 1, b: 1, a: 1}}
  m_RaycastTarget: 0
  m_RaycastPadding: {{x: 0, y: 0, z: 0, w: 0}}
  m_Maskable: 1
  m_OnCullStateChanged:
    m_PersistentCalls:
      m_Calls: []
  m_FontData:
    m_Font: {{fileID: 10102, guid: 0000000000000000e000000000000000, type: 0}}
    m_FontSize: {font_size}
    m_FontStyle: 1
    m_BestFit: 0
    m_MinSize: 10
    m_MaxSize: 40
    m_Alignment: 4
    m_AlignByGeometry: 0
    m_RichText: 1
    m_HorizontalOverflow: 0
    m_VerticalOverflow: 0
    m_LineSpacing: 1
  m_Text: "{label_str}" """
    return s, btn_rt, btn_bt, cid

# -------------------------------------------------------------
# Generate 1: SplashScene.unity
# -------------------------------------------------------------
def run_splash():
    cam_es, cid = make_cam_es(100)
    bg_s, bg_rt, bg_im, cid = make_image(cid, "Background", 0, (0,0), (1,1), (0,0), (0,0), 0.04, 0.03, 0.08, 1)
    logo_s, logo_rt, logo_im, cid = make_image(cid, "DragonBallLogo", 0, (0.5, 0.68), (0.5, 0.68), (0,0), (160,160), 1, 1, 1, 1, "dcca930b11c3ade4aaac93235de3bfc0", 0)
    t1_s, t1_rt, t1_tx, cid = make_text(cid, "TitleMain", 0, (0.5, 0.50), (0.5, 0.50), (0,0), (900,90), "DRAGON BALL", 64, 1, 0.85, 0.15)
    t2_s, t2_rt, t2_tx, cid = make_text(cid, "Subtitle", 0, (0.5, 0.42), (0.5, 0.42), (0,0), (800,40), "★ SUPER FIGHTING CLASH ★", 24, 0, 0.9, 1)
    pm_s, pm_rt, pm_tx, cid = make_text(cid, "PromptText", 0, (0.5, 0.22), (0.5, 0.22), (0,0), (800,40), "— PRESS ANY KEY OR CLICK TO START —", 24, 1, 0.95, 0.7)
    btn_s, btn_rt, btn_bt, cid = make_button(cid, "ClickBtn", 0, (0.5, 0.5), (0.5, 0.5), (0,0), (1920, 1080), "", 0, 0, 0, 0)
    
    cv_children = [bg_rt, logo_rt, t1_rt, t2_rt, pm_rt, btn_rt]
    mb_block = f"""  m_Script: {{fileID: 11500000, guid: a626b14efeba71d49aba400c7831061e, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  startButton: {{fileID: {btn_bt}}}
  promptText: {{fileID: {pm_tx}}}
  titleText: {{fileID: {t1_tx}}}"""
    
    cv_s, cv_rt, cv_mb, cid = make_canvas(cid, "SplashCanvas", cv_children, mb_block)

    all_parts = [bg_s, logo_s, t1_s, t2_s, pm_s, btn_s]
    content = header + cam_es + "\n" + cv_s + "\n" + "\n".join(all_parts)
    content = content.replace("m_Father: {fileID: 0}", f"m_Father: {{fileID: {cv_rt}}}")
    content = content.replace(f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 100}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: {cv_rt}}}", f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 100}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: 0}}")
    content = content.replace(f"m_Father: {{fileID: {cv_rt}}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &106", f"m_Father: {{fileID: 0}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &106")

    with open("Assets/Scenes/SplashScene.unity", "w", encoding="utf-8") as f:
        f.write(content)
    print("Generated SplashScene.unity")

# -------------------------------------------------------------
# Generate 2: MainMenuScene.unity
# -------------------------------------------------------------
def run_main_menu():
    cam_es, cid = make_cam_es(200)

    bg_s, bg_rt, bg_im, cid = make_image(cid, "Background", 0, (0,0), (1,1), (0,0), (0,0), 0.35, 0.4, 0.55, 1, "a259f35f8b995f34d94ffaa7978854e4")
    vig_s, vig_rt, vig_im, cid = make_image(cid, "Vignette", 0, (0,0), (1,1), (0,0), (0,0), 0, 0, 0, 0.5)

    t1_s, t1_rt, t1_tx, cid = make_text(cid, "Title", 0, (0.5, 0.85), (0.5, 0.85), (0, 0), (900, 80), "DRAGON BALL: SUPER CLASH", 52, 1, 0.85, 0.15)
    t2_s, t2_rt, t2_tx, cid = make_text(cid, "Sub", 0, (0.5, 0.77), (0.5, 0.77), (0, 0), (800, 40), "★ SELECT GAME MODE ★", 22, 0, 0.9, 1)

    b1_s, b1_rt, b1_bt, cid = make_button(cid, "ArcadeBtn", 0, (0.5, 0.60), (0.5, 0.60), (0, 0), (420, 56), "⚔️  ARCADE / VERSUS", 0.9, 0.55, 0.1, 24)
    b2_s, b2_rt, b2_bt, cid = make_button(cid, "TrainingBtn", 0, (0.5, 0.49), (0.5, 0.49), (0, 0), (420, 56), "🥋  TRAINING MODE", 0.15, 0.65, 0.9, 24)
    b3_s, b3_rt, b3_bt, cid = make_button(cid, "SettingsBtn", 0, (0.5, 0.38), (0.5, 0.38), (0, 0), (420, 56), "⚙️  SETTINGS & OPTIONS", 0.6, 0.35, 0.8, 24)
    b4_s, b4_rt, b4_bt, cid = make_button(cid, "QuitBtn", 0, (0.5, 0.27), (0.5, 0.27), (0, 0), (420, 56), "🚪  QUIT GAME", 0.85, 0.25, 0.25, 24)

    foot_s, foot_rt, foot_tx, cid = make_text(cid, "Footer", 0, (0.5, 0.06), (0.5, 0.06), (0, 0), (900, 30), "[Click or Tab/Enter] Select Menu  •  [ESC] Exit  •  60 FPS Locked", 15, 0.7, 0.7, 0.8, 4, 0)

    cv_children = [bg_rt, vig_rt, t1_rt, t2_rt, b1_rt, b2_rt, b3_rt, b4_rt, foot_rt]
    mb_block = f"""  m_Script: {{fileID: 11500000, guid: fb60b10fbe275ac4ab98be045dcb805e, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  arcadeButton: {{fileID: {b1_bt}}}
  trainingButton: {{fileID: {b2_bt}}}
  settingsButton: {{fileID: {b3_bt}}}
  quitButton: {{fileID: {b4_bt}}}
  settingsPanel: {{fileID: 0}}
  bgmSlider: {{fileID: 0}}
  sfxSlider: {{fileID: 0}}
  fpsToggle: {{fileID: 0}}
  fullscreenToggle: {{fileID: 0}}
  closeSettingsButton: {{fileID: 0}}"""

    cv_s, cv_rt, cv_mb, cid = make_canvas(cid, "MainMenuCanvas", cv_children, mb_block)

    all_parts = [bg_s, vig_s, t1_s, t2_s, b1_s, b2_s, b3_s, b4_s, foot_s]
    content = header + cam_es + "\n" + cv_s + "\n" + "\n".join(all_parts)
    content = content.replace("m_Father: {fileID: 0}", f"m_Father: {{fileID: {cv_rt}}}")
    content = content.replace(f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 200}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: {cv_rt}}}", f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 200}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: 0}}")
    content = content.replace(f"m_Father: {{fileID: {cv_rt}}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &206", f"m_Father: {{fileID: 0}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &206")

    with open("Assets/Scenes/MainMenuScene.unity", "w", encoding="utf-8") as f:
        f.write(content)
    print("Generated MainMenuScene.unity")

# -------------------------------------------------------------
# Generate 3: CharacterSelectScene.unity
# -------------------------------------------------------------
def run_char_select():
    cam_es, cid = make_cam_es(300)

    bg_s, bg_rt, bg_im, cid = make_image(cid, "Background", 0, (0,0), (1,1), (0,0), (0,0), 0.25, 0.28, 0.45, 1, "a259f35f8b995f34d94ffaa7978854e4")
    head_s, head_rt, head_tx, cid = make_text(cid, "HeaderTitle", 0, (0.5, 0.92), (0.5, 0.92), (0, 0), (800, 50), "CHOOSE YOUR FIGHTER", 38, 1, 0.85, 0.15)

    p1b_s, p1b_rt, p1b_im, cid = make_image(cid, "P1Box", 0, (0.03, 0.16), (0.28, 0.86), (0, 0), (0, 0), 0.1, 0.1, 0.16, 0.9)
    p1h_s, p1h_rt, p1h_tx, cid = make_text(cid, "P1Header", p1b_rt, (0.5, 0.93), (0.5, 0.93), (0, 0), (280, 35), "PLAYER 1", 22, 1, 0.25, 0.25)
    p1p_s, p1p_rt, p1p_im, cid = make_image(cid, "P1Portrait", p1b_rt, (0.5, 0.68), (0.5, 0.68), (0, 0), (160, 160), 1, 1, 1, 1, "78f58893d8ed65341bfe0eeb77ceba3e", 0)
    p1n_s, p1n_rt, p1n_tx, cid = make_text(cid, "P1Name", p1b_rt, (0.5, 0.47), (0.5, 0.47), (0, 0), (300, 35), "VEGETA (SSJ)", 22, 1, 0.9, 0.2)
    p1t_s, p1t_rt, p1t_tx, cid = make_text(cid, "P1Title", p1b_rt, (0.5, 0.41), (0.5, 0.41), (0, 0), (300, 25), "Prince of All Saiyans", 14, 0.8, 0.8, 0.9)
    p1s_s, p1s_rt, p1s_tx, cid = make_text(cid, "P1Stats", p1b_rt, (0.5, 0.32), (0.5, 0.32), (0, 0), (300, 30), "POWER: 90 • SPEED: 92 • KI: 95", 13, 0.9, 0.9, 0.9)
    p1m_s, p1m_rt, p1m_tx, cid = make_text(cid, "P1Moves", p1b_rt, (0.5, 0.20), (0.5, 0.20), (0, 0), (280, 50), "• Final Flash\n• Galick Gun\n• Big Bang Attack", 12, 0.7, 0.85, 1, 3)
    p1g_s, p1g_rt, p1g_tx, cid = make_text(cid, "P1Badge", p1b_rt, (0.5, 0.06), (0.5, 0.06), (0, 0), (200, 30), "[ READY ]", 18, 0.3, 1, 0.4)

    p2b_s, p2b_rt, p2b_im, cid = make_image(cid, "P2Box", 0, (0.72, 0.16), (0.97, 0.86), (0, 0), (0, 0), 0.1, 0.1, 0.16, 0.9)
    p2h_s, p2h_rt, p2h_tx, cid = make_text(cid, "P2Header", p2b_rt, (0.5, 0.93), (0.5, 0.93), (0, 0), (280, 35), "PLAYER 2 / CPU", 22, 0.25, 0.5, 1)
    p2p_s, p2p_rt, p2p_im, cid = make_image(cid, "P2Portrait", p2b_rt, (0.5, 0.68), (0.5, 0.68), (0, 0), (160, 160), 1, 1, 1, 1, "78f58893d8ed65341bfe0eeb77ceba3e", 0)
    p2n_s, p2n_rt, p2n_tx, cid = make_text(cid, "P2Name", p2b_rt, (0.5, 0.47), (0.5, 0.47), (0, 0), (300, 35), "VEGETA (SSJ)", 22, 1, 0.9, 0.2)
    p2t_s, p2t_rt, p2t_tx, cid = make_text(cid, "P2Title", p2b_rt, (0.5, 0.41), (0.5, 0.41), (0, 0), (300, 25), "Prince of All Saiyans", 14, 0.8, 0.8, 0.9)
    p2s_s, p2s_rt, p2s_tx, cid = make_text(cid, "P2Stats", p2b_rt, (0.5, 0.32), (0.5, 0.32), (0, 0), (300, 30), "POWER: 90 • SPEED: 92 • KI: 95", 13, 0.9, 0.9, 0.9)
    p2m_s, p2m_rt, p2m_tx, cid = make_text(cid, "P2Moves", p2b_rt, (0.5, 0.20), (0.5, 0.20), (0, 0), (280, 50), "• Final Flash\n• Galick Gun\n• Big Bang Attack", 12, 0.7, 0.85, 1, 3)
    p2g_s, p2g_rt, p2g_tx, cid = make_text(cid, "P2Badge", p2b_rt, (0.5, 0.06), (0.5, 0.06), (0, 0), (200, 30), "[ READY ]", 18, 0.3, 1, 0.4)

    rost_s, rost_rt, rost_im, cid = make_image(cid, "RosterBox", 0, (0.31, 0.24), (0.69, 0.84), (0, 0), (0, 0), 0.08, 0.08, 0.14, 0.92)
    rtit_s, rtit_rt, rtit_tx, cid = make_text(cid, "RosterTitle", rost_rt, (0.5, 0.92), (0.5, 0.92), (0, 0), (400, 30), "FIGHTER ROSTER", 20, 0.9, 0.9, 1)

    btn_slots = []
    fighters = ["VEGETA", "GOKU", "TRUNKS", "GOHAN", "CELL", "FRIEZA", "A-17", "A-18"]
    colors = [(0.9, 0.75, 0.1), (1, 0.5, 0.1), (0.4, 0.7, 1), (1, 0.9, 0.2), (0.2, 0.8, 0.3), (0.8, 0.3, 1), (0.1, 0.8, 0.9), (1, 0.4, 0.7)]
    grid_positions = [
        (-120, 60), (0, 60), (120, 60),
        (-120, -10), (0, -10), (120, -10),
        (-60, -80), (60, -80)
    ]
    for idx in range(8):
        pos = grid_positions[idx]
        col = colors[idx]
        b_s, b_rt, b_bt, cid = make_button(cid, f"RosterBtn_{idx}", rost_rt, (0.5, 0.5), (0.5, 0.5), pos, (105, 55), fighters[idx], col[0], col[1], col[2], 14)
        btn_slots.append(b_s)

    sbtn_s, sbtn_rt, sbtn_bt, cid = make_button(cid, "StartBattleBtn", 0, (0.5, 0.08), (0.5, 0.08), (0, 0), (360, 54), "⚡ START BATTLE! ⚡", 1, 0.6, 0.1, 26)
    bbtn_s, bbtn_rt, bbtn_bt, cid = make_button(cid, "BackBtn", 0, (0.15, 0.08), (0.15, 0.08), (0, 0), (220, 44), "◀ BACK TO MENU", 0.5, 0.5, 0.6, 18)

    cv_children = [bg_rt, head_rt, p1b_rt, p2b_rt, rost_rt, sbtn_rt, bbtn_rt]
    mb_block = f"""  m_Script: {{fileID: 11500000, guid: 2b265b3b5ef486a45b8ac7d3fffa1ee3, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  p1NameText: {{fileID: {p1n_tx}}}
  p1TitleText: {{fileID: {p1t_tx}}}
  p1PortraitImage: {{fileID: {p1p_im}}}
  p1StatsText: {{fileID: {p1s_tx}}}
  p1SpecialsText: {{fileID: {p1m_tx}}}
  p1StatusBadge: {{fileID: {p1g_tx}}}
  p2NameText: {{fileID: {p2n_tx}}}
  p2TitleText: {{fileID: {p2t_tx}}}
  p2PortraitImage: {{fileID: {p2p_im}}}
  p2StatsText: {{fileID: {p2s_tx}}}
  p2SpecialsText: {{fileID: {p2m_tx}}}
  p2StatusBadge: {{fileID: {p2g_tx}}}
  startBattleButton: {{fileID: {sbtn_bt}}}
  backButton: {{fileID: {bbtn_bt}}}"""

    cv_s, cv_rt, cv_mb, cid = make_canvas(cid, "CharacterSelectCanvas", cv_children, mb_block)

    all_parts = [bg_s, head_s, p1b_s, p1h_s, p1p_s, p1n_s, p1t_s, p1s_s, p1m_s, p1g_s,
                 p2b_s, p2h_s, p2p_s, p2n_s, p2t_s, p2s_s, p2m_s, p2g_s,
                 rost_s, rtit_s] + btn_slots + [sbtn_s, bbtn_s]

    content = header + cam_es + "\n" + cv_s + "\n" + "\n".join(all_parts)
    content = content.replace("m_Father: {fileID: 0}", f"m_Father: {{fileID: {cv_rt}}}")
    content = content.replace(f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 300}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: {cv_rt}}}", f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 300}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: 0}}")
    content = content.replace(f"m_Father: {{fileID: {cv_rt}}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &306", f"m_Father: {{fileID: 0}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &306")

    with open("Assets/Scenes/CharacterSelectScene.unity", "w", encoding="utf-8") as f:
        f.write(content)
    print("Generated CharacterSelectScene.unity")

# -------------------------------------------------------------
# Generate 4: PostMatchScene.unity
# -------------------------------------------------------------
def run_post_match():
    cam_es, cid = make_cam_es(400)

    bg_s, bg_rt, bg_im, cid = make_image(cid, "Background", 0, (0,0), (1,1), (0,0), (0,0), 0.2, 0.22, 0.35, 1, "a259f35f8b995f34d94ffaa7978854e4")
    vig_s, vig_rt, vig_im, cid = make_image(cid, "Vignette", 0, (0,0), (1,1), (0,0), (0,0), 0, 0, 0, 0.6)

    head_s, head_rt, head_tx, cid = make_text(cid, "VictoryTitle", 0, (0.5, 0.88), (0.5, 0.88), (0, 0), (800, 70), "VICTORY!", 58, 1, 0.85, 0.15)
    cond_s, cond_rt, cond_tx, cid = make_text(cid, "VictoryCondition", 0, (0.5, 0.80), (0.5, 0.80), (0, 0), (600, 35), "DECISIVE K.O. KNOCKOUT!", 22, 0, 0.9, 1)

    card_s, card_rt, card_im, cid = make_image(cid, "WinnerCard", 0, (0.26, 0.26), (0.74, 0.74), (0, 0), (0, 0), 0.1, 0.1, 0.18, 0.92)

    port_s, port_rt, port_im, cid = make_image(cid, "WinnerPortrait", card_rt, (0.5, 0.68), (0.5, 0.68), (0, 0), (170, 170), 1, 1, 1, 1, "78f58893d8ed65341bfe0eeb77ceba3e", 0)
    name_s, name_rt, name_tx, cid = make_text(cid, "WinnerName", card_rt, (0.5, 0.40), (0.5, 0.40), (0, 0), (600, 40), "PLAYER 1 • VEGETA (SUPER SAIYAN)", 26, 1, 0.9, 0.2)
    rank_s, rank_rt, rank_tx, cid = make_text(cid, "RankBadge", card_rt, (0.5, 0.28), (0.5, 0.28), (0, 0), (400, 35), "★ S RANK - SAIYAN PRIDE! ★", 20, 1, 0.6, 0.1)

    hp_s, hp_rt, hp_tx, cid = make_text(cid, "HpStat", card_rt, (0.2, 0.12), (0.2, 0.12), (0, 0), (220, 30), "HEALTH: 85 HP", 16, 0.4, 1, 0.5)
    time_s, time_rt, time_tx, cid = make_text(cid, "TimeStat", card_rt, (0.5, 0.12), (0.5, 0.12), (0, 0), (220, 30), "TIME: 38.5s", 16, 1, 1, 1)
    comb_s, comb_rt, comb_tx, cid = make_text(cid, "ComboStat", card_rt, (0.8, 0.12), (0.8, 0.12), (0, 0), (220, 30), "MAX COMBO: 12 HITS", 16, 1, 0.8, 0.3)

    b1_s, b1_rt, b1_bt, cid = make_button(cid, "RematchBtn", 0, (0.25, 0.12), (0.25, 0.12), (0, 0), (260, 52), "🔄 REMATCH (R)", 1, 0.55, 0.1, 20)
    b2_s, b2_rt, b2_bt, cid = make_button(cid, "SelectBtn", 0, (0.5, 0.12), (0.5, 0.12), (0, 0), (260, 52), "👥 FIGHTERS (C)", 0.2, 0.6, 1, 20)
    b3_s, b3_rt, b3_bt, cid = make_button(cid, "MenuBtn", 0, (0.75, 0.12), (0.75, 0.12), (0, 0), (260, 52), "🏠 MAIN MENU (M)", 0.5, 0.5, 0.6, 20)

    cv_children = [bg_rt, vig_rt, head_rt, cond_rt, card_rt, b1_rt, b2_rt, b3_rt]
    mb_block = f"""  m_Script: {{fileID: 11500000, guid: 8e19afa0f1898834c85815d51e009024, type: 3}}
  m_Name: 
  m_EditorClassIdentifier: 
  winnerTitleText: {{fileID: {head_tx}}}
  winnerNameText: {{fileID: {name_tx}}}
  winnerPortraitImage: {{fileID: {port_im}}}
  victoryConditionText: {{fileID: {cond_tx}}}
  performanceRankText: {{fileID: {rank_tx}}}
  matchDurationText: {{fileID: {time_tx}}}
  remainingHpText: {{fileID: {hp_tx}}}
  comboScoreText: {{fileID: {comb_tx}}}
  rematchButton: {{fileID: {b1_bt}}}
  selectFighterButton: {{fileID: {b2_bt}}}
  mainMenuButton: {{fileID: {b3_bt}}}"""

    cv_s, cv_rt, cv_mb, cid = make_canvas(cid, "PostMatchCanvas", cv_children, mb_block)

    all_parts = [bg_s, vig_s, head_s, cond_s, card_s, port_s, name_s, rank_s, hp_s, time_s, comb_s, b1_s, b2_s, b3_s]
    content = header + cam_es + "\n" + cv_s + "\n" + "\n".join(all_parts)
    content = content.replace("m_Father: {fileID: 0}", f"m_Father: {{fileID: {cv_rt}}}")
    content = content.replace(f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 400}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: {cv_rt}}}", f"Transform:\n  m_ObjectHideFlags: 0\n  m_CorrespondingSourceObject: {{fileID: 0}}\n  m_PrefabInstance: {{fileID: 0}}\n  m_PrefabAsset: {{fileID: 0}}\n  m_GameObject: {{fileID: 400}}\n  serializedVersion: 2\n  m_LocalRotation: {{x: 0, y: 0, z: 0, w: 1}}\n  m_LocalPosition: {{x: 0, y: 0, z: -10}}\n  m_LocalScale: {{x: 1, y: 1, z: 1}}\n  m_ConstrainProportionsScale: 0\n  m_Children: []\n  m_Father: {{fileID: 0}}")
    content = content.replace(f"m_Father: {{fileID: {cv_rt}}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &406", f"m_Father: {{fileID: 0}}\n  m_LocalEulerAnglesHint: {{x: 0, y: 0, z: 0}}\n--- !u!114 &406")

    with open("Assets/Scenes/PostMatchScene.unity", "w", encoding="utf-8") as f:
        f.write(content)
    print("Generated PostMatchScene.unity")

run_splash()
run_main_menu()
run_char_select()
run_post_match()
print("All 4 scenes generated successfully!")
