using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject jumper;
    JumperController jumperController;

    public  Text distanceLabel;
    private Text distanceLabelComponent;

    public Text speedLabel;
    private Text speedLabelComponent;

    public  Button restartGameButton;
    private Button restartGameButtonComponent;
    public  Button goToMainMenuButton;
    private Button goToMainMenuButtonComponent;


    void Start()
    {
        jumper = GameObject.Find("Jumper");
        jumperController = jumper.GetComponent<JumperController>();
        
        distanceLabelComponent = distanceLabel.GetComponent<Text>();
        speedLabelComponent = speedLabel.GetComponent<Text>();

        restartGameButtonComponent = restartGameButton.GetComponent<Button>();
        goToMainMenuButtonComponent = goToMainMenuButton.GetComponent<Button>();

        restartGameButtonComponent.onClick.AddListener(RestartGame);
        goToMainMenuButtonComponent.onClick.AddListener(GoToMainMenu);

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

    void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");       
    }
}
