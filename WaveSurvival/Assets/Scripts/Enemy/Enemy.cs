using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 10;
    public int difficultyHealthIncrease = 5;
    protected int _currentHealth;
    public Animator animator;
    public PickUps enemyDrop;
    public Slider healthSlider;
    protected WaveController _waveControl;

    // Start is called before the first frame update
    void Start()
    {
        _waveControl = GameObject.Find("WaveController").GetComponent<WaveController>();
        int extraHealth = (_waveControl.GetBossCount() +1) * difficultyHealthIncrease;//extra health gained after every boss
        _currentHealth = startingHealth + extraHealth;

        healthSlider.maxValue = _currentHealth;
        healthSlider.value = _currentHealth;
    }

    /*
     * removes health based on damage
     */
    public virtual void DealDamage(int dmg)
    {
        animator.SetBool("EnemyHit", true);

        _currentHealth -= dmg;
        if(_currentHealth <= 0)
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

        _waveControl.EnemyDied();
        Destroy(this.gameObject);
    }

    //Decides if Enemy drops a consumable
    protected void EnemyDrop()
    {
        if(Random.Range(0f, 1f) <= _waveControl.GetEnemyDropRate())
        {
            Instantiate(enemyDrop, transform.position, Quaternion.identity);
        }
    }

    protected void UpdateUI()
    {
        healthSlider.value = _currentHealth;
    }

}
