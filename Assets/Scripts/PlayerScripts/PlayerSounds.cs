using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    private float collisionSoundEffect = 1f;

    public float audioFootVolume = 1f;
    public float soundEffectPitchRandomness = 0.05f;

    //for main menu
   // public bool buttonCliked;

    private AudioSource audioSource;
    public AudioClip genericFootSound;
    public AudioClip metalFootSound;

  


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
       

    }


    #region GAME PLAY SOUNDS

    void FootSound()
    {
        audioSource.volume = collisionSoundEffect * audioFootVolume;
        audioSource.pitch = Random.Range(1.0f - soundEffectPitchRandomness, 1.0f + soundEffectPitchRandomness);

        if (Random.Range(0,2) > 0)
        {
            audioSource.clip = genericFootSound;
        }
        else
        {
            audioSource.clip = metalFootSound;
        }

        audioSource.Play();



        print("play sound");
    }


    void JumpSound()
    {

    }


    void DieSound()
    {

    }

    #endregion

  

}//class
