using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public Gun gun;
    [SerializeField] private GameObject GrabButton;
    private Mouvement Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MyPlayer")
        {
            Player = collision.gameObject.GetComponent<Mouvement>();
            Player.CanPickItem(gun);
            GrabButton.SetActive(true);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MyPlayer")
        {
            GrabButton.SetActive(false);
            
        }
    }

}
