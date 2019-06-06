﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumperController : MonoBehaviour {
    public bool isCollidedWithDistanceBasePoint = false;
    public bool isMultiMode = false;
    
    int clickCounter = 0;
    float distance;
    bool isDistanceMeasured = false;

    GameObject jumper;
    GameObject mainCam;
    GameObject distanceBasePoint;

    Vector3 addedForce;
    Vector3 jumpModifier = new Vector3(0,3000f,0);
    
    Rigidbody rbJumper;

    void Start() {

        if (SceneManager.GetActiveScene().name == "MultiMode"){
            isMultiMode = true;
        }

        clickCounter = 0;

        rbJumper = GetComponent<Rigidbody>();
        mainCam = Camera.main.gameObject;

        jumper = GameObject.Find("Jumper");
        distanceBasePoint = GameObject.Find("DistanceBasePoint");

        Physics.gravity = new Vector3(0,-30f,0);
        addedForce = new Vector3((float)clickCounter,0,0);

    }

    void Update() {
        if (!isCollidedWithDistanceBasePoint) {
            AccelerateJumper();
        }
    }

    void OnCollisionEnter(Collision collision) {
       if(collision.gameObject.name == "MeasureStage" && !isDistanceMeasured) {
            distance = MeasureDistance();
            isDistanceMeasured = true;

            RotateMainCamWithEulerAngle("x", 30);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "DistanceBasePoint") {
            isCollidedWithDistanceBasePoint = true;
            RotateMainCamWithEulerAngle("x", 45);
            AutoDeccelerateJumperByJump();

            rbJumper.AddForce(jumpModifier);
        }

        if(collider.gameObject.name == "ChangeCameraDegreePoint") {
            RotateMainCamWithEulerAngle("x", 90);
        }
    }

    float MeasureDistance(){
        return Vector3.Distance(distanceBasePoint.transform.position, jumper.transform.position);
    }

    public float getDistance() {
        return distance;
    }

    public float getSpeed() {
        return rbJumper.velocity.magnitude;
    }

    void AccelerateJumper() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) {
            clickCounter++;
            addedForce.z = (float)clickCounter;
        }
        rbJumper.AddForce(addedForce, ForceMode.Acceleration);
    }

    void RotateMainCamWithEulerAngle(string direction, int degree) {
        //Add Y and Z later
        //[FIX ME] cannnot rotate
        if (direction == "x") {
            mainCam.transform.eulerAngles = new Vector3(Mathf.LerpAngle(mainCam.transform.eulerAngles.x, degree,Time.deltaTime),0,0);
            Debug.Log(mainCam.transform.rotation);
        }

    }

    void AutoDeccelerateJumperByJump(){
            float xSpeed = rbJumper.velocity.z;
            rbJumper.AddForce( new Vector3(0,0,-xSpeed*35), ForceMode.Acceleration);
    }

}