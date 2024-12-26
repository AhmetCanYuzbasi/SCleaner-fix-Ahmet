using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    List<IWeapon> weapons = new List<IWeapon>();

    // Start is called before the first frame update
    void Start()
    {
        //IWeapon weaponOne = gameObject.AddComponent<StarterPistolScript>();
        //IWeapon weaponTwo = gameObject.AddComponent<Shotgun>();
        //weapons.Add(weaponOne);
        //weapons.Add(weaponTwo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
