using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    [SerializeField] private int currentHealth;
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
    }

    /*
     * removes health based on damage
     * updates UI
     */
    public void DealDamage(int dmg)
    {
        if (Time.time > nextDamageTime)
        {
            animator.SetBool("PlayerHit", true);

            currentHealth -= dmg;
            UpdateUIHealth();
            nextDamageTime = Time.time + IframeRate;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void HealHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UpdateUIHealth();
    }

    public bool IsMaxHealth()
    {
        return currentHealth == maxHealth;
    }

    void Die()
    {
        //play animation
        Debug.Log("Game Over");
        Time.timeScale = 0;//temp player death
    }

    //iframes
    public bool IsDamageable()
    {
        return Time.time > nextDamageTime;
    }

    private void UpdateUIHealth()
    {
        healthBar.value = currentHealth;
    }

    public void ChangePlayerMaxHealth(int amount)
    {
        if (maxHealth + amount > 0 && maxHealth + amount < 40)
        {//needs a max an min values
            maxHealth += amount;
            currentHealth += amount; //adds to current health too
        }
    }
}
