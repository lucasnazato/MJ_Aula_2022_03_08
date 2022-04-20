using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public GameObject player;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // ============ PHOTON ==============
    public override void OnConnected()
    {
        print("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server");

        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("OnJoinRandomFailed");

        string roomName = "jao_" + Random.Range(0, 99);
        PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}

