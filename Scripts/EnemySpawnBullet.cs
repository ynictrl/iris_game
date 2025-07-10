using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBullet : MonoBehaviour
{
    public Transform enemyPos;
    public float speed;
    //public Enemy enemy;
    //public Sprit sprit

    public int num = 0;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //enemyPos = GameObject.Find("Enemy" + num).GetComponent<Transform>();
  
        //enemy = GameObject.Find("Enemy" + num).GetComponent<Enemy>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        // if(Vector2.Distance(transform.position, enemyPos.position) > 0) //seguir enemy
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, enemyPos.position, speed * Time.deltaTime);         
        // } 

        // Vector3 targetPos = target.position;
        // Vector2 direction = new Vector2((targetPos.x  + num) - transform.position.x, (targetPos.y + num) - transform.position.y);
        // transform.up = direction;

        // // transform.rotation = target.rotation;
        
    }
}
