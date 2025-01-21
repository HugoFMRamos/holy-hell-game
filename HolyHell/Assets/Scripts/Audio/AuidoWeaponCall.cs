using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWeaponCall : MonoBehaviour
{
    public AudioGeneralOneClip audioGeneralOneClip;
    

    public void FireRandom(){
        audioGeneralOneClip.PlayRandomAudio();
    } 

    public void FireSpecific(int index){
        audioGeneralOneClip.PlaySpecificAudio(index);
    }
}
