using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiModeGameManager : MonoBehaviour, IPunObservable
{
    public int myJumpOrder;
    public int currentJumper;
    public int jumpers;
    public bool gameAborted;

    public float player1_result;
    public float player2_result;

    void Start() {
        currentJumper = 1;
        jumpers = 2;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadArena() {
        PhotonNetwork.LoadLevel("MultiMode");
    }

    void OnPhotonPlayerConnected() {
        if(PhotonNetwork.playerList.Length == 2) {
            PhotonNetwork.room.IsOpen = false;
            PhotonNetwork.room.IsVisible = false;
            LoadArena();
        }
    }

    public void OnPhotonPlayerDisconnected() {
        gameAborted = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.isWriting) {
            stream.SendNext(currentJumper);
        } else {
            currentJumper = (int)stream.ReceiveNext();
        }
    }


    public bool isDeterminedWinOrLose() {
        if (player1_result != 0 && player2_result != 0) return true;
        return false;
    }

    void OnLeftRoom() {
        Destroy(this.gameObject);
    }

    void OnJoinedRoom() {
       myJumpOrder = PhotonNetwork.playerList.Length;
    }

}
