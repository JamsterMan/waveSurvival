using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private readonly int maxHearts = 10;
    public int health = 8;
    [SerializeField] private int currentHealth;

    public Animator animator;

    public HeartsAreaUI heartsUI;
    private readonly int healthPerHeart = 2;

    public float IframeRate = 2f;
    float nextDamageTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;

        heartsUI.SetUpHeartsUI(health/healthPerHeart);

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
        Debug.Log("player healed: "+amount);
        currentHealth += amount;
        if (currentHealth > health)
            currentHealth = health;
        UpdateUIHealth();
    }

    public bool IsMaxHealth()
    {
        return currentHealth == health;
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
        heartsUI.UpdateHearts(currentHealth, health, healthPerHeart);
    }

    //amount -> number of hearts to add
    public void ChangePlayerMaxHealth(int amount)
    {
        if(amount > 0)//add hearts
        {
            for (int i = 0; i < amount; i++)
            {
                if(health <= maxHearts*healthPerHeart)
                {
                    heartsUI.AddHeart();
                    health += healthPerHeart;
                    currentHealth += healthPerHeart;

                }
            }
        }
        else if(amount < 0)//remove hearts
        {
            for (int i = 0; i < -amount; i++)
            {
                if (health > 0)
                {
                    heartsUI.RemoveHeart();
                    health -= healthPerHeart;
                    if (health <= 0)
                        Die();

                    if (currentHealth > health)
                        currentHealth = health;
                }
                else
                {
                    Die();
                }
            }
        }
    }
}
