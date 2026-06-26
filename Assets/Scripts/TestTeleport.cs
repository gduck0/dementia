using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class TestTeleport : MonoBehaviour
{
    public Transform targetPoint;

    private XROrigin xrOrigin;
    private InputActionManager inputActionManager;
    void Awake()
    {
        xrOrigin = FindFirstObjectByType<XROrigin>();
        inputActionManager = FindFirstObjectByType<InputActionManager>();
    }
    void Update()
    {
        // 키보드 Z 누르면 텔레포트
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            StartCoroutine(StartTimelineSequence());
        }
    }

    private IEnumerator StartTimelineSequence()
    {
        DisableAllInput();

        yield return new WaitForEndOfFrame();

        TeleportHelper.TeleportPlayer(targetPoint.position, targetPoint.rotation);

    }

    private void DisableAllInput()
    {
        if (inputActionManager == null) return;

        // InputActionManager가 관리하는 모든 Action Asset 비활성화
        foreach (var asset in inputActionManager.actionAssets)
        {
            asset.Disable();     // ⭐ 이동/회전/시선 입력 완전히 차단됨
        }

    }

}
