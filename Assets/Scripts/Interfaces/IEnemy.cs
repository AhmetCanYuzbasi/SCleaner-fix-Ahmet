using System.Collections;

public interface IEnemy
{

    void SpecialAbility(PlayerMovement playerMovement);
    void SetProvoked();
    void Attack();
}
