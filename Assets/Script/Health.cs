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
    MyPlayer player;
    ManagerHUD hud;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        player = GetComponent<MyPlayer>();
        hud = GameObject.FindObjectOfType<ManagerHUD>();
    }

    public void UpdateHealth(float value)
    {
        health += value;
        imgHealth.fillAmount = health / maxHealth;

        hud.SetHealth(health / maxHealth);

        if (health <= 0)
        {
            player.Die();
        }
    }
}
