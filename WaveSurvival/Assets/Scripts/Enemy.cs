using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public readonly int startingHealth = 10;
    private readonly int difficultyHealthIncrease = 5;
    private int waveNumIncrease = 5;
    private int currentHealth;
    public Animator animator;
    private WaveController waveControll;

    // Start is called before the first frame update
    void Start()
    {
        waveControll = GameObject.Find("WaveController").GetComponent<WaveController>();
        int extraHealth = (waveControll.GetWaveCount() / waveNumIncrease) * difficultyHealthIncrease;//extra health gained ever waveNumIncrease number of waves
        currentHealth = startingHealth + extraHealth;
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
