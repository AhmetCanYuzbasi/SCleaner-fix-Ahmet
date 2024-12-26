using System.Collections;

public interface IEnemy
{
    void SetProvoked();
    void Attack();
    void PlayerIsDetected();
    void PlayerIsOutOfChaseRange();
}
