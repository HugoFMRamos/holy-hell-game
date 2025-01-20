using UnityEngine;

public class AudioGeneralOneClip : MonoBehaviour
{
    public AudioClip[] audioClips;

    public AudioSource audioSource;

    public void PlaySpecificAudio(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            PlayAudio(audioClips, clipIndex);
        }
    }

    public void PlayRandomAudio()
    {
        if (audioClips.Length > 0)
        {
            int randomClipIndex = Random.Range(0, audioClips.Length);
            PlayAudio(audioClips, randomClipIndex);
        }
    }


    private void PlayAudio(AudioClip[] clips, int index)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);  
        audioSource.PlayOneShot(clips[index]);  
    }
}