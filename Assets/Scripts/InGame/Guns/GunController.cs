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
    [SerializeField] private Transform GunBarrel;

    private int currentGunAmmo = 0;
    private int currentGunBulletShot = 0;
    private bool isShooting = false, readyToShoot = true, isReloading = false;

    public void Awake()
    {
        
    }

    public void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (currentGunInfo == null) return;
        if (currentGunInfo.isAutomatic) isShooting = Input.GetButton("Fire1");
        else isShooting = Input.GetButtonDown("Fire1");

        if (Input.GetKeyDown("r") && currentGunAmmo < currentGunInfo.magasineSize) Reload();

        if (isShooting && readyToShoot && !isReloading && currentGunAmmo > 0)
        {
            currentGunBulletShot = currentGunInfo.bulletsShot;
            Shoot();
        }
        if (Input.GetKeyDown("p")) RunTest();
    }

    private void RunTest()
    {
        GunBarrel.position = GunBarrel.position + GunBarrel.position;
    }

    public void ChangeItem(Gun newGun)
    {
        if (newGun.itemInfo == null)
        {
            return;
        }
        gunEquiped = newGun;
        currentGunInfo = (GunInfo)gunEquiped.itemInfo;
        currentGunAmmo = currentGunInfo.magasineSize;
        currentGunBulletShot = currentGunInfo.bulletsShot;
        print(newGun.aimingPoint.position);
        GunBarrel.localPosition = newGun.aimingPoint.position;
        //GunBarrel.position -= newGun.transform.position;
    }

    private void Shoot()
    {
        readyToShoot = false;

        ShootBullet();
        currentGunAmmo--;
        currentGunBulletShot--;

        Invoke("ResetShot", currentGunInfo.timeBetweenShooting);

        if(currentGunBulletShot > 0 && currentGunAmmo > 0)
        {
            Invoke("Shoot", currentGunInfo.timeBetweenShots);
        }
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

        
        GameObject Bullet = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Balle"), TriggerPoint.localPosition + GunBarrel.position, Rotation);
        Bullet.GetComponent<BougerBalle>().InitialiseBullet(TriggerPoint.position + GunBarrel.right, currentGunInfo.BulletDamage, currentGunInfo.speed, spread);

        
    }

    private void Reload()
    {
        isReloading = true;
        Invoke("FinishedReloading", currentGunInfo.reloadTime);
    }

    private void FinishedReloading()
    {
        isReloading = false;
        currentGunAmmo = currentGunInfo.magasineSize;
    }


}
