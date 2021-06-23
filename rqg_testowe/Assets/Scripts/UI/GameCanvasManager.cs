using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private MainMenuScreen mainMenuScreen;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private HighScoreScreen highScoreScreen;
    [SerializeField] private GameResultScreen gameResultScreen;
    [SerializeField] private GamePlayScreen gamePlayScreen;


    private BaseUIScreen currentScreen;

    private void Awake()
    {
        gameManagerData.AfterGameStateChange += ManageScreens;
        //for debug
        currentScreen = mainMenuScreen;
    }


    private void Update()
    {
        currentScreen.DoJob();
    }

    private void ManageScreens()
    {
        switch (gameManagerData.currentGameState)
        {
            case GameState.Loading:
                SwitchScreen(loadingScreen);
                break;
            case GameState.MainMenu:
                SwitchScreen(mainMenuScreen);
                break;
            case GameState.Gameplay:
                SwitchScreen(gamePlayScreen);
                break;
            case GameState.Results:
                SwitchScreen(gameResultScreen);
                break;
            case GameState.Highscores:
                SwitchScreen(highScoreScreen);
                break;
        }
    }

    private void SwitchScreen(BaseUIScreen screenToSwitch)
    {
        currentScreen.DisableScreen();
        screenToSwitch.EnableScreen();
        currentScreen = screenToSwitch;
    }
}
