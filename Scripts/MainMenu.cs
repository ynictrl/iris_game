using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneName;
    public LevelLoader levelLoader;

    public enum MenuButtons
    {
        START, OPTIONS, CREDITS, QUIT,
        AUDIO, VIDEO, CONTROLLER, BACK
    }

    public MenuButtons[] currentMenuButton; // menu, options, credits

    public enum MenuScreen
    {
        MAINMENU, OPTIONSMENU, CREDITSMENU
    }

    public TextMeshProUGUI[] tittle;
    public TMP_Text[] textButtonsMainMenu;
    public TMP_Text[] textButtonsOptionsMenu;
    public TMP_Text[] textButtonsCreditsMenu;
    // public float outlineValue;
    public float timeDilate;
    public float dilateValue_Menu = -1;
    public float dilateValue_Options;
    public float dilateValue_Credits;

    public GameObject screenMenu;
    public GameObject screenOptions;

    public GameObject screenCredits;

    public bool[] startScreen; // menu, options


    public void PlayGame()
    {
        levelLoader.Transition(sceneName);
        Time.timeScale = 1f;
        Player.pausePlayer = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Player.frozenPlayer = false;
    }

    // public void GoToOptions()
    // {
    //     SceneManager.LoadScene("SettingsMenu");
    // }
    // public void GoToMainMenu()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }
    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        // StartCoroutine(AwakeMenu());
    }

    void Update()
    {

        // Main Menu
        tittle[0].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Menu);
        textButtonsMainMenu[0].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Menu);
        textButtonsMainMenu[1].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Menu);
        textButtonsMainMenu[2].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Menu);
        textButtonsMainMenu[3].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Menu);

        // Options
        tittle[1].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Options);
        textButtonsOptionsMenu[0].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Options);
        textButtonsOptionsMenu[1].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Options);
        textButtonsOptionsMenu[2].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Options);
        textButtonsOptionsMenu[3].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Options);

        // Creditis
        tittle[2].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Credits);
        textButtonsCreditsMenu[0].fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, dilateValue_Credits);




        // tittle.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineMode, dilateValue);
        // textButtons[0].fontMaterial.SetFloat(ShaderUtilities.ID_OutlineMode, outlineValue);
        // textButtons[1].fontMaterial.SetFloat(ShaderUtilities.ID_OutlineMode, outlineValue);
        // textButtons[2].fontMaterial.SetFloat(ShaderUtilities.ID_OutlineMode, outlineValue);

        // if (screenMenu == false)
        // {
        //     currentMenuButton = MenuButtons.AUDIO;
        // }


        if (startScreen[0])
        {
            AwakeScreen(MenuScreen.MAINMENU);
            ControlButtonsMainMenu();
        }
        else
        {
            FadeScreen(MenuScreen.MAINMENU);
        }

        if (startScreen[1])
        {
            AwakeScreen(MenuScreen.OPTIONSMENU);
            ControlButtonsOptionsMenu();
        }
        else
        {
            FadeScreen(MenuScreen.OPTIONSMENU);
        }

        if (startScreen[2])
        {
            AwakeScreen(MenuScreen.CREDITSMENU);
            ControlButtonsCreditsMenu();
        }
        else
        {
            FadeScreen(MenuScreen.CREDITSMENU);
        }
    }

    public void ControlButtonsMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (currentMenuButton[0])
            {
                case MenuButtons.START:
                    textButtonsMainMenu[0].text = "Start Game";
                    textButtonsMainMenu[1].text = "[ Credits ]";
                    currentMenuButton[0] = MenuButtons.CREDITS;
                    break;
                case MenuButtons.CREDITS:
                    textButtonsMainMenu[1].text = "Credits";
                    textButtonsMainMenu[2].text = "[ Quit ]";
                    currentMenuButton[0] = MenuButtons.QUIT;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            switch (currentMenuButton[0])
            {
                case MenuButtons.CREDITS:
                    textButtonsMainMenu[0].text = "[ Start Game ]";
                    textButtonsMainMenu[1].text = "Credits";
                    currentMenuButton[0] = MenuButtons.START;
                    break;
                case MenuButtons.QUIT:
                    textButtonsMainMenu[1].text = "[ Credits ]";
                    textButtonsMainMenu[2].text = "Quit";
                    currentMenuButton[0] = MenuButtons.CREDITS;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentMenuButton[0])
            {
                case MenuButtons.START:
                    timeDilate = 1.3f;
                    startScreen[0] = false;
                    PlayGame();
                    break;
                case MenuButtons.OPTIONS:
                    StartCoroutine(MainToOptions());
                    break;
                case MenuButtons.CREDITS:
                    StartCoroutine(MainToCredits());
                    break;
                case MenuButtons.QUIT:
                    QuitGame();
                    break;
            }
        }
    }

    public void ControlButtonsOptionsMenu()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (currentMenuButton[1])
            {
                case MenuButtons.AUDIO:
                    textButtonsOptionsMenu[0].text = "Audio";
                    textButtonsOptionsMenu[1].text = "[ Video ]";
                    currentMenuButton[1] = MenuButtons.VIDEO;
                    break;
                case MenuButtons.VIDEO:
                    textButtonsOptionsMenu[1].text = "Video";
                    textButtonsOptionsMenu[2].text = "[ Controller ]";
                    currentMenuButton[1] = MenuButtons.CONTROLLER;
                    break;
                case MenuButtons.CONTROLLER:
                    textButtonsOptionsMenu[2].text = "Controller";
                    textButtonsOptionsMenu[3].text = "[ Back ]";
                    currentMenuButton[1] = MenuButtons.BACK;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            switch (currentMenuButton[1])
            {
                case MenuButtons.VIDEO:
                    textButtonsOptionsMenu[0].text = "[ Audio ]";
                    textButtonsOptionsMenu[1].text = "Video";
                    currentMenuButton[1] = MenuButtons.AUDIO;
                    break;
                case MenuButtons.CONTROLLER:
                    textButtonsOptionsMenu[1].text = "[ Video ]";
                    textButtonsOptionsMenu[2].text = "Controller";
                    currentMenuButton[1] = MenuButtons.VIDEO;
                    break;
                case MenuButtons.BACK:
                    textButtonsOptionsMenu[2].text = "[ Controller ]";
                    textButtonsOptionsMenu[3].text = "Back";
                    currentMenuButton[1] = MenuButtons.CONTROLLER;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentMenuButton[1])
            {
                // case MenuButtons.VIDEO:
                //     PlayGame();
                //     break;
                // case MenuButtons.OPTIONS:
                //     StartCoroutine(MainToOptions());
                //     break;
                case MenuButtons.BACK:
                    StartCoroutine(OptionsToMain());
                    break;
            }
        }
    }

    public void ControlButtonsCreditsMenu()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CreditsToMain());
            // switch (currentMenuButton[1])
            // {
            //     // case MenuButtons.VIDEO:
            //     //     PlayGame();
            //     //     break;
            //     // case MenuButtons.OPTIONS:
            //     //     StartCoroutine(MainToOptions());
            //     //     break;
            //     case MenuButtons.BACK:
            //         StartCoroutine(OptionsToMain());
            //         break;
            // }
        }
    }


    void AwakeScreen(MenuScreen screen)
    {
        if (screen == MenuScreen.MAINMENU)
        {
            if (dilateValue_Menu <= 0f)
            {
                dilateValue_Menu += Time.deltaTime / timeDilate;
            }
        }

        if (screen == MenuScreen.OPTIONSMENU)
        {
            if (dilateValue_Options <= 0f)
            {
                dilateValue_Options += Time.deltaTime / timeDilate;
            }
        }

        if (screen == MenuScreen.CREDITSMENU)
        {
            if (dilateValue_Credits <= 0f)
            {
                dilateValue_Credits += Time.deltaTime / timeDilate;
            }
        }
    }

    void FadeScreen(MenuScreen screen)
    {

        if (screen == MenuScreen.MAINMENU)
        {
            if (dilateValue_Menu >= -1f)
            {
                dilateValue_Menu -= Time.deltaTime / timeDilate;
            }
        }

        if (screen == MenuScreen.OPTIONSMENU)
        {
            if (dilateValue_Options >= -1f)
            {
                dilateValue_Options -= Time.deltaTime / timeDilate;
            }
        }

        if (screen == MenuScreen.CREDITSMENU)
        {
            if (dilateValue_Credits >= -1f)
            {
                dilateValue_Credits -= Time.deltaTime / timeDilate;
            }
        }
    }

    IEnumerator MainToOptions()
    {
        startScreen[0] = false;
        yield return new WaitForSeconds(timeDilate);
        startScreen[1] = true;
        screenMenu.SetActive(false);
        screenOptions.SetActive(true);
        dilateValue_Menu = -1;

        yield return new WaitForSeconds(timeDilate);
        dilateValue_Options = 0;
    }

    IEnumerator OptionsToMain()
    {
        startScreen[1] = false;
        yield return new WaitForSeconds(timeDilate);
        startScreen[0] = true;
        screenMenu.SetActive(true);
        screenOptions.SetActive(false);
        dilateValue_Options = -1;

        yield return new WaitForSeconds(timeDilate);
        dilateValue_Menu = 0;
    }

    IEnumerator MainToCredits()
    {
        startScreen[0] = false;
        yield return new WaitForSeconds(timeDilate);
        startScreen[2] = true;
        screenMenu.SetActive(false);
        screenCredits.SetActive(true);
        dilateValue_Menu = -1;

        yield return new WaitForSeconds(timeDilate);
        dilateValue_Credits = 0;
    }
    
    IEnumerator CreditsToMain()
    {
        startScreen[2] = false;
        yield return new WaitForSeconds(timeDilate);
        startScreen[0] = true;
        screenMenu.SetActive(true);
        screenCredits.SetActive(false);
        dilateValue_Credits = -1;
        
        yield return new WaitForSeconds(timeDilate);
        dilateValue_Menu = 0;
    }

}