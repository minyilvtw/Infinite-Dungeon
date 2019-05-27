using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour {

    public EnemyMovement eMovement;
    public Collider2D detector;
    public void EnableMovement()
    {
        eMovement.canMove = true;
    }

    public void DisableMovement()
    {
        eMovement.canMove = false;
    }

    public void Attack() {

        float damage = GetComponentInParent<EnemyStatus>().attackDamage;

        Collider2D[] cols = new Collider2D[150];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Active"));
        detector.OverlapCollider(contactFilter, cols);
        foreach (Collider2D x in cols)
        {
            if (x != null)
            {
                if (x.tag == "Player")
                {
                    x.GetComponent<PlayerStatus>().TakeDamage(damage);
                }
            }
        }
    }
}
