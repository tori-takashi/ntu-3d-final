using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperController : MonoBehaviour {
    int clickCounter = 0;
    bool isJumperCollided = false;
    Vector3 addedForce;
    Rigidbody rbJumper;
    // Start is called before the first frame update
    void Start() {
        clickCounter = 0;

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
