using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 10;
    public int difficultyHealthIncrease = 5;
    protected int currentHealth;
    public Animator animator;
    public PickUps enemyDrop;
    public Slider healthSlider;
    protected WaveController waveControl;

    // Start is called before the first frame update
    void Start()
    {
        waveControl = GameObject.Find("WaveController").GetComponent<WaveController>();
        int extraHealth = (waveControl.GetBossCount() +1) * difficultyHealthIncrease;//extra health gained after every boss
        currentHealth = startingHealth + extraHealth;

        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    /*
     * removes health based on damage
     */
    public virtual void DealDamage(int dmg)
    {
        animator.SetBool("EnemyHit", true);

        currentHealth -= dmg;
        if(currentHealth <= 0)
        {
            Die();
        }
        UpdateUI();
    }

    /*
     * Tells wave controller that enemy died then destroys self
     */
    protected virtual void Die()
    {
        //play animation

        EnemyDrop();

        waveControl.EnemyDied();
        Destroy(this.gameObject);
    }

    //Decides if Enemy drops a consumable
    protected void EnemyDrop()
    {
        if(Random.Range(0f, 1f) <= waveControl.GetEnemyDropRate())
        {
            Instantiate(enemyDrop, transform.position, Quaternion.identity);
        }
    }

    protected void UpdateUI()
    {
        healthSlider.value = currentHealth;
    }

}
