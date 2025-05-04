using System.Security.Cryptography;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
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

    // Stops attack animation.
    public void FinishAttacking()
    {
        anim.SetBool("isAttackSide", false);
    }
}
