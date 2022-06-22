using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        waveControl = GameObject.Find("WaveController").GetComponent<WaveController>();
        healthSlider = GameObject.Find("BossHealth").GetComponent<Slider>();
        //int extraHealth = (waveControl.GetWaveCount() / waveNumIncrease) * difficultyHealthIncrease;//extra health gained ever waveNumIncrease number of waves
        currentHealth = startingHealth;// + extraHealth;

        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    /*
     * removes health based on damage
     */
    public override void DealDamage(int dmg)
    {
        //animator.SetBool("EnemyHit", true);

        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateUI();
    }

    protected override void Die()
    {
        //play animation

        EnemyDrop();

        waveControl.BossDied();

        healthSlider.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}
