using UnityEngine;

[CreateAssetMenu(fileName = "Global Variables", menuName = "ScriptableObjects/Global Variables")]
public class GlobalSO : ScriptableObject
{
    public bool playerIsAlive = true;

    void Init(){
        playerIsAlive = true;
    }
}
