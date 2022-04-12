using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public readonly int maxHealth = 10;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("enemy health: " + currentHealth);
    }

    public void DealDamage(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log("enemy health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //play animation
        Debug.Log("enemy Died");
        Destroy(this.gameObject);
    }
}
