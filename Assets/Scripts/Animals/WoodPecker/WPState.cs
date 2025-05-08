using UnityEngine;

public partial class WPStateManager
{
    public class WPIdling : IdlingBase
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
    public class WPMoving : MovingBase
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
    public class WPJumping : JumpingBase
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
    public class WPFalling : FallingBase
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
    public class WPAttacking : AttackingBase
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
    public class WPDamaged : DamagedBase
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
    public class WPDead : DeadBase
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
