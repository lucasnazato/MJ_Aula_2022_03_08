using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public enum EnumPlayer
{
    Player1 = 1,
    Player2 = 2
}

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public PlayerData_SO data;
    public GameData_SO gamedata;
    public EnumPlayer numPlayer;
    
    public float force = 100;
    public float torque = 100;

    public GameObject explosionFX;
    MyPlayer playerScript;
    public GameObject canon;

    Rigidbody rb;
    AudioSource audioPlayer;
    PhotonView view;
    public Camera cam;

    public AudioClip motorIdle;
    public AudioClip tankMoving;
    public AudioClip onFire;

    float hor;
    float ver;

    bool isPlayerDead = false;
    bool isIdle = true;

    float health = 100;

    public GameObject[] arrayPrefabs;
    public GameObject playerPrefab;

    public TMP_Text txtName;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = GetComponent<MyPlayer>();
        audioPlayer = GetComponent<AudioSource>();
        view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            cam.enabled = false;
            return;
        }
        else
        {
            cam.enabled = true;
        }

        txtName.text = view.Owner.NickName;

        int playerNumber = view.Owner.ActorNumber - 1;

        playerPrefab.GetComponent<MeshFilter>().mesh = arrayPrefabs[playerNumber].GetComponent<MeshFilter>().sharedMesh;
        playerPrefab.GetComponent<MeshRenderer>().material = arrayPrefabs[playerNumber].GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!view.IsMine)
        {
            return;
        }

        Vector3 dir = transform.forward * force * ver;
        rb.velocity =  new Vector3 (dir.x, rb.velocity.y, dir.z);

        float angle = transform.rotation.eulerAngles.y;
        rb.MoveRotation(Quaternion.Euler(0, angle + (hor * torque), 0));

        rb.AddForce(transform.forward * force * ver);

        if (hor != 0 || ver != 0)
        {
            if (!isIdle)
            {
                audioPlayer.clip = tankMoving;
                audioPlayer.Play();
                audioPlayer.volume = 0.6f;
                isIdle = true;
            }
        }
        else if (hor == 0 && ver == 0)
        {
            if (isIdle)
            {
                audioPlayer.clip = motorIdle;
                audioPlayer.Play();
                audioPlayer.volume = 0.2f;
                isIdle = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet") && !isPlayerDead)
        {
            health -= 25;
            health = Mathf.Clamp(health, 0, 100);

            SetHealth(health);

            if (health <= 0)
            {
                GameObject temp = Instantiate(explosionFX, gameObject.transform.position, gameObject.transform.rotation);
                temp.transform.localScale = new Vector3(6, 6, 6);

                audioPlayer.clip = onFire;
                audioPlayer.Play();
                audioPlayer.volume = 0.95f;

                playerScript.enabled = false;
                canon.GetComponent<Shoot>().enabled = false;

                collision.gameObject.GetComponent<Bullet>().player.AddPoint(1);

                isPlayerDead = true;
            }
        }
    }

    public void AddPoint(int value)
    {
        data.score += value;
        gamedata.onUpdateHUD.Invoke();
    }

    public void SetHealth(float value)
    {
        data.health = value;
        gamedata.onUpdateHUD.Invoke();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
