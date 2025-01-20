using UnityEngine;

public class AudioGeneralOneClip : MonoBehaviour
{
    public AudioClip[] audioClips;

    public AudioSource audioSource;

    void Awake()
    {

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }


    public void PlaySpecificAudio(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            PlayAudio(audioClips, clipIndex);
        }
        else
        {
            Debug.LogWarning("Clip index out of bounds: Please select a valid clip index.");
        }
    }


    public void PlayRandomAudio()
    {
        if (audioClips.Length > 0)
        {
            int randomClipIndex = Random.Range(0, audioClips.Length);
            PlayAudio(audioClips, randomClipIndex);
        }
        else
        {
            Debug.LogWarning("Audio clip array is empty: Please add audio clips.");
        }
    }


    private void PlayAudio(AudioClip[] clips, int index)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);  
        audioSource.PlayOneShot(clips[index]);  
    }
}