using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougerBalle : MonoBehaviour
{
    public GameObject hitEffect;

    public void ShootBullet(Vector2 shootingDirection)
    {
        GetComponent<Rigidbody2D>().AddForce(shootingDirection * 10, ForceMode2D.Impulse);
        print("test");
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);
        Destroy(gameObject);
    }
}
