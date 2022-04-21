using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public readonly int maxHealth = 20;
    private int currentHealth;
    public Animator animator;

    public float IframeRate = 2f;
    float nextDamageTime = 0f;

    private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        UpdateUIHealth();
        Debug.Log("player health: " + currentHealth);
    }

    public void DealDamage(int dmg)
    {
        if (Time.time > nextDamageTime)
        {
            animator.SetBool("PlayerHit", true);

            currentHealth -= dmg;
            UpdateUIHealth();
            Debug.Log("player health: " + currentHealth);
            nextDamageTime = Time.time + IframeRate;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        //play animation
        Debug.Log("Game Over");
        Time.timeScale = 0;//temp player death
    }

    public bool isDamageable()
    {
        return Time.time > nextDamageTime;
    }

    private void UpdateUIHealth()
    {
        healthBar.value = currentHealth;
    }
}
