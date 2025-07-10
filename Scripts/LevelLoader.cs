using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator anim;
    public int state; // -1 dead, 0 idle, 1 start, 2 hurt

    void Start()
    {
        anim.SetTrigger("Start");
        Cursor.visible = false; // Oculta o cursor ao iniciar
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
    }

    void Update()
    {

        // GetComponent<Animator>().SetInteger("State", state);
        // if (Player.deadPlayer == true)
        // {

        // }
        if (Input.GetKeyDown(KeyCode.R)/* && !offPaused */)
        {
            Player.deadPlayer = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.M)/* && !offPaused */)
        {
            Player.deadPlayer = false;
            Transition("Menu");

        }
    }
    public void Transition(string sceneName)
    {
        anim.SetTrigger("Finish");
        StartCoroutine(LoadScening(sceneName));
    }

    IEnumerator LoadScening(string sceneName)
    {
        // anim.SetTrigger("Finish");
        // state = 1;
        PauseMenu.offPaused = true;

        yield return new WaitForSeconds(1f);

        // state = 0;
        SceneManager.LoadScene(sceneName);
        PauseMenu.offPaused = false;
    }

    IEnumerator DeadScene(string sceneName)
    {
        // state = -1;
        PauseMenu.offPaused = true;

        yield return new WaitForSeconds(1f);

        // state = 0;
        SceneManager.LoadScene(sceneName);
        PauseMenu.offPaused = false;
    }
    

}
