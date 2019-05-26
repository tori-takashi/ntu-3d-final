using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {
    int clickCounter = 0;
    bool isCollidedWithJumpRamp = false;

    float distance;

    GameObject jumper;
    GameObject measureStage;
    Vector3 addedForce;
    Rigidbody rbJumper;

    // Start is called before the first frame update
    void Start() {
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
       if(collision.gameObject.name == "MeasureStage") {
            distance = Vector3.Distance(measureStage.transform.position, jumper.transform.position);
        }

        if (collision.gameObject.name == "JumpRamp") {
            isCollidedWithJumpRamp = true;
        }
    }

    public float getDistance() {
        return distance;
    }

    void AccelerateJumper() {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) {
            clickCounter++;
            addedForce.x = (float)clickCounter;
        }
        rbJumper.AddForce(addedForce, ForceMode.Acceleration);
    }
}
