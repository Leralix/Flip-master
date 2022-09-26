using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    
    public override void Use()
    {

    }

    public void ReceiveGunInfo(Gun newGun)
    {
        itemInfo = newGun.itemInfo;
        aimingPoint.position = newGun.aimingPoint.position;
        itemSprite.sprite = newGun.itemSprite.sprite;
    }

}
