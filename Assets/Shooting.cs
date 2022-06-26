using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public GameObject GunRotation;
    private Vector2 mousePos;


    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - new Vector2(GunRotation.transform.position.x, GunRotation.transform.position.y);
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg;
        GunRotation.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        

    }
}
