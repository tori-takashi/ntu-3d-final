using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button singleModeButton;
    public Button multiModeButton;
    public Button rankingButton;
    public Button quitButton;

    void Start() {
        Button singleModeButtonComponent = singleModeButton.GetComponent<Button>();
        Button multiModeButtonComponent  = multiModeButton.GetComponent<Button>();
        Button rankingButtonComponent    = rankingButton.GetComponent<Button>();
        Button quitButtonComponent       = quitButton.GetComponent<Button>();
        
        singleModeButtonComponent.onClick.AddListener(StartSingleMode);
        multiModeButtonComponent.onClick.AddListener(StartMultiMode);
        rankingButtonComponent.onClick.AddListener(ShowRanking);
        quitButtonComponent.onClick.AddListener(QuitGame);
    }

    void Update() {
    }

    void StartSingleMode() {
        SceneManager.LoadScene("SingleMode");
    }

    void StartMultiMode() {
        SceneManager.LoadScene("MultiModeWaiting");
    }

    void ShowRanking() {
    }

    void QuitGame() {
        Application.Quit();
    }
}
