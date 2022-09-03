using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public GameObject ItemGameObject;

    public Transform aimingPoint;

    public abstract void Use();

}
