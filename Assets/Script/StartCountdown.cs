using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public Text txtCountdown;
    public GameObject panel;
    float timer = 0;

    AudioSource audioTimer;

    private void Start()
    {
        audioTimer = GetComponent<AudioSource>();
        audioTimer.Play();

        txtCountdown.text = "3";
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
        }
        if (timer > 4.5f)
        {
            txtCountdown.enabled = false;
            Destroy(gameObject);
        }
    }
}
