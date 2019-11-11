using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamagePooling : MonoBehaviour
{
    //updated PlayerDamageShot script TODO


    [SerializeField]
    private Transform playerBullet;

    [SerializeField]
    private Transform[] bullet_array; // create 100 bullets and deactivate them and reuse them 



    private float distanceBeetweenNewPlatforms = 120f;
    private LevelGenerator levelGenerator;

    private LevelGeneratorPoolling levelGenerator_pooling;

    [HideInInspector]
    public bool canShoot;

    

    void Awake()
    {
        levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGenerator>();
        levelGenerator_pooling = GameObject.Find(Tags.LEVEL_GENERATOR_OBJ).GetComponent<LevelGeneratorPoolling>();

        //bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();
    }



    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canShoot)
            {
                
            }

        }
    }

    private void OnTriggerEnter(Collider target)
    {

    }

    private void OnCollisionEnter(Collision target)
    {

    }

    void Die()
    {
        // stop scrolling bg
        Destroy(gameObject);
    }

}//class
