using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class Health : MonoBehaviour
{
    PhotonView view;
    public float health = 100;
    public float maxHealth = 100;
    public Image imgHealth;
    public Image imgPlayerHealth;
    MyPlayer player;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        player = GetComponent<MyPlayer>();
    }

    public void UpdateHealth(float value)
    {
        health += value;
        imgHealth.fillAmount = health / maxHealth;
        imgPlayerHealth.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            player.Die();
        }
    }
}
