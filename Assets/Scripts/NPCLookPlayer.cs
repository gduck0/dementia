using UnityEngine;
using UnityEngine.Audio;

public class NPCLookPlayer : MonoBehaviour
{
    [Header("Player")]
    public Transform player;                 // VR 카메라(MainCamera)
    public float rotateSpeed = 5f;
    public float maxDistance = 20f;          // 너무 멀면 안 바라봄

    [Header("NPC Dialogue")]
    public AudioSource npcAudioSource;       // NPC에 붙은 AudioSource
    public AudioClip dialogueClip;           // NPC 대사 오디오
    public float talkDistance = 20f;          // 말할 거리
    public float lookAngleLimit = 90f;       // 플레이어를 보고 있다고 판단할 각도
    private bool hasTalked = false;          // 한 번만 말하게

    [Header("Quest Dialogue (퀘스트용 대사들)")]
    public AudioClip[] questDialogueClips;

    void Update()
    {
        if (player == null) return;

        // 거리 체크
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > maxDistance) return;

        // 플레이어 바라보기
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        // 이미 말했으면 멈춤
        if (hasTalked) return;

        // 플레이어가 일정 거리 안에 있는가?
        if (distance > talkDistance) return;

        // NPC가 플레이어쪽을 향하는 각도 확인
        float angle = Vector3.Angle(transform.forward, player.position - transform.position);
        if (angle < lookAngleLimit)
        {
            PlayDialogue();
        }
    }

    void PlayDialogue()
    {
        if (npcAudioSource != null && dialogueClip != null)
        {
            npcAudioSource.PlayOneShot(dialogueClip);
        }

        hasTalked = true; // 한 번만 말하기
    }

    public void PlayFisrtDialogueClip()
    {
        if (npcAudioSource.isPlaying) npcAudioSource.Stop();
        npcAudioSource.PlayOneShot(questDialogueClips[0]);
    }

    public bool IsPlaying
    {
        get { return npcAudioSource != null && npcAudioSource.isPlaying; }
    }

}
