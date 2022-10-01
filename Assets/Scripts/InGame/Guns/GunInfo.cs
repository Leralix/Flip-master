using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
    public int BulletDamage, magasineSize, bulletPerTap, speed, bulletsShot;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public bool isAutomatic;
    public Vector3 barrelPosition;
}
