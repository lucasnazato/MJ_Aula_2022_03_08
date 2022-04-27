using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LookAtCamera : MonoBehaviour
{
    PhotonView view;
    public TMP_Text txtName;

    private void Start()
    {
        view = GetComponentInParent<PhotonView>();

        if (view.IsMine)
        {
            txtName.enabled = false;
        }
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
