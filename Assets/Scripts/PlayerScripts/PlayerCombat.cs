using System.Security.Cryptography;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;

    public LayerMask enemyLayer;
    
    
    public Animator anim;

    public float cooldown;
    private float timer;

    // Attack cooldown.
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }    
    }

    // Triggers attack animation.
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttackSide", true);

            timer = cooldown;
        }
    }

    // Detects enemy and deals damage.
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
                    attackPoint.position,
                    StatsManager.Instance.weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-StatsManager.Instance.damage);
            enemies[0].GetComponent<EnemyKnockback>()
                    .Knockback(transform, StatsManager.Instance.knockbackForce,
                    StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }

    // Stops attack animation.
    public void FinishAttacking()
    {
        anim.SetBool("isAttackSide", false);
    }

    // Shows boundary of attack range.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);    
    }

}
