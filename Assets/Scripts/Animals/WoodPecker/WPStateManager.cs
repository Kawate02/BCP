using UnityEngine;

public partial class WPStateManager : StateManager
{
    public WPStateManager()
    {
        par = new WPParamater();
        stateIdling = new WPIdling();
        stateMoving = new WPMoving();
        stateJumping = new WPJumping();
        stateFalling = new WPFalling();
        stateAttacking = new WPAttacking();
        stateDamaged = new WPDamaged();
        stateDead = new WPDead();
    }
}
