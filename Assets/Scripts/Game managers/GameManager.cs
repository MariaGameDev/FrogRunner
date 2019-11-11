using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedPlayerDied, lifesFinished; //it's public variable that makes access from other script but at the same time not visible in inspector
    //but also we can make it private bu we should create getter and setter for it 

    [HideInInspector]
    public float score, health, level, lifes;
    


    private void Awake()
    {
        MakeSingleton();
    }


    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }






}//class

