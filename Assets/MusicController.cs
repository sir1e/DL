using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Damagble damagble;
    public Skeleton skeleton;
    public AudioSource peaceMusic;
    public AudioSource battleMusic;
    public AudioSource deathSound;

    public bool isBattleMusicPlaying = false;

    private void Awake()
    {
        damagble = GetComponent<Damagble>();
        skeleton = GetComponent<Skeleton>();
    }

    void Start()
    {
        peaceMusic.Play();
    }

    void Update()
    {
        if (damagble.InBattle && !isBattleMusicPlaying)
        {
            peaceMusic.Stop();
            battleMusic.Play();
            isBattleMusicPlaying = true;
        }
        else if (!damagble.InBattle && isBattleMusicPlaying)
        {
            battleMusic.Stop();
            peaceMusic.Play();
            isBattleMusicPlaying = false;
        }

        if (damagble.IsAlive == false && !deathSound.isPlaying)
        {
            peaceMusic.Stop();
            battleMusic.Stop();
            isBattleMusicPlaying = false;
            deathSound.Play();
        }

      
    }
}
