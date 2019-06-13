using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerController : MonoBehaviour
{

    public GameObject mainCam;
    GameObject mainCamObject;

    public int clickCounter = 0;
    public GameObject blower;
    public JumperController jumperController;
    //Defined in PlayerController

    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;
    GameObject playerControllerObject;
    PlayerController playerController;

    PhotonView photonView;

    Vector3 addedForce;
    
    void Start()
    {
        multiModeGameManagerObject = GameObject.Find("MultiModeGameManager");
        multiModeGameManager = multiModeGameManagerObject.GetComponent<MultiModeGameManager>();

        playerControllerObject = GameObject.Find("PlayerController");
        playerController = playerControllerObject.GetComponent<PlayerController>();

        blower = GameObject.FindGameObjectWithTag("Blower");
        blower.transform.rotation = Quaternion.Euler(new Vector3(270f,30f,150f));
        
        clickCounter = 0;

        photonView = this.GetComponent<PhotonView>();

        if (playerController.myRole == "Blower") {
            mainCamObject = GameObject.Instantiate(mainCam);
            mainCamObject.transform.parent = blower.transform;
            mainCamObject.transform.position = new Vector3(0f, 99.5f, 220f);
            mainCamObject.transform.rotation = Quaternion.Euler(new Vector3(30f,180f,0));
        }

    }

    void Update(){
        if (jumperController.isCollidedWithDistanceBasePoint && playerController.myRole == "Blower" && !jumperController.isDistanceMeasured) {
            if(Input.GetMouseButtonDown(0)) {
                photonView.RPC("DeccelerateJumper", PhotonTargets.All, null);
            }
        }
    }

    [PunRPC]
    void DeccelerateJumper() {
        Debug.Log("Remote Called");
        clickCounter++;
        addedForce.z = -1 * (float)clickCounter * 0.3f;
        jumperController.rbJumper.AddForce(addedForce, ForceMode.Acceleration);
    }
}
