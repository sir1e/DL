using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlletSkeleton : MonoBehaviour
{
    public Skeleton skeleton;
    public AudioSource deathSound;
    private MusicController controller;
   

    private void Awake()
    {
        skeleton = GetComponent<Skeleton>();
    }


    void Update()
    {
        if (controller.isBattleMusicPlaying == false && skeleton.GetHit)
        {
            controller.peaceMusic.Stop();
            controller.battleMusic.Play();
            controller.isBattleMusicPlaying = true;
        }
        else if (controller.isBattleMusicPlaying && !skeleton.IsAlive)
        {
            controller.battleMusic.Play();
           controller.peaceMusic.Play();
            controller.isBattleMusicPlaying = false;
        }

        if (!skeleton.IsAlive && !deathSound.isPlaying)
        {
            deathSound.Play();
        }


    }
}
