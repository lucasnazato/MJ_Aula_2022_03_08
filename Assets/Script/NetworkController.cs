using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [Header("LOGIN")]
    public GameObject pnLogin;
    public TMP_InputField iNickname;
    string nickname;
    public Button btnLogin;

    [Header("LOBBY")]
    public GameObject pnLobby;
    public TMP_InputField iRoomName;
    string roomName;


    public GameObject player;
    private void Start()
    {
        pnLogin.SetActive(true);
        pnLobby.SetActive(false);

        if (PlayerPrefs.HasKey("user"))
        {
            iNickname.text = PlayerPrefs.GetString("user");
        }
    }

    public override void OnConnected()
    {
        print("OnConnected");
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
        pnLobby.SetActive(false);
    }

    public void CreateRoom()
    {
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(iRoomName.text, opt, TypedLobby.Default, null);
        pnLobby.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinLobby();
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

        PhotonNetwork.LoadLevel("DesertTank");
        //PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);


        Hashtable myHash = new Hashtable();
        myHash.Add("score", 0);

        //PhotonNetwork.LocalPlayer.SetCustomProperties(myHash, null, null);

        //CreatePlayer();
    }

    /*public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }*/
}