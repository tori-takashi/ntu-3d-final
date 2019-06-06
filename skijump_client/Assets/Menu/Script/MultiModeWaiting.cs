using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiModeWaiting : MonoBehaviour
{
    public Button cancelButton;

    string gameVersion = "1";

    int jumpOrder;

    public RoomInfo[] roomList;

    void Awake() {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    void Start() {
        Button cancelButtonComponent = cancelButton.GetComponent<Button>();
        cancelButtonComponent.onClick.AddListener(Cancel);
    }

    void Update() {
        
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

    void Cancel() {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
}
