using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button gameStartButton;
    public Button quitButton;

    void Start()
    {
        Button gameStartButtonComponent = gameStartButton.GetComponent<Button>();
        Button quitButtonComponent      = quitButton.GetComponent<Button>();
        
        gameStartButtonComponent.onClick.AddListener(StartSoloGame);
        quitButtonComponent.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        
    }

    void QuitGame(){
        Application.Quit();
    }

    void StartSoloGame(){
        SceneManager.LoadScene("InGameScene");
    }
}
