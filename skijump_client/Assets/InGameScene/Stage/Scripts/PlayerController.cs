﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public bool isMultiMode = false;
    public bool isCountDownFinished = false;
    
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
                if(multiModeGameManager.currentJumper == 1) {
                    PhotonNetwork.Instantiate("Jumper_1", new Vector3(0f, 79.5f, 204f), Quaternion.identity, 0);
                } else {
                    PhotonNetwork.Instantiate("Jumper_2", new Vector3(0f, 79.5f, 204f), Quaternion.identity, 0);
                }
            } else {
                myRole = "Blower";
                PhotonNetwork.Instantiate("Blower", new Vector3(0f,82f,214f), Quaternion.identity, 0);
            }
        }

    }


    void Update() {
        if(isMultiMode && !isInstantiatedTwoPlayers){
          if (isTwoPlayerExisted) {
            isInstantiatedTwoPlayers = true;

            blower = GameObject.FindGameObjectWithTag("Blower"); 
            BlowerController blowerController;
            blowerController = blower.GetComponent<BlowerController>();

            jumper = GameObject.FindGameObjectWithTag("Jumper"); 
            JumperController jumperController;
            jumperController = jumper.GetComponent<JumperController>();

            blowerController.jumperController = jumperController;

            blower.transform.parent = jumper.transform;
            GameObject.Instantiate(UIManager);

          } else {
            if( GameObject.FindGameObjectWithTag("Jumper") && GameObject.FindGameObjectWithTag("Blower")) isTwoPlayerExisted = true;
          }
        }

    }

}
