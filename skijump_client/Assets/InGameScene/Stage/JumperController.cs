using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {
    int clickCounter = 0;
    public bool isCollidedWithDistanceBasePoint = false;
    public bool isDistanceMeasured = false;

    float distance;

    GameObject jumper;
    GameObject mainCam;
    GameObject distanceBasePoint;
    Vector3 addedForce;
    Vector3 jumpModifier = new Vector3(0,3000f,0);
    Rigidbody rbJumper;

    // Start is called before the first frame update
    void Start() {
        Physics.gravity = new Vector3(0,-30f,0);
        rbJumper = GetComponent<Rigidbody>();
        mainCam = Camera.main.gameObject;

        clickCounter = 0;

        jumper = GameObject.Find("Jumper");
        distanceBasePoint = GameObject.Find("DistanceBasePoint");

        addedForce = new Vector3((float)clickCounter,0,0);
    }

    // Update is called once per frame
    void Update() {
        if (!isCollidedWithDistanceBasePoint) {
            AccelerateJumper();
        }

        if (isDistanceMeasured) {
            DeccelerateJumper();
        }
    }

    void OnCollisionEnter(Collision collision) {
       if(collision.gameObject.name == "MeasureStage" && !isDistanceMeasured) {
            distance = Vector3.Distance(distanceBasePoint.transform.position, jumper.transform.position);
            RotateMainCamWithEulerAngle("x", 30);
            isDistanceMeasured = true;
        }

    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "DistanceBasePoint") {
            isCollidedWithDistanceBasePoint = true;
            RotateMainCamWithEulerAngle("x", 45);
            rbJumper.AddForce(jumpModifier);
            AutoDeccelerateJumperByJump();
        }

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
        if (direction == "x") {
            mainCam.transform.eulerAngles = new Vector3(Mathf.LerpAngle(mainCam.transform.eulerAngles.x, degree,Time.deltaTime),0,0);
        }

    }

    void AutoDeccelerateJumperByJump(){
            float xSpeed = rbJumper.velocity.z;
            rbJumper.AddForce( new Vector3(0,0,-xSpeed*35), ForceMode.Acceleration);
    }

    void DeccelerateJumper() {
        
    }
}
