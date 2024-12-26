using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    void SetupEquipmentParameters(Quaternion rotation, Vector2 direction);
    void UseEquipment();
    void TriggerAbility();
    IEnumerator TriggerAbilityAfterTime();
}
