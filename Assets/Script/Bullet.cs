using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float Force = 100;
    public GameObject explosionParticle;

    public Player player;

    AudioSource audioBullet;
    SphereCollider capCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioBullet = GetComponent<AudioSource>();
        capCollider = GetComponent<SphereCollider>();

        rb.AddForce(transform.forward * Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject temp = PhotonNetwork.Instantiate(explosionParticle.name, this.gameObject.transform.position, this.gameObject.transform.rotation);

        audioBullet.Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        capCollider.enabled = false;

        Destroy(temp, 3);
        Destroy(gameObject, 5);
    }
}
