using UnityEngine;

public class LivingRoomTrigger : MonoBehaviour
{
    public QuestManager gameManager; // Inspector 연결
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 거실 진입 이벤트 실행
        gameManager.OnEnterLivingRoom();

        // 한 번만 실행되도록 비활성화
        gameObject.SetActive(false);
    }
}
