using UnityEngine;

public class AudioEnemies : MonoBehaviour
{
    public AudioClip[] audioClips_idle, audioClips_attack, audioClps_hit, audioClips_death;
    public AudioSource audioSource;

    public void playThisSound(int index){
        switch (index)
        {
            case 0: //idle
                PlayRandomAudio(audioClips_idle);
                break;
            case 1: //attack
                PlayRandomAudio(audioClips_attack);
                break;
            case 2: //hit
                PlayRandomAudio(audioClps_hit);
                break;
            case 3: //death
                PlayRandomAudio(audioClips_death);
                break;
            default:
                break;
        }
    }
    private void PlayRandomAudio(AudioClip[] audioClips)
    {
        if (audioClips.Length > 0)
        {
            int randomClipIndex = Random.Range(0, audioClips.Length);
            PlayAudio(audioClips, randomClipIndex);
        }
    }
    private void PlayAudio(AudioClip[] clips, int index)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);  
        audioSource.PlayOneShot(clips[index]);  
    }
}
