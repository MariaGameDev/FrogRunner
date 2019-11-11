using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField]
    private int levelLength;

    [SerializeField]
    private int startPlatformLength = 5, endPlatformLength = 5;

    [SerializeField]
    private int distance_between_platforms;

    [SerializeField]
    private Transform platformPrefab, platform_parent;

    [SerializeField]
    private Transform monster, monster_parent;

    [SerializeField]
    private Transform health_CollectAble, health_Collactable_parent;

    [SerializeField]
    private float platformPosition_MinY = 0f, platformPosition_MaxY = 10f;

    [SerializeField]
    private int platformLength_Min = 1, platformLength_Max = 4;

    [SerializeField]
    private float chanceForMonsterExcistance = 0.25f, chanceForCollectableExistance = 0.1f;

    [SerializeField]
    private float healthCollectable_MinY = 1f, healthCollectable_MaxY = 3f;

    private float platforLastPositionX;

    private enum PlatformType
    {
        None,
        Flat

    }

    

    //inner class
    private class PlatformPositionInfo
    {
        public PlatformType platformType;
        public float positionY;
        public bool hasMonster;
        public bool hasHealthCollectable;

        //constructor
        public PlatformPositionInfo(PlatformType type, float posY, bool has_monster, bool has_collectable)
        {
            platformType = type;
            positionY = posY;
            hasMonster = has_monster;
            hasHealthCollectable = has_collectable;

        }


    } //class PlatformPositionInfo

    private void Start()
    {
        GenerateLevel(true);
    }


    void FillOutPositionInfo(PlatformPositionInfo[] platformInfo )
    {
        int currentPlatformInfoIndex = 0;
        
        for (int i = 0; i < startPlatformLength; i++)
        {
            platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
            platformInfo[currentPlatformInfoIndex].positionY = 0f;
            currentPlatformInfoIndex++;
        }

        while (currentPlatformInfoIndex < levelLength - endPlatformLength)
        {
            if (platformInfo[currentPlatformInfoIndex - 1].platformType != PlatformType.None)
            {
                currentPlatformInfoIndex++;
                continue;
            }

            float platformPositionY = Random.Range(platformPosition_MinY, platformPosition_MaxY);
            int platformLength = Random.Range(platformLength_Min, platformLength_Max);

            for (int i = 0; i < platformLength; i++)
            {
                bool has_Monster = (Random.Range(0f, 1f) < chanceForMonsterExcistance);
                bool has_Health_Collectable = (Random.Range(0f, 1f) < chanceForCollectableExistance);

                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = platformPositionY;
                platformInfo[currentPlatformInfoIndex].hasMonster = has_Monster;
                platformInfo[currentPlatformInfoIndex].hasHealthCollectable = has_Health_Collectable;

                currentPlatformInfoIndex++;

                if (currentPlatformInfoIndex > (levelLength - endPlatformLength))
                {
                    currentPlatformInfoIndex = levelLength - endPlatformLength;
                    break;
                }

            }

            for (int i = 0; i < endPlatformLength; i++)
            {
                platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
                platformInfo[currentPlatformInfoIndex].positionY = 0f;
                currentPlatformInfoIndex++;
            }


        } //while loop

        
    }

    void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformPositionInfo, bool gameStarted)
    {
        for (int i = 0; i < platformPositionInfo.Length; i++)
        {
            PlatformPositionInfo positionInfo = platformPositionInfo[i];

            if (positionInfo.platformType == PlatformType.None)
            {
                continue;
            }

            Vector3 platformPosition;

            //check if is game has started or not
            if (gameStarted)
            {
                platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY, 0);
            }
            else
            {
                platformPosition = new Vector3(distance_between_platforms + platforLastPositionX, positionInfo.positionY, 0);
            }

            //save the platform position x for later use
            platforLastPositionX = platformPosition.x;


            Transform createBlock = (Transform)Instantiate(platformPrefab, platformPosition, Quaternion.identity);
            createBlock.parent = platform_parent;

            if (positionInfo.hasMonster)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_platforms * i, positionInfo.positionY + 0.1f, 0); 
                }
                else
                {
                    platformPosition = new Vector3(distance_between_platforms + platforLastPositionX, positionInfo.positionY + 0.1f, 0);
                }

                Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0,-90,0));

                createMonster.parent = monster_parent;

            }



            if (positionInfo.hasHealthCollectable)
            {
                if (gameStarted)
                {
                    platformPosition = new Vector3(distance_between_platforms * i, 
                        positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY));
                }
                else
                {
                    platformPosition = new Vector3(distance_between_platforms + platforLastPositionX,
                        positionInfo.positionY + Random.Range(healthCollectable_MinY, healthCollectable_MaxY));
                }

                Transform createHealthCollectable =(Transform)Instantiate(health_CollectAble, platformPosition, Quaternion.identity);
                createHealthCollectable.parent = health_Collactable_parent;



            }

        } //for loop


    }

    public void GenerateLevel(bool gameStarted)
    {
        PlatformPositionInfo[] platforminfo = new PlatformPositionInfo[levelLength];

        for (int i = 0; i < platforminfo.Length; i++)
        {
            platforminfo[i] = new PlatformPositionInfo(PlatformType.None, -1, false, false);
        }

        FillOutPositionInfo(platforminfo);
        CreatePlatformsFromPositionInfo(platforminfo, gameStarted);
    }

} //class



