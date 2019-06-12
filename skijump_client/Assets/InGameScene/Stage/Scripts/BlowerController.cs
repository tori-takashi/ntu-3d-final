using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerController : MonoBehaviour
{

    public GameObject mainCam;
    GameObject mainCamObject;

    //int clickCounter = 0;
    public GameObject blower;
    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;
    
    void Start()
    {
        //clickCounter = 0;
        mainCamObject = GameObject.Instantiate(mainCam);
        blower = GameObject.FindGameObjectWithTag("Blower");
        mainCamObject.transform.parent = blower.transform;
        //mainCamObject.transform.position = new Vector3(0f, 2f, 3f);

        multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
        multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

    }

    void Update()
    {
        
    }
}
