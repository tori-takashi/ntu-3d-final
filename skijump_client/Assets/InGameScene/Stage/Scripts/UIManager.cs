using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    bool isMultiMode = false;

    GameObject playerObject;
    PlayerController playerController;

    GameObject jumperObject;
    JumperController jumperController;

    GameObject distanceLabel;
    Text distanceLabelComponent;
    GameObject speedLabel;
    Text speedLabelComponent;
    GameObject matchResultLabel;
    Text matchResultLabelComponent;

    GameObject restartGameButton;
    Button restartGameButtonComponent;
    GameObject goToMainMenuButton;
    Button goToMainMenuButtonComponent;

    GameObject networkManagerObject;
    NetworkManager networkManager;
    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;

    Text playerNumberLabelComponent;

    void Start() {

        playerObject = GameObject.Find("PlayerController");
        playerController = playerObject.GetComponent<PlayerController>();

        isMultiMode = playerController.isMultiMode;

        jumperObject = GameObject.FindGameObjectWithTag("Jumper");
        jumperController = jumperObject.GetComponent<JumperController>();

        distanceLabel = GameObject.Find("DistanceLabel");
        distanceLabelComponent = distanceLabel.GetComponent<Text>();
        speedLabel = GameObject.Find("SpeedLabel");
        speedLabelComponent = speedLabel.GetComponent<Text>();
        matchResultLabel = GameObject.Find("MatchResultLabel");
        matchResultLabelComponent = matchResultLabel.GetComponent<Text>();

        restartGameButton = GameObject.Find("RestartGameButton");
        restartGameButtonComponent  = restartGameButton.GetComponent<Button>();
        goToMainMenuButton = GameObject.Find("GoToMainMenuButton");
        goToMainMenuButtonComponent  = goToMainMenuButton.GetComponent<Button>();

        restartGameButtonComponent.onClick.AddListener(RestartGame);
        goToMainMenuButtonComponent.onClick.AddListener(GoToMainMenu);

        restartGameButton.gameObject.SetActive(false);
        goToMainMenuButton.gameObject.SetActive(false);

        if(isMultiMode) {
            networkManagerObject = GameObject.Find("NetworkManager");
            networkManager = networkManagerObject.GetComponent<NetworkManager>();

            multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
            multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

            playerNumberLabelComponent = GameObject.Find("PlayerNumberLabel").GetComponent<Text>();
            playerNumberLabelComponent.text = "You are Player" + multiModeGameManager.myJumpOrder.ToString() + " Current Jumper is Player" + multiModeGameManager.currentJumper.ToString();

            if (playerController.myRole == "Blower") {
                speedLabel.SetActive(false);
            }

        }
    }

    void Update()
    {

        if(multiModeGameManager.gameAborted) {
            matchResultLabelComponent.text = "YOU WIN!";
            restartGameButton.gameObject.SetActive(true);
            goToMainMenuButton.gameObject.SetActive(true);
        }

        if (!jumperController.isCollidedWithDistanceBasePoint) {
            speedLabelComponent.text = Mathf.Round(jumperController.getSpeed()).ToString() + "km/h";
        } else {
            speedLabel.SetActive(false);
        }

        if(jumperController.isCollidedWithDistanceBasePoint && playerController.myRole == "Blower" && !jumperController.isDistanceMeasured) {
            speedLabel.SetActive(true);
            speedLabelComponent.text = Mathf.Round(jumperController.getSpeed()).ToString() + "km/h";
        } else {
            speedLabel.SetActive(false);
        }

        if (jumperController.getDistance() != 0 && !multiModeGameManager.gameAborted) {
            distanceLabelComponent.text = jumperController.getDistance().ToString() + "m";
        
            if(multiModeGameManager.isDeterminedWinOrLose()) {
                JudgeAndShowWinOrLose();
                restartGameButton.gameObject.SetActive(true);
                goToMainMenuButton.gameObject.SetActive(true);
            } else {
                Invoke("PlayerChange", 3f);
            }
        }
    }

    void JudgeAndShowWinOrLose() {
        if(multiModeGameManager.player1_result == multiModeGameManager.player2_result) {
            matchResultLabelComponent.text = "DRAW";
        } else {

            if(multiModeGameManager.myJumpOrder == 1) {
                if(multiModeGameManager.player1_result > multiModeGameManager.player2_result) {
                    matchResultLabelComponent.text = "YOU WIN!";
                } else {
                    matchResultLabelComponent.text = "YOU LOSE...";
                }

            } else {
                if(multiModeGameManager.player1_result < multiModeGameManager.player2_result) {
                    matchResultLabelComponent.text = "YOU WIN!";
                } else {
                    matchResultLabelComponent.text = "YOU LOSE...";                    }
            }
        }
    }

    void ChangeScene() {
        multiModeGameManager.LoadArena();
    }

    void PlayerChange() {
        PhotonNetwork.LoadLevel("PlayerChange");
    }

    void GoToMainMenu() {
        if (isMultiMode) networkManager.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    void RestartGame(){
        if (isMultiMode) {
            networkManager.Disconnect();
            SceneManager.LoadScene("MultiModeWaiting");
        } else {
            SceneManager.LoadScene("SingleMode");
        }
    }

}
