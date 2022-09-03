using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class GunController : MonoBehaviour
{
    public Transform TriggerPoint;

    private Gun gunEquiped;
    private GunInfo currentGunInfo;
    private Transform GunBarrel;

    private int currentGunAmmo = 0;
    private bool isShooting = false, readyToShoot = true, isReloading = false;

    public void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (currentGunInfo.isAutomatic) isShooting = Input.GetButton("Fire1");
        else isShooting = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown("r") && currentGunAmmo < currentGunInfo.magasineSize) Reload();

        if (isShooting && readyToShoot && !isReloading && currentGunAmmo > 0) Shoot();
    }

    public void ChangeItem(Gun newGun)
    {
        gunEquiped = newGun;
        currentGunInfo = (GunInfo)gunEquiped.itemInfo;
        currentGunAmmo = currentGunInfo.magasineSize;
        GunBarrel = newGun.aimingPoint;
    }

    private void Shoot()
    {
        readyToShoot = false;
        currentGunAmmo--;
        ShootBullet();

        Invoke("ResetShot", currentGunInfo.timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ShootBullet()
    {
        //Spread
        float spread = Random.Range(-currentGunInfo.spread, currentGunInfo.spread) * 10;
        Quaternion Rotation = Quaternion.Euler(TriggerPoint.rotation.eulerAngles.x, TriggerPoint.rotation.eulerAngles.y, TriggerPoint.rotation.eulerAngles.z + spread) ;

        GameObject Bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Balle"), GunBarrel.position, Rotation);//Quaternion.Euler(GunBarrel.rotation.x, GunBarrel.rotation.y, GunBarrel.rotation.z - 90));
        Bullet.GetComponent<BougerBalle>().InitialiseBullet(GunBarrel.right, currentGunInfo.BulletDamage, -1,spread);
    }

    private void Reload()
    {
        currentGunAmmo = currentGunInfo.magasineSize;
    }


}
