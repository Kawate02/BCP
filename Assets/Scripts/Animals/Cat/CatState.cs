using UnityEngine;

public partial class CatStateManager
{
    public class CatIdling : IdlingBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
            if (owner.A || owner.D)
            {
                owner.ChangeState(owner.stateMoving);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                owner.ChangeState(owner.stateJumping);
            }
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatMoving : MovingBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
            owner.rb.velocity = Move(owner);
            if (!owner.A && !owner.D)
            {
                owner.ChangeState(owner.stateIdling);
            }
            if (owner.Space)
            {
                owner.ChangeState(owner.stateJumping);
            }
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatJumping : JumpingBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
            if (owner.A || owner.D)
            {
                owner.rb.velocity = new Vector2(owner.par.moveSpeed * owner.par.facingDir * 0.8f, owner.rb.velocity.y);
            }
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatFalling : FallingBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
            if (owner.A || owner.D)
            {
                owner.rb.velocity = new Vector2(owner.par.moveSpeed * owner.par.facingDir * 0.8f, owner.rb.velocity.y);
            }
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatAttacking : AttackingBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
            owner.ChangeState(preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatDamaged : DamagedBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
    public class CatDead : DeadBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            base.OnEnter(owner, preState);
        }
        public override void OnUpdate(StateManager owner)
        {
            base.OnUpdate(owner);
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            base.OnExit(owner, nextState);
        }
    }
}
