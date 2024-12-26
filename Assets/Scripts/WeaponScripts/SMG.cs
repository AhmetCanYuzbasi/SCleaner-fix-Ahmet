using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour, IWeapon
{
    public WeaponSO WeaponInfo { get; set;}
    [SerializeField] BulletSO bulletInfo;
    [SerializeField] Transform firingPoint;
    [SerializeField] Animator _animator;

    void Awake(){
        if (WeaponInfo == null){
            Debug.Log("SMG weapon info missing. Loading resource.");
            WeaponInfo = Resources.Load<WeaponSO>("ScriptableObjects/SMGSO");
        }
        print(WeaponInfo);

        if(WeaponInfo.bulletInfo == null){
            Debug.Log("SMG bullet info (part of SMG weapon info) missing. Loading pistol bullet info resource.");
            bulletInfo = Resources.Load<BulletSO>("ScriptableObjects/SMGBulletSO");
            //WeaponInfo.bulletInfo = bulletInfo;
        }
        else {
            Debug.Log("SMG bullet info (part of SMG bullet info) already exists.");
            bulletInfo = WeaponInfo.bulletInfo;
        }

        if (firingPoint == null){
            Debug.Log("FiringPoint not assigned. Finding...");
            print(firingPoint.transform.position.x + "    " + firingPoint.transform.position.y);
        }
    }

    void Start(){
        //GetComponent<SpriteRenderer>().sprite = WeaponInfo.weaponSprite;
        if (!_animator){
            _animator = GetComponent<Animator>();
        }
        WeaponInfo.lastFireTime = WeaponInfo.fireRate;
        //PlayWeaponSounds.ReceiveAudioSource(GetComponent<AudioSource>());
    }

    void Update(){
        if (!HasAmmo() && WeaponInfo.currentReserveAmmo != 0 && !WeaponInfo.isReloading){
            HandlePrimaryAttackInputCancel();
            HandleReloadStart();
        }
    }

    public void PrimaryAttack(){
        print("called");
        if (!HasAmmo() || WeaponInfo.isReloading || !CanFire()) return;

        Bullet instantiatedBullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, transform.rotation).GetComponent<Bullet>();
        instantiatedBullet.SetupBulletParameters(bulletInfo.projectileSpeed, bulletInfo.size, WeaponInfo.damage, bulletInfo.lifeTime);
        //WeaponInfo.lastFireTime = Time.time;
        --WeaponInfo.currentAmmo;
    }

    public void SecondaryAttack(){
        //no op
    }

    public bool CanFire(){
        return WeaponInfo.fireRate <= Time.time - WeaponInfo.lastFireTime;
    }

    public bool HasAmmo(){
        if (WeaponInfo.currentAmmo > 0){
            return true;
        }
        return false;
    }

    public void Reload()
    {
        int ammoBeforeReload = WeaponInfo.currentAmmo;
        WeaponInfo.currentAmmo = 0;

        if( WeaponInfo.ammoInClip - ammoBeforeReload <= WeaponInfo.currentReserveAmmo){
            WeaponInfo.currentReserveAmmo -= WeaponInfo.ammoInClip - ammoBeforeReload;
            WeaponInfo.currentAmmo = WeaponInfo.ammoInClip;
        }
        else {
            WeaponInfo.currentAmmo += WeaponInfo.currentReserveAmmo;
            WeaponInfo.currentReserveAmmo = 0;
        }
        WeaponInfo.isReloading = false;

    }


    public void HandlePrimaryAttackInput()
    {
        if(!HasAmmo() || WeaponInfo.isReloading || WeaponInfo.isFiring)
        return;
        
        WeaponInfo.isFiring = true;
        _animator.SetBool("isFiring", true);
    }

    public void HandlePrimaryAttackInputCancel(){
        WeaponInfo.isFiring = false;
        _animator.SetBool("isFiring", false);
    }

    public void HandleReloadStart(){

        if(WeaponInfo.currentReserveAmmo == 0 || WeaponInfo.currentAmmo == WeaponInfo.ammoInClip || WeaponInfo.isReloading) 
        return;

        WeaponInfo.isFiring = false;        
        _animator.SetBool("isFiring", WeaponInfo.isFiring);
        WeaponInfo.isReloading = true;
        _animator.SetTrigger("ReloadTrigger");
    }

    public void HandleReloadEnd(){
        WeaponInfo.isReloading = false;
    }

    public void HandleFiringAnimationEnd(){
        //
    }
}
