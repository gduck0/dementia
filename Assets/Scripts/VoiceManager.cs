using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager instance;

    public AudioSource audioSource;
    public AudioClip[] voiceClips; //플레이어 음성
    public AudioClip RingClip; //전화벨 소리
    void Awake()
    {
        instance = this;
    }

    public void PlayVoice(int index) //음성 재생
    {
        audioSource.PlayOneShot(voiceClips[index]);
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlayVoice: clip is null");
            return;
        }

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }

    public void PlayRing() //벨소리 재생
    {
        audioSource.PlayOneShot(RingClip);
    }

    public bool IsPlaying
    {
        get { return audioSource != null && audioSource.isPlaying; }
    }
}
