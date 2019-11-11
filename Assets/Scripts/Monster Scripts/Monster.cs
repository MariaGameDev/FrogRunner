using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public GameObject MonsterDiedEffect;
    public Transform bullet;
    public float distanceFromPlayerToStartMove = 30f;
    public float movementSpeedMin = 1f;
    public float movementSpeedMax = 2f;

    private bool moveRight;
    private float movementSpeed;
    private bool isPlayerInRegion;

    private Transform playerTransform;

    public bool canShoot;

    private string FUNCTION_TO_INVOKE = "StartShooting";

    
    void Start()
    {

        

        if (Random.Range(0.0f, 0.1f) > 0.5f)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }

        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);

        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;

    }

    
    void Update()
    {
        if (playerTransform)
        {
            float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;  //distance a minus distance b
            if (distanceFromPlayer < distanceFromPlayerToStartMove)
            {
                if (moveRight)
                {
                    Vector3 temp = transform.position;   //move to the right sight
                    temp.x += Time.deltaTime * movementSpeed;
                    transform.position = temp;
                 //it's the same as above/   transform.position = new Vector3(transform.position.x + 
                 //       Time.deltaTime * movementSpeed, transform.position.y, transform.position.z);
                }
                else
                {
                    Vector3 temp = transform.position;   // move to the left side
                    temp.x -= Time.deltaTime * movementSpeed;
                    transform.position = temp;
                }

                if (!isPlayerInRegion)
                {
                    //region where player starts movement
                    if (canShoot)
                    {
                        InvokeRepeating(FUNCTION_TO_INVOKE, 0.5f, 1.5f);
                    }

                    isPlayerInRegion = true;

                }
                
            }
            else
            {
                CancelInvoke(FUNCTION_TO_INVOKE);
            }

        }

        
    }


    void StartShooting()
    {
        if (playerTransform)
        {
            //todo update this method with pooling gameobject technique
            Vector3 bulletPosition = transform.position;
            bulletPosition.y += 1.5f;
            bulletPosition.x -= 1f;
            Transform newBullet = (Transform)Instantiate(bullet, bulletPosition,Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
            newBullet.parent = transform;

        }
    }

    void MonsterDied()
    {
        Vector3 effectPosition = transform.position;
        effectPosition.y += 2f;
        Instantiate(MonsterDiedEffect, effectPosition, Quaternion.identity);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.PLAYER_BULLET_TAG)
        {
            GamePlayController.instance.IncrementScore(200);
            MonsterDied();
        }

    }  

    private void OnCollisionEnter(Collision target)
    {
        if (target.collider.tag == Tags.PLAYER_TAG)
        {
            MonsterDied();
        }


    }

  





}
