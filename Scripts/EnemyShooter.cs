using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Enemy enemy;
    public GameObject bulletObject;
    public Transform spawnBullet;
    public float fireTime;
    public bool onFire = true;
    public bool isFire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shot();
        Flip();
    }

    void Shot()
    {
        if (onFire && isFire && enemy.healthCurrent > 0)
        {
            GameObject cloneBullet = Instantiate(bulletObject, spawnBullet.position, spawnBullet.rotation);
            StartCoroutine(Shotting());
            StartCoroutine(ShottingAnim());
        }
    }

    void Flip()
    {
        if (enemy.direction_x == -1)
        {
            spawnBullet.position = new Vector2(transform.position.x - 0.4f, transform.position.y + 0.4f);
            spawnBullet.rotation = Quaternion.Euler(0, 0, 90f);
        }
        if (enemy.direction_x == 1)
        {
            spawnBullet.position = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.4f);
            spawnBullet.rotation = Quaternion.Euler(0, 0, 270f);
        }
    }

    IEnumerator Shotting()
    {
        isFire = false;

        yield return new WaitForSeconds(fireTime);

        isFire = true;
    }

    IEnumerator ShottingAnim()
    {
        enemy.state = 2;

        yield return new WaitForSeconds(0.3f);

        enemy.state = 0;
        
        yield return new WaitForSeconds(fireTime);

        enemy.state = 2;
    }
}
