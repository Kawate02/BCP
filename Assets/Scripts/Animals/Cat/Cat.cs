using Unity.VisualScripting;
using UnityEngine;

public class Cat : MonoBehaviour, IDamageable, IAttackable
{
    public static ParamaterBase par = new CatParamater();
    public static StateManager cat = new CatStateManager(par);
    public static AttitudeController attitude = new AttitudeController();
    private AttackDitection attackDitection;
    private BoxCollider2D attackCollider;
    public void OnStart()
    {
        attackDitection = Instantiate(Resources.Load("AttackDitection").GetComponent<AttackDitection>(), transform);
        attackCollider = attackDitection.GetComponent<BoxCollider2D>();
        cat.rb = GetComponent<Rigidbody2D>();
        cat.ani = GetComponent<Animator>();
        cat.tf = GetComponent<Transform>();
        cat.OnStart();
    }

    public void Attack()
    {
        Vector2 offset = new Vector2((par.atkReach / 2) + 150f * par.facingDir, 0),
            size = new Vector2(par.atkReach, 190f);

        attackDitection.damage = par.atkDmg;
        attackCollider.offset = offset;
        attackCollider.size = size;
        attackDitection.isAttaking = true; 
    }

    public void Damaged(int damage)
    {
        cat.ChangeState(cat.stateDamaged);
        cat.par.hp -= damage;
        if (cat.par.hp >= 0) cat.ChangeState(cat.stateDead);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        cat.isFalling = false;
    }
}
