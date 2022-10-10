using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    
    public override void Use()
    {

    }

    public void ReceiveGunInfo(GunInfo newGunInfo)
    {
        itemInfo = newGunInfo;
        aimingPoint.position = newGunInfo.barrelPosition;
        itemSprite.sprite = newGunInfo.itemSprite;
}

}
