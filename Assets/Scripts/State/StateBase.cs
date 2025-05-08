using Unity.VisualScripting;
using UnityEngine;


public class ParamaterBase
{
    public int hp;
    public float moveSpeed;
    public float jumpHight;
    public float jumpTime;
    public float fallSpeed;
    public float atkReach;
    public int atkDmg;
    public int facingDir;
}

public abstract class StateBase
{
    public virtual void OnEnter(StateManager owner, StateBase preState) { }

    public virtual void OnUpdate(StateManager owner) { }

    public virtual void OnExit(StateManager owner, StateBase nextState) { }
}

public interface IAttackable
{
    void Attack();
}
public interface IDamageable
{
    void Damaged(int damage);
}

public abstract partial class StateManager
{
    public ParamaterBase par;

    public IdlingBase stateIdling = new();
    public MovingBase stateMoving = new();
    public JumpingBase stateJumping = new();
    public FallingBase stateFalling = new();
    public AttackingBase stateAttacking = new();
    public DamagedBase stateDamaged = new();
    public DeadBase stateDead = new();

    public float jumpStartedTime;

    public bool isFalling = false;
    public bool hasDamaged = false;

    protected StateBase currentState;

    public Rigidbody2D rb;
    public Transform tf;
    public Animator ani;

    public bool A = false, D = false, Space = false;

    public void OnStart()
    {
        currentState = stateIdling;
        currentState.OnEnter(this, null);
    }

    public void OnUpdate()
    {
        currentState.OnUpdate(this);
    }

    public void ChangeState(StateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }
}

public abstract partial class StateManager
{
    public class IdlingBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            if (preState == owner.stateFalling) owner.ani.SetBool("Landing", true);
            owner.ani.SetBool("Idling", true);
        }
        public override void OnUpdate(StateManager owner)
        {
            
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            owner.ani.SetBool("Landing", false);
            owner.ani.SetBool("Idling", false);
        }
    }
    public class MovingBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            if (preState == owner.stateFalling) owner.ani.SetBool("Landing", true);
            owner.ani.SetBool("Moving", true);
        }
        public override void OnUpdate(StateManager owner)
        {
            
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            if (nextState == owner.stateIdling) owner.rb.velocity = new Vector2(0, 0);
            if (nextState == owner.stateJumping) owner.jumpStartedTime = Time.time + owner.par.jumpTime;
            if (nextState == owner.stateFalling) owner.rb.velocity = new Vector2(owner.rb.velocity.x, 0);
            owner.ani.SetBool("Landing", false);
            owner.ani.SetBool("Moving", false);
        }
        public Vector2 Move(StateManager owner)
        {
            float moveVec = owner.par.moveSpeed * owner.par.facingDir;
            return new Vector2(moveVec, owner.rb.velocity.y);
        }
    }
    public class JumpingBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            owner.ani.SetBool("Jumping", true);
            owner.jumpStartedTime = Time.time + owner.par.jumpTime;
            Vector2 jumpPow = new Vector2(0, owner.par.jumpHight);
            owner.rb.velocity = jumpPow;

            Vector3 rotation = owner.tf.eulerAngles;
            rotation.z = 0;
            owner.tf.rotation = Quaternion.Euler(rotation);
        }
        public override void OnUpdate(StateManager owner)
        {
            float jumpNowTime = owner.jumpStartedTime - Time.time;
            if (jumpNowTime > 0 && jumpNowTime <= 0.2)
            {
                owner.rb.velocity = new Vector2(0, owner.par.jumpHight / 10);
            }
            if (jumpNowTime <= 0)
            {
                owner.ChangeState(owner.stateFalling);
            }
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            owner.ani.SetBool("Jumping", false);
        }
    }
    public class FallingBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {
            owner.ani.SetBool("Falling", true);
            owner.isFalling = true;
            owner.rb.velocity = new Vector2(0, -owner.par.fallSpeed);
        }
        public override void OnUpdate(StateManager owner)
        {
            if (owner.isFalling == false)
            {
                owner.ChangeState(owner.stateIdling);
            }

        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {
            owner.ani.SetBool("Falling", false);
        }
    }
    public class AttackingBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {

        }
        public override void OnUpdate(StateManager owner)
        {
            
        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {

        }
    }
    public class DamagedBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {

        }
        public override void OnUpdate(StateManager owner)
        {

        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {

        }
    }
    public class DeadBase : StateBase
    {
        public override void OnEnter(StateManager owner, StateBase preState)
        {

        }
        public override void OnUpdate(StateManager owner)
        {

        }
        public override void OnExit(StateManager owner, StateBase nextState)
        {

        }
    }
}
