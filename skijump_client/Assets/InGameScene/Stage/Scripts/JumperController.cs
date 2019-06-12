using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour, IPunObservable
{

    public bool isCollidedWithDistanceBasePoint = false;
    public float distance;
    public bool isDistanceMeasured = false;
    int clickCounter = 0;
    float speed;

    GameObject jumper;

    public GameObject mainCam;
    GameObject mainCamObject;

    GameObject distanceBasePoint;

    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;


    Vector3 addedForce;
    Vector3 jumpModifier = new Vector3(0,3000f,0);
    
    Rigidbody rbJumper;

    void Start()
    {
        clickCounter = 0;

        jumper = GameObject.FindGameObjectWithTag("Jumper");

        rbJumper = GetComponent<Rigidbody>();
        addedForce = new Vector3((float)clickCounter,0,0);

        multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
        multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

        distanceBasePoint = GameObject.Find("DistanceBasePoint");

        mainCamObject = GameObject.Instantiate(mainCam);
        mainCamObject.transform.parent = jumper.transform;
        //mainCamObject.transform.position = new Vector3(0f, 10f, -3f);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.isWriting) {
            stream.SendNext(speed);
            stream.SendNext(distance);
        } else {
            speed = (float)stream.ReceiveNext();
            distance = (float)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!isCollidedWithDistanceBasePoint && multiModeGameManager.currentJumper == multiModeGameManager.myJumpOrder) {
            AccelerateJumper();
        }

        if(getDistance() != 0) {
          if (multiModeGameManager.currentJumper == 1) multiModeGameManager.player1_result = distance;
          if (multiModeGameManager.currentJumper == 2) multiModeGameManager.player2_result = distance;
        }

        speed = rbJumper.velocity.magnitude;
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
        return speed;
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
            mainCamObject.transform.eulerAngles = new Vector3(Mathf.LerpAngle(mainCam.transform.eulerAngles.x, degree,Time.deltaTime),0,0);
        }

    }

    void AutoDeccelerateJumperByJump(){
            float xSpeed = rbJumper.velocity.z;
            rbJumper.AddForce( new Vector3(0,0,-xSpeed*35), ForceMode.Acceleration);
    }

}
