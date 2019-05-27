using UnityEngine;

public class SlimeAnimationEvents : MonoBehaviour {

    public GameObject bulletPrefab;
    public EnemyMovement eMovement;

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
        if (GetComponentInParent<EnemyMovement>().sprRenderer.flipX)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, null);
            bullet.GetComponent<projectile>().moveDirectionLeft = true;
            bullet.GetComponent<projectile>().damage = GetComponentInParent<EnemyStatus>().attackDamage;
        }
        else
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, null);
            bullet.GetComponent<projectile>().damage = GetComponentInParent<EnemyStatus>().attackDamage;
        }
    }
}
