using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class ManagerHUD : MonoBehaviour
{
    public Image imgHealth;
    public TMP_Text txtScore;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void SetHealth(float value)
    {
        if (!view.IsMine)
        {
            imgHealth.fillAmount = value;
        }
    }

    public void SetScore(int value)
    {
        if (!view.IsMine)
        {
            txtScore.text = value.ToString();
        }
    }
}
