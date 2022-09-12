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
        print(newGun.itemInfo);
        itemInfo = newGun.itemInfo;
        aimingPoint = newGun.aimingPoint;
        itemSprite.sprite = newGun.itemSprite.sprite;
    }

}
