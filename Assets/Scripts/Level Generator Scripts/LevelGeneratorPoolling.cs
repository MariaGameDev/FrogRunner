using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPoolling : MonoBehaviour
{
    [SerializeField]
    private Transform platform, platform_parent;

    [SerializeField]
    private Transform monster, monster_parent;

    [SerializeField]
    private Transform health_Collectable, health_collectable_parent;

    [SerializeField]
    private int levelLength = 100;

    [SerializeField]
    private float distance_between_Platforms = 15f;

    [SerializeField]
    private float MIN_Position_Y = 0f, MAX_Position_Y = 7f;

    [SerializeField]
    private float chance_of_MonsterExistence = 0.25f, chance_of_HealthCollectable_Existence = 0.1f;

    [SerializeField]
    private float healthCollectable_Min_Y = 1f, healthCollectable_Max_Y = 3f;

    private float platformLastPosition_X;
    private Transform[] platform_array;


    void Start()
    {
        CreatePlatforms();
    }

    
    void CreatePlatforms()
    {
        platform_array = new Transform[levelLength];

        for (int i = 0; i < platform_array.Length; i++)
        {

            Transform newPlatform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
            platform_array[i] = newPlatform;

        }

        //to make a position for platforms
        for (int i = 0; i < platform_array.Length; i++)
        {
            float platformPositionY = Random.Range(MIN_Position_Y, MAX_Position_Y);
            Vector3 platformPosition;

            if (i < 2)
            {
                platformPositionY = 0f;
            }

            platformPosition = new Vector3(distance_between_Platforms * i, platformPositionY, 0);
            platformLastPosition_X = platformPosition.x;
            platform_array[i].position = platformPosition;
            platform_array[i].parent = platform_parent;

            //spawn monsters and health collectables(hearts)
            SpawnHealthAndMonsters(platformPosition, i, true);

        }
    }



    public void PoolingPlatforms()
    {
        for (int i = 0; i < platform_array.Length; i++)
        {
            //if platforms is NOT active in the scene
            if (!platform_array[i].gameObject.activeInHierarchy)
            {
                platform_array[i].gameObject.SetActive(true);
                float platformPositionY = Random.Range(MIN_Position_Y, MAX_Position_Y);
                Vector3 platformPosition = new Vector3(distance_between_Platforms + platformLastPosition_X, platformPositionY, 0);
                platform_array[i].position = platformPosition;
                platformLastPosition_X = platformPosition.x;

                //spawn health and monsters
                SpawnHealthAndMonsters(platformPosition, i, false);
            }
        }



    }

    void SpawnHealthAndMonsters(Vector3 platformPosition, int i, bool gameStarted)
    {
        if (i > 3)
        {
            if (Random.Range(0f, 2f) < chance_of_MonsterExistence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_Platforms * i , platformPosition.y + 0.1f, 0 );
                }
                else
                {
                    platformPosition = new Vector3(distance_between_Platforms + platformLastPosition_X, platformPosition.y + 0.1f, 0);
                }

                Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0));
                createMonster.parent = monster_parent;

            } // if monsters

            if (Random.Range(0f, 1f) < chance_of_HealthCollectable_Existence)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_Platforms * i, 
                        platformPosition.y + Random.Range(healthCollectable_Min_Y, healthCollectable_Max_Y),0);
                }

                else
                {
                    platformPosition = new Vector3(distance_between_Platforms + platformLastPosition_X,
                        platformPosition.y + Random.Range(healthCollectable_Min_Y, healthCollectable_Max_Y),0);
                }

                Transform createHealthCollectable = (Transform)Instantiate(health_Collectable,
                    platformPosition, Quaternion.identity);
                createHealthCollectable.parent = health_collectable_parent;
            }




        }//if i > a


    }











}//class
