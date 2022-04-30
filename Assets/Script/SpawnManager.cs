using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity);
    }
}
