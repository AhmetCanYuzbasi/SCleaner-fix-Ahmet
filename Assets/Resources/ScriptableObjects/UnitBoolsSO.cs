using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Bools", menuName = "ScriptableObjects/Unit Bools")]
public class UnitBoolsSO : ScriptableObject
{
    public bool isAlive = true;
    public bool hasBeenAttacked = false;
    public bool playerDetected = false;

    public void Init(){
        isAlive = true;
        hasBeenAttacked = false;
        playerDetected = false;
    }
}
