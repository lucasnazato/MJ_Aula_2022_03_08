using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public GameObject player;

    [Header("LOGIN")]
    public GameObject pnLogin;
    public TMP_InputField ifNickname;
    string nickname;
    public Button btnLogin;

    [Header("LOBBY")]
    public GameObject pnLobby;
    public TMP_InputField ifRoomName;
    string roomName;
    public Button btnEntrarSala;
    public Button btnCriarSala;

    private void Start()
    {
        pnLogin.SetActive(true);
        pnLobby.SetActive(false);

        if (PlayerPrefs.HasKey("user"))
        {
            ifNickname.text = PlayerPrefs.GetString("user");
        }
    }


    // ================= HELPERS =======================
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
        print("######## BUSCAR PARTIDA ########");
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        print("######## CRIAR PARTIDA ########");
        RoomOptions opt = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(ifRoomName.text, opt, TypedLobby.Default, null);
    }



    // ============ PHOTON ==============
    public override void OnConnected()
    {
        print("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        PhotonNetwork.NickName = ifNickname.text;

        pnLogin.SetActive(false);
        pnLobby.SetActive(true);

        PlayerPrefs.SetString("user", ifNickname.text);
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

        foreach (Player nick in PhotonNetwork.PlayerList)
        {
            print("PlayerList: " + nick.NickName);
        }

        //PhotonNetwork.LoadLevel("SampleScene");
        pnLobby.SetActive(false);
        //PhotonNetwork.Instantiate(player.name, Vector2.zero, Quaternion.identity);
        Hashtable myHash = new Hashtable();
        myHash.Add("score", 0);
        PhotonNetwork.LocalPlayer.SetCustomProperties(myHash, null, null);

        PhotonNetwork.LoadLevel("DesertTank");
        //CreatePlayer();
    }

    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}

