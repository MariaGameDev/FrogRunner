using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;

public class MainMenuSounds : MonoBehaviour
{




    public Animation animationButton;
    public AudioSource audioSource;
    public AudioClip slideButtons;
    public AudioClip clickButtons;



    //  private MainMenuController mainMenuController;


    /*  void Awake()
      {
          audioSource = GetComponent<AudioSource>();
          mainMenuController = GetComponent<MainMenuController>();

      } */

    private void Start()
    {
        StartCoroutine(StartSound());
    }


    #region MAIN MENU SOUNDS

    public void ClickButtonSound()
    {
        audioSource.PlayOneShot(clickButtons);


    }

    public void SlideButtonSound()
    {
        
        audioSource.PlayOneShot(slideButtons);


    }

   
   IEnumerator StartSound()
    {
        yield return new WaitForSeconds(0.5f);
        SlideButtonSound();
    }


    #endregion

}//class
















