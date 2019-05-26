using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLabel : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.UI.Text distanceLabel;

    GameObject jumper;
    JumperController jumperController;
    void Start()
    {
        distanceLabel = GetComponent<UnityEngine.UI.Text>();

        jumper = GameObject.Find("Jumper");
        jumperController = jumper.GetComponent<JumperController>();
        //access to get a distance that jumper object has
    }

    // Update is called once per frame
    void Update()
    {
        if(jumperController.getDistance() != 0) {
            distanceLabel.text = jumperController.getDistance().ToString() + "m";
        }
    }
}
