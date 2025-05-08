using UnityEngine;

public partial class CatStateManager : StateManager
{
    public CatStateManager(ParamaterBase paramater)
    {
        par = paramater;
        stateIdling = new CatIdling();
        stateMoving = new CatMoving();
        stateJumping = new CatJumping();
        stateFalling = new CatFalling();
        stateAttacking = new CatAttacking();
        stateDamaged = new CatDamaged();
        stateDead = new CatDead();
    }
}
