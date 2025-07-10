using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Player.deadPlayer == true)
        {
            GetComponent<Animator>().SetTrigger("Start");
            // Time.timeScale = 0f;
        }
    }
}
