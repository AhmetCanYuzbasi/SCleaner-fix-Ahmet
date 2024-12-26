using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerInventory : MonoBehaviour
{

    public static Action<GameObject, IWeapon, WeaponSO> OnInventoryReadyEvent;
    public static Action<GameObject, IWeapon> OnWeaponSwitchEvent;
    [SerializeField] List<GameObject> weaponPrefabs = new();
    [SerializeField] List<GameObject> weaponGOs = new();
    [SerializeField] List<IWeapon> weaponScripts = new();

    int weaponCount = 4;
    [SerializeField] Transform _weaponParent;
    [SerializeField] Transform _weaponPosition;
    GameObject currentWeapon;
    IWeapon currentWeaponScript;
    public int currentlyUsedSlot = 0;
    public int slotToSwitchTo = 0;

    void Awake(){
        GameObject instantiatedWeaponPrefab;
        for(int i = 0; i < weaponCount; ++i){
            if(weaponPrefabs.ElementAt(i)){
                instantiatedWeaponPrefab = Instantiate(weaponPrefabs[i]);
                weaponGOs.Add(instantiatedWeaponPrefab);
            }
        }
    }
    
    void Start()
    {
        IWeapon instantiatedWeaponScript;
        for(int i = 0; i < weaponCount; ++i){
            if(_weaponParent != null){

            instantiatedWeaponScript = weaponGOs.ElementAt(i).GetComponent<IWeapon>();
            weaponScripts.Add(instantiatedWeaponScript);
            weaponGOs[i].transform.parent = _weaponParent;
            print(_weaponPosition.position);
            weaponGOs[i].transform.position = _weaponPosition.position;

            weaponGOs[i].transform.localPosition += instantiatedWeaponScript.WeaponInfo.offset;
            weaponGOs[i].SetActive(false);
            }
            
        }
        print(weaponGOs.Count);
        print(weaponScripts.Count);
        currentWeapon = weaponGOs.ElementAt(1);
        currentWeaponScript = weaponScripts.ElementAt(1);
        OnInventoryReadyEvent?.Invoke(currentWeapon, currentWeaponScript, currentWeaponScript.WeaponInfo);
    }

    public void WeaponSwap(InputAction.CallbackContext ctx){
        
    if(ctx.performed){
        if (ctx.control is KeyControl keyControl)
        {
            Key pressedKey = keyControl.keyCode;
            switch(pressedKey){
                case Key.Digit1:
                    slotToSwitchTo = 0;
                    if(weaponGOs[slotToSwitchTo] != null && slotToSwitchTo != currentlyUsedSlot){
                        
                        OnWeaponSwitchEvent?.Invoke(weaponGOs[slotToSwitchTo], weaponScripts[slotToSwitchTo]);
                        currentlyUsedSlot = slotToSwitchTo;
                    }
                    
                    break;
                case Key.Digit2:
                    slotToSwitchTo = 1;
                    if(weaponGOs[slotToSwitchTo] != null && slotToSwitchTo != currentlyUsedSlot){
                        
                        OnWeaponSwitchEvent?.Invoke(weaponGOs[slotToSwitchTo], weaponScripts[slotToSwitchTo]);
                        currentlyUsedSlot = slotToSwitchTo;
                        print(weaponGOs[currentlyUsedSlot]);
                        print(weaponScripts[currentlyUsedSlot]);
                    }
                    
                    break;
                case Key.Digit3:
                        slotToSwitchTo = 2;
                    if(weaponGOs[slotToSwitchTo] != null && slotToSwitchTo != currentlyUsedSlot){
                        
                        OnWeaponSwitchEvent?.Invoke(weaponGOs[slotToSwitchTo], weaponScripts[slotToSwitchTo]);
                        currentlyUsedSlot = slotToSwitchTo;
                        print(weaponGOs[currentlyUsedSlot]);
                        print(weaponScripts[currentlyUsedSlot]);
                    }   
                    break;
                case Key.Digit4:
                        slotToSwitchTo = 3;
                    if(weaponGOs[slotToSwitchTo] != null && slotToSwitchTo != currentlyUsedSlot){

                        OnWeaponSwitchEvent?.Invoke(weaponGOs[slotToSwitchTo], weaponScripts[slotToSwitchTo]);
                        currentlyUsedSlot = slotToSwitchTo;
                        print(weaponGOs[currentlyUsedSlot]);
                        print(weaponScripts[currentlyUsedSlot]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
        
    }
}
