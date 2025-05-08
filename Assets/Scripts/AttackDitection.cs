using UnityEngine;

public class AttackDitection : MonoBehaviour
{
    public bool isAttaking = false;
    public int damage = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttaking)
        {
            isAttaking = false;
            gameObject.GetComponent<BoxCollider2D>().size = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().offset = Vector2.zero;
            IDamageable damaged = collision.GetComponent<IDamageable>();
            if (damaged != null)
            {
                damaged.Damaged(damage);
                Debug.Log(collision.name);
            }
        }
    }
}
