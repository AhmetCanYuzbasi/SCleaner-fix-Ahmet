using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

    //This class handles input related to the weapon functionality
    //such as shooting, reloading. It only takes the input and
    //relegates the actual functionality to the weapon itself

public class WeaponManager : MonoBehaviour
{
    public Sprite playerAttackSprite;
    public Sprite defaultSprite;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _currentWeaponGO;
    public Transform _weaponPosition;
    public AudioClip weaponSwitchAudio;

    public IWeapon currentWeaponScript;
    InputAction _primaryAttackAction;
    
    bool _isLMBHeld;

    void OnEnable(){
        PlayerInventory.OnInventoryReadyEvent += ReceiveWeapon;
        PlayerInventory.OnWeaponSwitchEvent += SwitchWeapon;
    }

    void OnDisable(){
        PlayerInventory.OnInventoryReadyEvent -= ReceiveWeapon;
        PlayerInventory.OnWeaponSwitchEvent -= SwitchWeapon;
    }

    public void Update(){
        if(_primaryAttackAction.IsPressed()){
            _isLMBHeld = true;
            currentWeaponScript.HandlePrimaryAttackInput();
            _spriteRenderer.sprite = playerAttackSprite;
        } 
        else if (_isLMBHeld && !_primaryAttackAction.IsPressed()){
            _spriteRenderer.sprite = defaultSprite;
            _isLMBHeld = false;
            currentWeaponScript.HandlePrimaryAttackInputCancel();
        }
        
    }

    public void ReceiveWeapon(GameObject weapon, IWeapon weaponScript, WeaponSO info){
        _currentWeaponGO = weapon;
        currentWeaponScript = weaponScript;
        if(currentWeaponScript.WeaponInfo == null)
            currentWeaponScript.WeaponInfo = info;
        
        _currentWeaponGO.SetActive(true);
    }

    
    public void SwitchWeapon(GameObject weapon, IWeapon weaponScript){
        if(currentWeaponScript.WeaponInfo.isFiring) return;
        PlayPlayerSounds.PlayAudio(weaponSwitchAudio);
        weaponScript.ResetWeaponState();
        _currentWeaponGO.SetActive(false);
        _currentWeaponGO = weapon;
        _currentWeaponGO.SetActive(true);
        currentWeaponScript = weaponScript;
    }

    public void TriggerReload(InputAction.CallbackContext ctx){
        if (ctx.performed){
            currentWeaponScript.HandleReloadStart();
        }
    }

    public void ReceivePrimaryAttackInputAction(InputAction action){
        _primaryAttackAction = action;
    }

    
}