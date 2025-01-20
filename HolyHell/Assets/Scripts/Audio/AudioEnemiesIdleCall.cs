using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnemiesIdleCall : MonoBehaviour
{

    public AudioEnemies audioEnemies;

    void playSoundMaybe(int index){
        if (Random.value > 0.7f)
        {
            audioEnemies.playThisSound(index);
        }
    }

    void playSound(int index){
        audioEnemies.playThisSound(index);
    }
}
