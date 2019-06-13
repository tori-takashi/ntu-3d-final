using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public RoomInfo[] roomList;

    string gameVersion = "1";

    void Start() {
        DontDestroyOnLoad(this.gameObject);

        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    void OnPhotonRandomJoinFailed() {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public void Disconnect() {
        PhotonNetwork.LeaveRoom();
        // destroy with multimodegamemanager
        PhotonNetwork.Disconnect();
        Destroy(this.gameObject);
    }

    void OnReceivedRoomListUpdate() {
        roomList = PhotonNetwork.GetRoomList();
    }

    void OnJoinedLobby() {
        roomList = PhotonNetwork.GetRoomList();
        PhotonNetwork.JoinRandomRoom();
    }

    void OnFailedToConnectToPhoton() {
        Debug.Log("Connection Failed. Check your network status");
    }

}
