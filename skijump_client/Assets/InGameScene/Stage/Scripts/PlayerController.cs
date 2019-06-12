using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public bool isMultiMode = false;
    
    public bool isInstantiatedTwoPlayers = false;
    public string myRole;
    bool isTwoPlayerExisted = false;

    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;

    public GameObject UIManager;

    public GameObject jumper;
    public GameObject blower;

    void Start() {
        Physics.gravity = new Vector3(0,-30f,0);

        if (SceneManager.GetActiveScene().name == "MultiMode"){
            isMultiMode = true;

            multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
            multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();
            
            if(multiModeGameManager.currentJumper == multiModeGameManager.myJumpOrder) {
                myRole = "Jumper";
                PhotonNetwork.Instantiate("Jumper", new Vector3(0f, 82.5f, 200f), Quaternion.identity, 0);
            } else {
                myRole = "Blower";
                PhotonNetwork.Instantiate("Blower", new Vector3(0f,82.5f,200f), Quaternion.identity, 0);
            }
        } else {
            InitializeSingleMode();
        }

    }

    void InitializeSingleMode() {
        //Instantiate(jumper);
        //Instantiate(mainCam);
    }


    void Update() {
        if(isMultiMode && !isInstantiatedTwoPlayers){
          if (isTwoPlayerExisted) {
            isInstantiatedTwoPlayers = true;

            blower = GameObject.FindGameObjectWithTag("Blower"); 
            jumper = GameObject.FindGameObjectWithTag("Jumper"); 

            blower.transform.parent = jumper.transform;
            GameObject.Instantiate(UIManager);


          } else {
            if( GameObject.FindGameObjectWithTag("Jumper") && GameObject.FindGameObjectWithTag("Blower")) isTwoPlayerExisted = true;
          }
        }

    }

}
