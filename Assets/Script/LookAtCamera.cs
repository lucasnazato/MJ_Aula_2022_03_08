using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    PhotonView view;

    public GameObject EnemyCanvas;
    public GameObject PlayerCanvas;

    private void Start()
    {
        view = GetComponentInParent<PhotonView>();

        if (view.IsMine)
        {
            EnemyCanvas.SetActive(false);
        }
        else
        {
            PlayerCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
