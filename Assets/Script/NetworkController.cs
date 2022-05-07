using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("LOGIN")]
    public GameObject pnLogin;
    public TMP_InputField iNickname;
    public string nickname;
    public Button btnLogin;

    [Header("LOBBY")]
    public GameObject pnLobby;
    public TMP_InputField iRoomName;
    public string roomName;

    private void Start()
    {
        pnLogin.SetActive(true);
        pnLobby.SetActive(false);

        if (PlayerPrefs.HasKey("user"))
        {
            iNickname.text = PlayerPrefs.GetString("user");
        }
    }

    public void Login()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        btnLogin.interactable = false;
    }

    public void JoinRandomRoom()
    {
        print("BUSCAR PARTIDA...");
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        print("Create Room");
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(iRoomName.text, opt, TypedLobby.Default, null);
    }


    // Photon
    public override void OnConnected()
    {
        print("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        PhotonNetwork.NickName = iNickname.text;

        pnLogin.SetActive(false);
        pnLobby.SetActive(true);

        PlayerPrefs.SetString("user", iNickname.text);
    }

    public override void OnJoinedLobby()
    {
        print("OnJoinedLobby");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("OnJoinRandomFailed");

        string roomName = "Sala_" + Random.Range(0, 99);
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Facens", opt);
    }

    public override void OnJoinedRoom()
    {
        print("OnJoinedRoom");

        pnLobby.SetActive(false);

        PhotonNetwork.LoadLevel("DesertTank");

        Hashtable myHash = new Hashtable();
        myHash.Add("score", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(myHash, null, null);
    }
}