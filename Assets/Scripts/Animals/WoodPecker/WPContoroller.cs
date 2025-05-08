using UnityEngine;

public class WPContoroller : MonoBehaviour, IDamageable
{
    private WPStateManager wp = new WPStateManager();
    private AttitudeController attitude = new AttitudeController();
    private Paint pause = new Paint();
    private MaskGenerator maskGenerator;

    void Start()
    {
        maskGenerator = FindObjectOfType<MaskGenerator>();
        wp.rb = GetComponent<Rigidbody2D>();
        wp.tf = GetComponent<Transform>();
        wp.ani = GetComponent<Animator>();
        wp.OnStart();
    }

    void FixedUpdate()
    {
        wp.OnUpdate();
    }

    public void Damaged(int damage)
    {
        wp.ChangeState(wp.stateDamaged);
        wp.par.hp -= damage;
        if (wp.par.hp >= 0) wp.ChangeState(wp.stateDead);
        Debug.Log(wp.par.hp);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        wp.isFalling = false;
    }
}
