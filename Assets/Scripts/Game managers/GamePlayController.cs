using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{

    public static GamePlayController instance;

    private Text scoreText, healthText, levelText, lifesText;

    [HideInInspector]
    public float score, health, level, lifesOfPlayer;
    

    [HideInInspector]
    public bool canCountScore;

    private BGScroller bgScroller;

    

    PlayerDamageShot playerDamageShot;

    #region Unity Methods

    void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find(Tags.SCORE_TEXT_OBJ).GetComponent<Text>();
        healthText = GameObject.Find(Tags.HEALTH_TEXT_OBJ).GetComponent<Text>();
        levelText = GameObject.Find(Tags.LEVEL_TEXT_OBJ).GetComponent<Text>();
        lifesText = GameObject.Find("LifesText").GetComponent<Text>();

        bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();

        playerDamageShot = GetComponent<PlayerDamageShot>();
    }

    private void Update()
    {
      //  IncrementScore(1);
    }


    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 

    #endregion

    //here we use delegates
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }

   




    #region Scene manager load Methods

    private void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Tags.FIRST_LEVEL)
        {
            
            Debug.Log("First level loaded");
            if (GameManager.instance.gameStartedFromMainMenu)
            {
                GameManager.instance.gameStartedFromMainMenu = false;
                GameManager.instance.lifesFinished = false;
                
                score = 0;
                health = 3;
                level = 1;
                lifesOfPlayer = 2;

                Debug.Log("GAME STARTED FROM MAIN MENU!!!");
            }
            else if (GameManager.instance.gameRestartedPlayerDied & GameManager.instance.lifesFinished)
            {
                
                GameManager.instance.gameRestartedPlayerDied = false;
                GameManager.instance.lifesFinished = false;
                score = GameManager.instance.score;
                health = GameManager.instance.health;
                lifesOfPlayer = GameManager.instance.lifes;
                level = 1;
                lifesOfPlayer= 1;
                
                Debug.Log("GAME STARTED FROM FIRST LEVEL!!!");
            }

            else if (GameManager.instance.gameRestartedPlayerDied & GameManager.instance.lifesFinished)
            {
                GameManager.instance.gameRestartedPlayerDied = false;
                GameManager.instance.lifesFinished = false;
                score = GameManager.instance.score;
                health = GameManager.instance.health;
                lifesOfPlayer = 0;

                Debug.Log("LIFEs FINISHED!");

            }
            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();
            lifesText.text = lifesOfPlayer.ToString();
        }

       


    }

    public void TakeDamage()
    {

        
        health--;

        if (health < 3)
        {
            healthText.text = health.ToString();
            Debug.Log("HEALTH " + health.ToString());
            
        }
       
     

    }




    public void Died()
    {
        lifesOfPlayer--;
        if (health == 0)
        {
            Debug.Log("HEATH EQUALS ZERO!!!");
            StartCoroutine(PlayerDied(Tags.FIRST_LEVEL));  
            //healthText.text = health.ToString();
        }


    }

    
    public void RestartToMainMenu()
    {
        

        if (lifesOfPlayer <= 0)
        {
            if (health <= 0)
            {
                StartCoroutine(PlayerDied(Tags.MAIN_MENU_SCENE));

            }
        }
        
            
        
       
    }

    public void IncrementScore(float scoreValue)
    {
        
        if (canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }
    }

    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
    }

    









    IEnumerator PlayerDied(string sceneName)
    {
        canCountScore = false;
        bgScroller.canScroll = false;
        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.gameRestartedPlayerDied = true;
        yield return new WaitForSecondsRealtime(2f);
        //Die animation play
        SceneManager.LoadScene(sceneName);

    }






    #endregion




}//class
