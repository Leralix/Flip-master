using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public Sprite icon;
    public Transform aimingPoint;
    public SpriteRenderer itemSprite;

    public abstract void Use();

}
