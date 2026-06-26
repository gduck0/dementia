using Unity.XR.CoreUtils;
using UnityEngine;

public class TeleportHelper : MonoBehaviour
{
    public static void TeleportPlayer(Vector3 pos, Quaternion rot)
    {
        XROrigin origin = Object.FindFirstObjectByType<XROrigin>();
        if (origin == null) return;

        //// 1️⃣ Yaw만 회전 적용
        //Vector3 currentEuler = origin.transform.eulerAngles;
        //origin.transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);

        // 1️⃣ 타겟의 Yaw(= forward 방향)로 XR Origin 회전
        float targetYaw = rot.eulerAngles.y;
        origin.transform.rotation = Quaternion.Euler(0, targetYaw, 0);

        // 2️⃣ XR Origin을 카메라가 pos로 오도록 이동시키기
        origin.MoveCameraToWorldLocation(pos);

    }
}
