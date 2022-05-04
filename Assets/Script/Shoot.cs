using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public EnumPlayer numPlayer;

    public AudioClip fire1;
    public AudioClip fire2;

    AudioSource audioCannon;

    PhotonView view;

    MyPlayer player;

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

        if (Input.GetButtonDown("Fire" + (int)numPlayer))
        {
            GameObject temp = PhotonNetwork.Instantiate(bullet.name, this.gameObject.transform.position, this.gameObject.transform.rotation);
            temp.GetComponent<Bullet>().player = transform.parent.gameObject.GetComponent<MyPlayer>();

            temp.GetComponent<Damage>().player = player;

            audioCannon.PlayOneShot(fire1);
            audioCannon.PlayOneShot(fire2);
        }
    }
}
