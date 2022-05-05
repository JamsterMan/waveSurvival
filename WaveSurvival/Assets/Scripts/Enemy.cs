using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 10;
    public int difficultyHealthIncrease = 5;
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

    /*
     * removes health based on damage
     */
    public void DealDamage(int dmg)
    {
        animator.SetBool("EnemyHit", true);

        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    /*
     * Tells wave controller that enemy died then destroys self
     */
    void Die()
    {
        //play animation

        waveControll.EnemyDied();
        Destroy(this.gameObject);
    }

}
