using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfoSO", menuName = "ScriptableObjects/Weapon Info")]
public class WeaponSO : ScriptableObject
{
    public Sprite weaponSprite;
    public AudioClip gunSound;
    public AnimationClip shootAnimation;
    public AnimationClip reloadAnimation;
    public GameObject bulletPrefab;
    public WeaponType Type;
    public BulletSO bulletInfo;
    public Vector3 offset = Vector3.zero;
    public string weaponName;
    public int damage;
    public int currentAmmo;
    public int ammoInClip;
    public int maxReserveAmmo;
    public int currentReserveAmmo;
    public int pelletCount;
    public float lastFireTime;
    public float fireRate;
    public bool isReloading;
    public bool isFiring;



    public enum WeaponType{
    MELEE,
    RANGED
}

}


