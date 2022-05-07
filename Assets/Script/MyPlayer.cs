using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MyPlayer : MonoBehaviourPun, IPunObservable
{
    public float force = 100;
    public float torque = 100;

    public GameObject explosionFX;
    public GameObject canon;

    Rigidbody rb;
    AudioSource audioPlayer;
    public PhotonView view;
    public Camera cam;

    public AudioClip motorIdle;
    public AudioClip tankMoving;
    public AudioClip onFire;

    float hor;
    float ver;

    bool isPlayerDead = false;
    bool isIdle = true;

    public GameObject[] arrayPrefabs;
    public GameObject tankPrefab;

    public TMP_Text txtName;

    GameObject[] spawnPoints;

    Network2 netWork;

    public TMP_Text txtScore;
    public TMP_Text txtPlayerScore;

    public MyPlayer enemyPlayer;

    bool updatePlayerPrefab = false;

    void Start()
    {
        txtName.text = view.Owner.NickName;

        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
        netWork = FindObjectOfType<Network2>();

        object tmp;
        if (view.Owner.CustomProperties.TryGetValue("score", out tmp))
        {
            txtScore.text = tmp.ToString();
            txtPlayerScore.text = tmp.ToString();
        }

        if (!view.IsMine)
        {
            cam.enabled = false;
            return;
        }
        else
        {
            cam.enabled = true;
        }

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;

        view.RPC("ChangePlayerPrefab", RpcTarget.All);
    }

    [PunRPC]

    private void ChangePlayerPrefab()
    {
        int playerNumber = view.Owner.ActorNumber - 1;
        tankPrefab.GetComponent<MeshRenderer>().material = arrayPrefabs[playerNumber].GetComponent<MeshRenderer>().sharedMaterial;
        tankPrefab.GetComponent<MeshFilter>().mesh = arrayPrefabs[playerNumber].GetComponent<MeshFilter>().sharedMesh;
    }

    void Update()
    {
        if (!updatePlayerPrefab)
        {
            view.RPC("ChangePlayerPrefab", RpcTarget.All);
            updatePlayerPrefab = true;
        }

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

        if (!isPlayerDead)
        {
            Vector3 dir = transform.forward * force * ver;
            rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);

            float angle = transform.rotation.eulerAngles.y;
            rb.MoveRotation(Quaternion.Euler(0, angle + (hor * torque), 0));

            rb.AddForce(transform.forward * force * ver);

            if (hor != 0 || ver != 0)
            {
                if (!isIdle)
                {
                    audioPlayer.clip = tankMoving;
                    audioPlayer.Play();
                    audioPlayer.volume = 0.3f;
                    isIdle = true;
                }
            }
            else if (hor == 0 && ver == 0)
            {
                if (isIdle)
                {
                    audioPlayer.clip = motorIdle;
                    audioPlayer.Play();
                    audioPlayer.volume = 0.1f;
                    isIdle = false;
                }
            }
        }
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

    public void AddScore(int value)
    {
        if (view.IsMine)
        {
            view.RPC("AddScoreNet", RpcTarget.All, value);
        }
    }

    [PunRPC]

    private void AddScoreNet(int value)
    {
        int scoreTmp = (int)view.Owner.CustomProperties["score"];
        scoreTmp += value;

        view.Owner.CustomProperties["score"] = scoreTmp;
        txtScore.text = view.Owner.CustomProperties["score"].ToString();
        txtPlayerScore.text = view.Owner.CustomProperties["score"].ToString();
    }

    public void Die()
    {
        GameObject temp = Instantiate(explosionFX, gameObject.transform.position, gameObject.transform.rotation, transform);
        temp.transform.localScale = new Vector3(6, 6, 6);

        audioPlayer.clip = onFire;
        audioPlayer.Play();
        audioPlayer.volume = 0.5f;

        canon.GetComponent<Shoot>().enabled = false;

        isPlayerDead = true;

        enemyPlayer.AddScore(1);

        Destroy(gameObject, 5);
    }

    private void OnDestroy()
    {
        if (!view.IsMine)
        {
            return;
        }
        netWork.CreatePlayer();
    }
}
