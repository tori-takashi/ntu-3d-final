using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumperController : MonoBehaviour, IPunObservable
{

    public bool isCollidedWithDistanceBasePoint = false;
    public float distance;
    public bool isDistanceMeasured = false;
    bool isChangedConstraints = false;
    int clickCounter = 0;
    float speed;
    bool isMultiMode;

    GameObject jumper;

    public GameObject UIManagerForSingleMode;
    public GameObject mainCam;
    GameObject mainCamObject;

    GameObject distanceBasePoint;

    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;
    GameObject playerControllerObject;
    PlayerController playerController;

    Vector3 addedForce;
    Vector3 jumpModifier = new Vector3(0,3000f,0);

    PhotonView photonView;
    
    public Rigidbody rbJumper;

    void Start()
    {
        playerControllerObject = GameObject.Find("PlayerController");
        playerController = playerControllerObject.GetComponent<PlayerController>();
        Debug.Log(playerController);
        
        isMultiMode = playerController.isMultiMode;
        clickCounter = 0;

        jumper = GameObject.FindGameObjectWithTag("Jumper");

        rbJumper = GetComponent<Rigidbody>();
        addedForce = new Vector3((float)clickCounter,0,0);

        distanceBasePoint = GameObject.Find("DistanceBasePoint");

        if(playerController.myRole == "Jumper") {
            mainCamObject = GameObject.Instantiate(mainCam);
            mainCamObject.transform.parent = jumper.transform;
            mainCamObject.transform.position = new Vector3(0f, 86.5f, 199f);
            mainCamObject.transform.rotation = Quaternion.Euler(new Vector3(30f,0,0));
        }

        if(isMultiMode) {
            // for multi mode
            multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
            multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

            photonView = this.GetComponent<PhotonView>();
        } else {
            // for single mode
            GameObject.Instantiate(UIManagerForSingleMode);
        }

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

    void Update() {
        if(playerController.isCountDownFinished) {
            ClearCountDownConstraints();
            speed = rbJumper.velocity.magnitude;

            if (isJumperSliding() ) AccelerateJumper();
            if (getDistance() != 0) SetResultOnlyMultiMode();
            
        } else {
            CountDownConstraints();
        }
    }

    void SetResultOnlyMultiMode() {
        if(isMultiMode){
            if (multiModeGameManager.currentJumper == 1) multiModeGameManager.player1_result = distance;
            if (multiModeGameManager.currentJumper == 2) multiModeGameManager.player2_result = distance;
        }
    }

    void ClearCountDownConstraints(){
      if(!isChangedConstraints){
        rbJumper.constraints = RigidbodyConstraints.None;
        rbJumper.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        isChangedConstraints = true;
      }
    }

    void CountDownConstraints() {
      rbJumper.constraints = RigidbodyConstraints.FreezeAll;
    }

    bool isJumperSliding() {
        if (isMultiMode) {
            return isJumperSlidingOnMultiMode();
        } else {
            return isJumperSlidingOnSingleMode();
        }
    }

    bool isJumperSlidingOnMultiMode (){
      return !isCollidedWithDistanceBasePoint && multiModeGameManager.currentJumper == multiModeGameManager.myJumpOrder;
    }

    bool isJumperSlidingOnSingleMode() {
      return !isCollidedWithDistanceBasePoint;
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
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Accelerated");
            clickCounter++;
            addedForce.z = (float)clickCounter;
        }
        rbJumper.AddForce(addedForce, ForceMode.Acceleration);
    }

    void RotateMainCamWithEulerAngle(string direction, int degree) {
        //Add Y and Z later
        //[FIX ME] cannnot rotate
        /* if (direction == "x") {
            mainCamObject.transform.eulerAngles = new Vector3(Mathf.LerpAngle(mainCam.transform.eulerAngles.x, degree,Time.deltaTime),0,0);
        }*/

    }

    void AutoDeccelerateJumperByJump(){
            float xSpeed = rbJumper.velocity.z;
            rbJumper.AddForce( new Vector3(0,0,-xSpeed*35), ForceMode.Acceleration);
    }

}
