using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamManager : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;

    Camera cam1;
    Camera cam2;

    bool isSinglePlayer = false;
    bool isVertical = true;

    Rect cam1Rect = new Rect(0, 0, 0.5f, 1);
    Rect cam2Rect = new Rect(0.5f, 0, 1, 1);

    public Image panel2;

    public Slider healthBarP1;
    public Slider healthBarP2;

    public Text txtP1;
    public Text txtP2;

    void Start()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        cam1 = Player1.transform.Find("Camera").gameObject.GetComponent<Camera>();
        cam2 = Player2.transform.Find("Camera").gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isSinglePlayer)
            {
                if (isVertical)
                {
                    cam1Rect = new Rect(0, 0.5f, 1, 1);
                    cam2Rect = new Rect(0, 0, 1, 0.5f);
                    panel2.rectTransform.anchoredPosition = new Vector2(-77, -510);
                }
                else
                {
                    cam1Rect = new Rect(0, 0, 0.5f, 1);
                    cam2Rect = new Rect(0.5f, 0, 1, 1);
                    panel2.rectTransform.anchoredPosition = new Vector2(-77, -41);
                }
                isVertical = !isVertical;
                cam1.rect = cam1Rect;
                cam2.rect = cam2Rect;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cam1.rect = new Rect(0, 0, 1, 1);
            Player2.SetActive(false);
            isSinglePlayer = true;
            panel2.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cam1.rect = cam1Rect;
            cam2.rect = cam2Rect;
            Player2.SetActive(true);
            isSinglePlayer = false;
            panel2.gameObject.SetActive(true);
        }
    }

    public void UpdateScore(int p1, int p2)
    {
        txtP1.text = p1.ToString();
        txtP2.text = p2.ToString();
    }

    public void UpdateHealth(float p1, float p2)
    {
        healthBarP1.value = p1;
        healthBarP2.value = p2;
    }
}
