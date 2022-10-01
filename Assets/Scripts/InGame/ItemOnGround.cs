using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    private ItemInfo iteminfo;
    [SerializeField] private GameObject GrabButton;
    private SpriteRenderer itemFrame;
    private Mouvement Player;

    private void Setup()
    {
        if (iteminfo == null)
        {
            Debug.Log("Bug de Map: Aucun Item dans un ItemHolder");
            gameObject.SetActive(false);
            return;
        }
        itemFrame = GetComponentInChildren<SpriteRenderer>();
        itemFrame.sprite = iteminfo.itemSprite;  
    }

    public void GiveItem(ItemInfo newItemInfo)
    {
        iteminfo = newItemInfo;
        
        Setup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MyPlayer")
        {
            Player = collision.gameObject.GetComponent<Mouvement>();
            Player.CanPickItem(iteminfo, this);
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

    public void ItemHasBeenPicked()
    {
        Destroy(this.gameObject);
    }

}
