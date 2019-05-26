using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    float distance;
    GameObject jumper;
    GameObject measureStage;

    // Start is called before the first frame update
    void Start()
    {
        jumper = GameObject.Find("Jumper");
        measureStage = GameObject.Find("MeasureStage");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.name == "Jumper") {
            distance = Vector3.Distance(measureStage.transform.position, jumper.transform.position);
            Debug.Log(distance);
        }
    }
}
