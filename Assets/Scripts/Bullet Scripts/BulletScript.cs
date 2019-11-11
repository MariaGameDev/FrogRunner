using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float lifeTime = 5f;
    private float startY;


    void Start()
    {

        startY = transform.position.y;
       // StartCoroutine();

    }

    

    // this function calls when update finishes
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
            
    }

    IEnumerator TurnOffBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Tags.MONSTER_TAG || target.tag == Tags.PLAYER_TAG 
            || target.tag == Tags.MONSTER_BULLET_TAG || target.tag == Tags.PLAYER_BULLET_TAG)
        {
            gameObject.SetActive(false);
           // Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.MONSTER_TAG || target.gameObject.tag == Tags.PLAYER_TAG )
        {
            gameObject.SetActive(false);
        }
    } 

}//class
