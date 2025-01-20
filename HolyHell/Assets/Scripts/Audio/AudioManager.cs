using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioGeneralOneClip[] audioGenerals;

    public void PlaySelectedGeneralRandom(int index){
        audioGenerals[index].PlayRandomAudio();
    }
}
