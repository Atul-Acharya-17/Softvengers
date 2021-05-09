using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public ParticleSystem leftWing;
    public ParticleSystem rightWing;
    public AudioSource shootAudio;

    bool missileReleased = false;

    void Update()
    {
        if (missileReleased)
        {
            ShootEffect();
        }
    }
    void ShootEffect()
    {
        leftWing.Play();
        rightWing.Play();
        shootAudio.Play();
    }

    public void releaseMissile()
    {
        missileReleased = true;
    }

    public void stopMissile()
    {
        missileReleased = false;
    }
}
