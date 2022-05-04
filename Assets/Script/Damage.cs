using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damage : MonoBehaviour
{
    public MyPlayer player;

    private void OnCollisionEnter(Collision collision)
    {
        Health health;

        if (collision.collider.TryGetComponent<Health>(out health))
        {
            health.UpdateHealth(-10);

            collision.gameObject.GetComponent<MyPlayer>().enemyPlayer = player;

            Destroy(gameObject);
        }
    }
}
