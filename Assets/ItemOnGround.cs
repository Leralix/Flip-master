using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    public Item item;
    [SerializeField] private GameObject GrabButton;
    private SpriteRenderer itemFrame;
    private Mouvement Player;

    private void Setup()
    {
        if (item == null)
        {
            Debug.Log("Bug de Map: Aucun Item dans un ItemHolder");
            gameObject.SetActive(false);
            return;
        }
        itemFrame = GetComponentInChildren<SpriteRenderer>();
        itemFrame.sprite = item.itemSprite.sprite;

    }

    public void GiveItem(Item newItem)
    {
        item = newItem;
        print(newItem.name);
        Setup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MyPlayer")
        {
            Player = collision.gameObject.GetComponent<Mouvement>();
            Player.CanPickItem((Gun)item, this);
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
