using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    // public GameObject pauseMenu;
    // public GameObject settingsMenu;
    // public GameObject controllMenu;
    // public GameObject soundMenu;
    [SerializeField] public static bool isPaused;
    public static bool offPaused;
    public string sceneName;
    public string atualSceneName;
    public string nextSceneName;
    public LevelLoader levelLoader;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        // pauseMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)/* && !offPaused */)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }



    }

    //resetar level
    public void Realoadlevel()
    {
        Time.timeScale = 1f;
        levelLoader.Transition(atualSceneName);
        PauseMenu.isPaused = false;
        isPaused = false;
        Player.pausePlayer = false;
        //SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        //player.isAlive = true;
        //public void RestartGame(string lvlName){
        //SceneManager.LoadScene(lvlName);}
    }

    public void PauseGame()
    {
        // pauseMenu.SetActive(true);
        Player.pausePlayer = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        //StartCoroutine(blockFireDelay());
        // pauseMenu.SetActive(false);
        // settingsMenu.SetActive(false);
        // soundMenu.SetActive(false);
        // controllMenu.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        levelLoader.Transition(sceneName);
        isPaused = false;
        //SceneManager.LoadScene("MainMenu");
        
        //Player.frozenPlayer = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
