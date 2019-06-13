using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiModeWaiting : MonoBehaviour
{
    GameObject networkManagerObject;
    NetworkManager networkManager;
    GameObject multiModeGameManagerObject;
    MultiModeGameManager multiModeGameManager;

    public Button cancelButton;

    void Start() {
        Button cancelButtonComponent = cancelButton.GetComponent<Button>();
        cancelButtonComponent.onClick.AddListener(Cancel);

        networkManagerObject = GameObject.Find("NetworkManager");
        networkManager = networkManagerObject.GetComponent<NetworkManager>();
    }

    void Cancel() {
        networkManager.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
