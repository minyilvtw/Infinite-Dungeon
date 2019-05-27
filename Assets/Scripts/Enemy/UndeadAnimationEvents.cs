using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadAnimationEvents : MonoBehaviour {

    public EnemyMovement eMovement;
    public Collider2D detector;
    public GameObject bulletPrefab;
    public Transform feetPos;


    public void EnableMovement()
    {
        eMovement.canMove = true;
    }

    public void DisableMovement()
    {
        eMovement.canMove = false;
    }

    public void Attack()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().ShakeCamera();

        float damage = GetComponentInParent<EnemyStatus>().attackDamage;

        Collider2D[] cols = new Collider2D[200];
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

        for (int i = 0; i < 5; i++) {
            if (GetComponentInParent<EnemyMovement>().sprRenderer.flipX)
            {
                GameObject bullet = Instantiate(bulletPrefab, feetPos.position + new Vector3(-1f *i, 0, 0), Quaternion.identity, null);
                bullet.GetComponent<projectile>().moveDirectionLeft = true;
                bullet.GetComponent<projectile>().damage = (int)(GetComponentInParent<EnemyStatus>().attackDamage/5f);
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, feetPos.position + new Vector3(1f * i, 0, 0), Quaternion.identity, null);
                bullet.GetComponent<projectile>().damage = (int)(GetComponentInParent<EnemyStatus>().attackDamage /5f);
            }
        }
    }

}
