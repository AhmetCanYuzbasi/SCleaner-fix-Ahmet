using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour, IWeapon
{
    public WeaponSO WeaponInfo { get; set;}
    [SerializeField] BulletSO bulletInfo;
    [SerializeField] Transform firingPoint;
    [SerializeField] Animator _animator;
    [SerializeField] RuntimeAnimatorController _runtimeAnimatorController;

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

        if (_animator.runtimeAnimatorController){
            print("Runtime animator controller is null, fetching.");
            _runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimatorControllers/SMG_AC");
            _animator.runtimeAnimatorController = _runtimeAnimatorController;
        }
        WeaponInfo.Init();
    }

    void Start(){
        if (!_animator){
            _animator = GetComponent<Animator>();
        }
        //PlayWeaponSounds.ReceiveAudioSource(GetComponent<AudioSource>());
    }

    void Update(){
        if (!HasAmmo() && WeaponInfo.currentReserveAmmo != 0 && !WeaponInfo.isReloading){
            HandlePrimaryAttackInputCancel();
            HandleReloadStart();
        }
    }

    public void ReceiveFiringPoint(Transform firingPoint){
        this.firingPoint = firingPoint;
    }

    public void PrimaryAttack(){
        print("called");
        if (!HasAmmo() || WeaponInfo.isReloading) return;

        Bullet instantiatedBullet = Instantiate(WeaponInfo.bulletPrefab, firingPoint.transform.position, transform.rotation).GetComponent<Bullet>();
        instantiatedBullet.SetupBulletParameters(bulletInfo.projectileSpeed, bulletInfo.size, WeaponInfo.damage, bulletInfo.lifeTime);
        --WeaponInfo.currentAmmo;
    }

    public void SecondaryAttack(){
        //no op
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

        if( WeaponInfo.roundCapacity - ammoBeforeReload <= WeaponInfo.currentReserveAmmo){
            WeaponInfo.currentReserveAmmo -= WeaponInfo.roundCapacity - ammoBeforeReload;
            WeaponInfo.currentAmmo = WeaponInfo.roundCapacity;
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

        if(WeaponInfo.currentReserveAmmo == 0 || WeaponInfo.currentAmmo == WeaponInfo.roundCapacity || WeaponInfo.isReloading) 
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
        //no op
    }

    public void HandleSecondaryAttackInput()
    {
        throw new System.NotImplementedException();
    }

    public void HandleSecondaryAttackInputCancel()
    {
        throw new System.NotImplementedException();
    }
    public void ResetWeaponState(){
        WeaponInfo.isFiring = false;
        WeaponInfo.isReloading = false;
        _animator.SetBool("isFiring", false);
        _animator.ResetTrigger("ReloadTrigger");
        //_animator.Play("Idle");
        _animator.SetTrigger("WeaponSwitchTrigger");
        GetComponent<SpriteRenderer>().sprite = WeaponInfo.sprite;
    }
}
