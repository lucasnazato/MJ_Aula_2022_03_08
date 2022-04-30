using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameData_SO gameData;
    public CamManager hud;
    public MyPlayer player1;
    public MyPlayer player2;

    public Text txtCountdown;
    public GameObject panel;
    float timer = 0;

    AudioSource audioTimer;

    private void Start()
    {
        gameData.onUpdateHUD.AddListener(updateHUD);
        updateHUD();

        audioTimer = GetComponent<AudioSource>();
        audioTimer.Play();

        txtCountdown.text = "3";
        player1.force = 0;
        player1.torque = 0;
        player2.force = 0;
        player2.torque = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            txtCountdown.text = "2";
        }
        if (timer > 2)
        {
            txtCountdown.text = "1";
        }
        if (timer > 3)
        {
            txtCountdown.text = "Start!";
            player1.force = 10;
            player1.torque = 2;
            player2.force = 10;
            player2.torque = 2;
        }
        if (timer > 4.5f)
        {
            panel.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player1.enabled = true;
            player2.enabled = true;
            player1.SetHealth(100);
            player2.SetHealth(100);
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void updateHUD()
    {
        hud.UpdateScore(player1.data.score, player2.data.score);
        hud.UpdateHealth(player1.data.health, player2.data.health);
    }
}
