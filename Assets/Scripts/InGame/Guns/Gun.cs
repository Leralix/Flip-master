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
        print("aming point:" + aimingPoint.position);
        print("New gun:" + newGunInfo.barrelPosition);
        aimingPoint.position = newGunInfo.barrelPosition;
        itemSprite.sprite = newGunInfo.itemSprite;
}

}
