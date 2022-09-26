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
        print(aimingPoint);
        aimingPoint.position = newGun.aimingPoint.position;
        print(aimingPoint);
        itemSprite.sprite = newGun.itemSprite.sprite;
    }

}
