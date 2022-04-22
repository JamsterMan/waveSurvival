using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public readonly int maxHealth = 10;
    private int currentHealth;
    public Animator animator;
    private WaveController waveControll;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        waveControll = GameObject.Find("WaveController").GetComponent<WaveController>();
    }

    public void DealDamage(int dmg)
    {
        animator.SetBool("EnemyHit", true);

        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //play animation
        //Debug.Log("enemy Died");
        waveControll.EnemyDied();
        Destroy(this.gameObject);
    }

}
