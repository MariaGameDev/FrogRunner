using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageShot : MonoBehaviour
{
    [SerializeField]
    private Transform playerBullet;


    private float distanceBeetweenNewPlatforms = 120f;
    private LevelGenerator levelGenerator;

    private LevelGeneratorPoolling levelGenerator_pooling;

    [HideInInspector]
    public bool canShoot;

    
    

  //  private BGScroller bgScroller;  / it moved to GamePlayController script
    
    void Awake()
    {
        levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
        levelGenerator_pooling = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGeneratorPoolling>();
        

        //bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();  / GamePlayController script
    }

    //void Start(){
    // start corutine when bg stops scrolling / GAMEPLAYCONTROLLER script
    //}

    void FixedUpdate()
    {
        Fire();
        
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canShoot)
            {
                Vector3 bulletPosition = transform.position;
                bulletPosition.y += 1.5f;
                bulletPosition.x += 1f;
                Transform newBullet = (Transform)Instantiate(playerBullet, bulletPosition, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1500f);
                transform.parent = transform;
            }
            
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        
        if (target.tag == Tags.MONSTER_BULLET_TAG || target.tag == Tags.BOUNDS_TAG)
        {
            GamePlayController.instance.TakeDamage();
            //inform game controller that player has died
            Die();
           

        }

        if (target.tag == Tags.HEALTH_TAG)
        {
            GamePlayController.instance.IncrementHealth();
            target.gameObject.SetActive(false);

        }


        if (target.tag == Tags.MORE_PLATFORMS_TAG)
        {
            Vector3 temp = target.transform.position;
            temp.x += distanceBeetweenNewPlatforms;
            target.transform.position = temp;

            // levelGenerator.GenerateLevel(false);

            levelGenerator_pooling.PoolingPlatforms();

        }


    }

    private void OnCollisionEnter(Collision target)
    {

       

        if (target.gameObject.tag == Tags.PLAYER_TAG || target.gameObject.tag == Tags.MONSTER_TAG)
        {
            if (GamePlayController.instance.health > 0)
            {
                GamePlayController.instance.TakeDamage();
                if (GamePlayController.instance.health == 0)
                {
                    GamePlayController.instance.health = 0;
                    Die();
                    GamePlayController.instance.Died();


                }
            }

            else if (GamePlayController.instance.health == 0)
            {
                Die();
                GamePlayController.instance.Died();
            }

            else if (GamePlayController.instance.health <= 0)
            {
                Debug.Log("LIFES FINISHED");
                Die();
                GamePlayController.instance.RestartToMainMenu();
            }
            
        }

     

    }



    void Die()
    {
        
       
         Destroy(gameObject);
        
    }

    IEnumerator PlayerDieCorutine()
    {
        yield return new WaitForSeconds(1f);
       
    }

}//class
