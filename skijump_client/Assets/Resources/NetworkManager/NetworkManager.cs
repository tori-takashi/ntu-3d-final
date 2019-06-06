using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    string gameVersion = "1";

    public Button cancelButton;
    public int jumpOrder;

    public RoomInfo[] roomList;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
        jumpOrder = 1;

        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    void Start() {

        Button cancelButtonComponent = cancelButton.GetComponent<Button>();
        cancelButtonComponent.onClick.AddListener(Cancel);
    }

    void Update() {
        
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnMultiModeSceneLoaded;
    }

    void OnMultiModeSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "MultiMode") {
            Debug.Log("Scene Loaded!!!!");
        }
    }

    void LoadArena() {
        PhotonNetwork.LoadLevel("MultiMode");
    }

    void OnJoinedRoom() {
        Debug.Log("Joined Successfully");
    }

    void OnPhotonPlayerConnected() {
        if(PhotonNetwork.playerList.Length == 2) {
            LoadArena();
        }
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("OnPhotonRandomJoinFailed()");
        RoomOptions roomOptions = new RoomOptions();
        jumpOrder = 2;

        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    void OnReceivedRoomListUpdate() {
        Debug.Log("OnReceivedRoomListUpdate()");
        roomList = PhotonNetwork.GetRoomList();
        Debug.Log("updated RoomList↓");
        foreach (RoomInfo ro in roomList) {
            Debug.Log(ro);
        }
    }

    void OnJoinedLobby() {
        Debug.Log("Joined Lobby");
        roomList = PhotonNetwork.GetRoomList();
        Debug.Log("fetched RoomList");
        Debug.Log(roomList);
        Debug.Log("trying to join a room randomly...");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnFailedToConnectToPhoton() {
        Debug.Log("Connection Failed. Check your network status");
    }

    public int getJumpOrder() {
        return jumpOrder;
    }

    void Cancel() {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
