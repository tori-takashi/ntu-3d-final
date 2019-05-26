using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {
    int clickCounter = 0;
    bool isJumperCollided = false;

    float distance;

    GameObject jumper;
    GameObject measureStage;
    Vector3 addedForce;
    Rigidbody rbJumper;
    // Start is called before the first frame update
    void Start() {
        clickCounter = 0;

        jumper = GameObject.Find("Jumper");
        measureStage = GameObject.Find("MeasureStage");

        rbJumper = GetComponent<Rigidbody>();
        addedForce = new Vector3((float)clickCounter,0,0);
    }

    // Update is called once per frame
    void Update() {
        if (!isJumperCollided) {
            AccelerateJumper();
        }
    }

    void OnCollisionEnter(Collision collision) {
       if(collision.gameObject.name == "MeasureStage") {
            distance = Vector3.Distance(measureStage.transform.position, jumper.transform.position);
            Debug.Log(distance);
        }

        if (collision.gameObject.name == "Jumper") {
            isJumperCollided = true;
        }
    }

    void AccelerateJumper(){
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) {
            clickCounter++;
            addedForce.x = (float)clickCounter;
        }
        rbJumper.AddForce(addedForce, ForceMode.Acceleration);
    }
}
