using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    











    public void PlayGameButtonClicked()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        
        //SceneManager.LoadScene(Tags.FIRST_LEVEL);

        StartCoroutine(StartScene());
        Debug.Log("Button is clicked");
    }

    public void GoBackMainMenuButton()
    {
        GameManager.instance.gameRestartedPlayerDied = false;
        SceneManager.LoadScene(Tags.MAIN_MENU_SCENE);
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        // code later
    }


    public void OptionsButtonClicked()
    {
        Debug.Log("OPTIONS Button is clicked");
        StartCoroutine(OptionsSceneStart());
    }




    public void SoundButtonClicked()
    {
        Debug.Log(" SOUND Button is clicked");
    }



    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Tags.FIRST_LEVEL);
    }

    IEnumerator OptionsSceneStart()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Tags.OPTIONS_SCENE);
    }










}//class
