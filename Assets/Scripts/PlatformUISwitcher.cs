using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.XR.Interaction.Toolkit.UI; // 오류 시: UnityEngine.XR.Interaction.Toolkit 사용

// Management 네임스페이스 제거됨
// using UnityEngine.XR.Management; 

public class PlatformUISwitcher : MonoBehaviour
{
    public Canvas mainCanvas;
    public InputSystemUIInputModule desktopModule;
    public XRUIInputModule vrModule;

    void Start()
    {
        // 복잡한 로더 체크 대신, 현재 XR 디스플레이 시스템이 실행 중인지 확인하는 간단한 방식입니다.
        bool isVREnabled = UnityEngine.XR.XRSettings.isDeviceActive;

        if (isVREnabled)
        {
            // VR 모드 
            desktopModule.enabled = false;
            vrModule.enabled = true;
            mainCanvas.renderMode = RenderMode.WorldSpace;

            if (Camera.main != null)
            {
                mainCanvas.worldCamera = Camera.main;
            }

            Debug.Log("모드 전환: VR (World Space)");
        }
        else
        {
            // 데스크탑
            desktopModule.enabled = true;
            vrModule.enabled = false;
            mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            Debug.Log("모드 전환: 데스크탑 (Screen Space)");
        }
    }
}