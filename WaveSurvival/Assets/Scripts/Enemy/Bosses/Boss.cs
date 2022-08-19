using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    public BossState bossState;
    public int bossCoinValue = 15;
    // Start is called before the first frame update
    void Start()
    {
        _waveControl = GameObject.Find("WaveController").GetComponent<WaveController>();
        healthSlider = GameObject.Find("BossHealth").GetComponent<Slider>();
        //int extraHealth = (waveControl.GetWaveCount() / waveNumIncrease) * difficultyHealthIncrease;//extra health gained ever waveNumIncrease number of waves
        _currentHealth = startingHealth;// + extraHealth;

        healthSlider.maxValue = _currentHealth;
        healthSlider.value = _currentHealth;
    }

    /*
     * removes health based on damage
     */
    public override void DealDamage(int dmg)
    {
        animator.SetBool("EnemyHit", true);

        _currentHealth -= dmg;
        if (!bossState.phase2 && ((float)_currentHealth) /((float)startingHealth) < 0.51f)
            bossState.SetPhase2();

        if (_currentHealth <= 0)
        {
            Die();
        }

        UpdateUI();
    }

    protected override void Die()
    {
        //play animation

        PlayerGold pGold = GameObject.Find("Player").GetComponent<PlayerGold>();
        pGold.AddGold(bossCoinValue);
        EnemyDrop();

        ///// main menu gold
        PlayerGameData mGold = GameObject.Find("PlayerDataObject").GetComponent<PlayerGameData>();
        mGold.IncreaseMenuGold(bossCoinValue);
        ////

        _waveControl.BossDied();

        healthSlider.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public bool IsInPhase2() 
    {
        return startingHealth - _currentHealth < 0.51;
    }

}
