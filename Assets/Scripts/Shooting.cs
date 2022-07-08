using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public GameObject GunRotation;
    private Vector2 mousePos;

    public float bulletForce;
    public GameObject GunAmmo;
    public Transform GunBarrel;
    


    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - new Vector2(GunRotation.transform.position.x, GunRotation.transform.position.y);
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg;
        GunRotation.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        
        
    }

    private void Shoot()
    {
        GameObject Balle = Instantiate(GunAmmo, GunBarrel.position, GunBarrel.rotation);
        Rigidbody2D rb = Balle.GetComponent<Rigidbody2D>();
        rb.AddForce(GunBarrel.up * bulletForce, ForceMode2D.Impulse);
    }
}
