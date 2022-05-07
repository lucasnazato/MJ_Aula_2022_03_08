using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    public AudioClip fire1;
    public AudioClip fire2;

    AudioSource audioCannon;

    PhotonView view;

    MyPlayer player;

    public float timeToShoot = 1f;
    float time = 0;

    private void Start()
    {
        view = GetComponentInParent<PhotonView>();
        audioCannon = GetComponent<AudioSource>();
        player = view.GetComponentInParent<MyPlayer>();
    }

    private void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        time += Time.deltaTime;

        if (Input.GetButtonDown("Fire") && time > timeToShoot)
        {
            view.RPC("InstatiateBullet", RpcTarget.All);
            time = 0;
        }
    }

    [PunRPC]

    private void InstatiateBullet()
    {
        GameObject temp = Instantiate(bullet, this.gameObject.transform.position, this.gameObject.transform.rotation);
        temp.GetComponent<Bullet>().player = transform.parent.gameObject.GetComponent<MyPlayer>();

        temp.GetComponent<Damage>().player = player;

        audioCannon.PlayOneShot(fire1);
        audioCannon.PlayOneShot(fire2);
    }
}
