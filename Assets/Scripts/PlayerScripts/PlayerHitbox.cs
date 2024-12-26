using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public delegate void OnPlayerHit(IEnemy attacker);
    public static OnPlayerHit onPlayerHit;

    void OnTriggerEnter2D(Collider2D other)
    {
        IEnemy attacker = other.GetComponent<IEnemy>();
        if (attacker != null){
            onPlayerHit?.Invoke(attacker);
        }
    }
}
