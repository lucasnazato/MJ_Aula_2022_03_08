using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Network2 : MonoBehaviourPunCallbacks
{
    public GameObject player;

    private void Start()
    {
        Login();
    }

    // ================================================
    // HELPERS
    // ================================================
    public void Login()
    {
        print("##################### LOGIN ##################");
        if (!PhotonNetwork.IsConnected)
        {
            //PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ================================================
    // PUN callbacks
    // ================================================

    public override void OnConnectedToMaster()
    {
        print("Connected to server");

        PhotonNetwork.JoinLobby();
        PhotonNetwork.NickName = "Sanic";
    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("OnJoinRandomFailed: " + message);
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Facens", opt);
    }

    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");

        print("Nome da sala: " + PhotonNetwork.CurrentRoom.Name);
        print("Players conectados: " + PhotonNetwork.CurrentRoom.PlayerCount);

        //pnLobby.SetActive(false);
        foreach (Player nick in PhotonNetwork.PlayerList)
        {
            print("PlayerList: " + nick.NickName);
        }

        Hashtable myHash = new Hashtable();
        myHash.Add("score", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(myHash, null, null);

        //PhotonNetwork.LoadLevel("Elevator");
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}


