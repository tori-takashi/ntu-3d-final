using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    bool isMultiMode = false;

    GameObject jumperObject;
    JumperController jumperController;
    GameObject networkManagerObject;
    NetworkManager networkManager;

    public  Text distanceLabel;
    private Text distanceLabelComponent;
    public  Text speedLabel;
    private Text speedLabelComponent;

    public  Button restartGameButton;
    private Button restartGameButtonComponent;
    public  Button goToMainMenuButton;
    private Button goToMainMenuButtonComponent;

    public  Text playerNumberLabel;
    private Text playerNumberLabelComponent;

    public  Button quitButton;
    private Button quitButtonComponent;

    void Start() {
        jumperObject = GameObject.Find("Jumper");
        jumperController = jumperObject.GetComponent<JumperController>();

        networkManagerObject = GameObject.Find("NetworkManager");
        Debug.Log("aaaaaaaaaaaaaaa");
        Debug.Log(networkManagerObject);
        networkManager = networkManagerObject.GetComponent<NetworkManager>();

        isMultiMode = jumperController.isMultiMode;
        
        distanceLabelComponent = distanceLabel.GetComponent<Text>();
        speedLabelComponent    = speedLabel.GetComponent<Text>();
        playerNumberLabelComponent = playerNumberLabel.GetComponent<Text>();

        restartGameButtonComponent  = restartGameButton.GetComponent<Button>();
        goToMainMenuButtonComponent = goToMainMenuButton.GetComponent<Button>();
        quitButtonComponent = quitButton.GetComponent<Button>();

        restartGameButtonComponent.onClick.AddListener(RestartGame);
        goToMainMenuButtonComponent.onClick.AddListener(GoToMainMenu);
        quitButtonComponent.onClick.AddListener(GoToMainMenu);

        playerNumberLabelComponent.text = "You are Player No." + networkManager.getJumpOrder().ToString();

        restartGameButton.gameObject.SetActive(false);
        goToMainMenuButton.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!jumperController.isCollidedWithDistanceBasePoint) {
            speedLabelComponent.text = Mathf.Round(jumperController.getSpeed()).ToString() + "km/h";
        } else {
            speedLabelComponent.text = "";
        }

        if(jumperController.getDistance() != 0) {
            distanceLabelComponent.text = jumperController.getDistance().ToString() + "m";

            restartGameButton.gameObject.SetActive(true);
            goToMainMenuButton.gameObject.SetActive(true);
        }
    }

    void OnLeftRoom() {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
        Destroy(networkManager);
        Debug.Log("Left from the room");
    }

    void GoToMainMenu() {
        if (isMultiMode) {
            PhotonNetwork.LeaveRoom();
        } else {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void RestartGame(){
        if (isMultiMode) {
            Destroy(networkManager);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("MultiModeWaitingRoom");
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
