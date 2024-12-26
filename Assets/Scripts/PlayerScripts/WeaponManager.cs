using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

    //This class handles input related to the weapon functionality
    //such as shooting, reloading. It only takes the input and
    //relegates the actual functionality to the weapon itself

public class WeaponManager : MonoBehaviour
{
    
    InputAction _primaryAttackAction;
    InputAction _secondaryAttackAction;
    public Transform _weaponPosition;
    bool _isLMBHeld;
    bool _isRMBHeld;
    IWeapon _currentWeaponScript;
    [SerializeField] GameObject _currentWeaponGO;

    void Awake(){

    }

    void OnEnable(){
        PlayerInventory.OnInventoryReadyEvent += ReceiveWeapon;
        PlayerInventory.OnWeaponSwitchEvent += SwitchWeapon;
    }

    void OnDisable(){
        PlayerInventory.OnInventoryReadyEvent -= ReceiveWeapon;
        PlayerInventory.OnWeaponSwitchEvent -= SwitchWeapon;
    }

    void Start(){
        //_currentWeaponGO.transform.parent = transform;
       // _currentWeaponGO.transform.position = _weaponPosition.position;
        //print(_currentWeaponScript.WeaponInfo);
        //_currentWeaponGO.transform.localPosition += _currentWeaponScript.WeaponInfo.offset;

    } 


    public void Update(){

        if(_primaryAttackAction.IsPressed()){
            _isLMBHeld = true;
            _currentWeaponScript.HandlePrimaryAttackInput();
        } 
        else if (_isLMBHeld && !_primaryAttackAction.IsPressed()){
            _isLMBHeld = false;
            _currentWeaponScript.HandlePrimaryAttackInputCancel();
        }
        if(_secondaryAttackAction.IsPressed()){
            _isRMBHeld = true;
            _currentWeaponScript.HandleSecondaryAttackInput();
        } 
        else if (_isRMBHeld && !_primaryAttackAction.IsPressed()){
            _isRMBHeld = false;
            _currentWeaponScript.HandleSecondaryAttackInputCancel();
        }
        
   
    }

    
    public void HandleSecondaryAttack(){
        _currentWeaponScript.SecondaryAttack();
    }

    public void ReceiveWeapon(GameObject weapon, IWeapon weaponScript, WeaponSO info){
        print("Received : " + weapon + " and " + weaponScript);
        _currentWeaponGO = weapon;
        _currentWeaponScript = weaponScript;
        if(_currentWeaponScript.WeaponInfo == null)
            _currentWeaponScript.WeaponInfo = info;
        
        _currentWeaponGO.SetActive(true);
        print(info);
    }

    
    public void SwitchWeapon(GameObject weapon, IWeapon weaponScript){
        weaponScript.ResetWeaponState();
        _currentWeaponGO.SetActive(false);
        _currentWeaponGO = weapon;
        _currentWeaponGO.SetActive(true);
        _currentWeaponScript = weaponScript;
    }

    public void TriggerReload(InputAction.CallbackContext ctx){
        if (ctx.performed){
            _currentWeaponScript.HandleReloadStart();
        }
    }

    public void ReceivePrimaryAttackInputAction(InputAction action){
        _primaryAttackAction = action;
    }

    public void ReceiveSecondaryAttackInputAction(InputAction action){
        _secondaryAttackAction = action;
    }

    
}