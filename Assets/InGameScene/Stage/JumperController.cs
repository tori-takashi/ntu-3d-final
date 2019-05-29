using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {
    public int clickCounter = 0;
    public bool isCollidedWithJumpRamp = false;
    public bool isDistanceMeasured = false;

    float distance;

    GameObject jumper;
    GameObject measureStage;
    Vector3 addedForce;
    Rigidbody rbJumper;

    // Start is called before the first frame update
    void Start() {
        Physics.gravity = new Vector3(0,-30f,0);
        rbJumper = GetComponent<Rigidbody>();

        clickCounter = 0;

        jumper = GameObject.Find("Jumper");
        measureStage = GameObject.Find("MeasureStage");

        addedForce = new Vector3((float)clickCounter,0,0);
    }

    // Update is called once per frame
    void Update() {
        if (!isCollidedWithJumpRamp) {
            AccelerateJumper();
        }
    }

    void OnCollisionEnter(Collision collision) {
       if(collision.gameObject.name == "MeasureStage" && !isDistanceMeasured) {
            distance = Vector3.Distance(measureStage.transform.position, jumper.transform.position);
            isDistanceMeasured = true;
        }

    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "JumpRamp") {
            isCollidedWithJumpRamp = true;
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
}
