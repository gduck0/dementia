using UnityEngine;
using UnityEngine.Audio;

public class PhoneRingPlayer : MonoBehaviour
{
    public AudioSource phoneAudioSource;   // ÀüÈ­±â AudioSource
    public AudioClip ringClip;
    public NPCLookPlayer sonNPC;

    public void PlayRing()
    {
        if (!phoneAudioSource.isPlaying)
        {
            phoneAudioSource.loop = true;
            phoneAudioSource.clip = ringClip;
            phoneAudioSource.Play();
        }
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlayVoice: clip is null");
            return;
        }

        if (phoneAudioSource.isPlaying) phoneAudioSource.Stop();
        phoneAudioSource.PlayOneShot(clip);
    }

    public void StopRing()
    {
        phoneAudioSource.Stop();
    }

    public bool IsPlaying
    {
        get { return phoneAudioSource != null && phoneAudioSource.isPlaying; }
    }
}
