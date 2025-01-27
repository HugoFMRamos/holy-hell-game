using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemCall : MonoBehaviour
{
    public ParticleSystem ps;

    public void PlayParticleSystem() {
        ps.Play();
    }

    public void Stop() {
        ps.Stop();
    }
}
