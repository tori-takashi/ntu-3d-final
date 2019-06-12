using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerChange : MonoBehaviour
{
    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;

    GameObject restartGameButton;
    Button restartGameButtonComponent;
    GameObject goToMainMenuButton;
    Button goToMainMenuButtonComponent;

    void Start()
    {
        multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
        multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

/*      restartGameButton = GameObject.Find("RestartGameButton");
        restartGameButtonComponent  = restartGameButton.GetComponent<Button>();
        goToMainMenuButton = GameObject.Find("GoToMainMenuButton");
        goToMainMenuButtonComponent  = goToMainMenuButton.GetComponent<Button>();

        restartGameButtonComponent.onClick.AddListener(RestartGame);
        goToMainMenuButtonComponent.onClick.AddListener(GoToMainMenu);

        restartGameButton.gameObject.SetActive(false);
        goToMainMenuButton.gameObject.SetActive(false);
*/
        if(multiModeGameManager.currentJumper == 1) multiModeGameManager.currentJumper++;
        

        Invoke("ChangeScene", 3f);
    }

    void Update()
    {
        /*
        if(multiModeGameManager.gameAborted) {
            restartGameButton.gameObject.SetActive(true);
            goToMainMenuButton.gameObject.SetActive(true);
        }
        */
        
    }

    void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void RestartGame(){
        SceneManager.LoadScene("MultiModeWaiting");
    }

    void ChangeScene() {
        if(!multiModeGameManager.gameAborted) PhotonNetwork.LoadLevel("MultiMode");
    }
}
