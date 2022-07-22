using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 8;
    public Animator animator;
    public HeartsAreaUI heartsUI;
    public float IframeRate = 2f;

    private readonly int _maxHearts = 10;
    [SerializeField] private int _currentHealth;
    private readonly int _healthPerHeart = 2;
    private float _nextDamageTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = health;

        heartsUI.SetUpHeartsUI(health/ _healthPerHeart);

        UpdateUIHealth();
    }

    /*
     * removes health based on damage
     * updates UI
     */
    public void DealDamage(int dmg)
    {
        if (Time.time > _nextDamageTime)
        {
            animator.SetBool("PlayerHit", true);

            _currentHealth -= dmg;
            UpdateUIHealth();
            _nextDamageTime = Time.time + IframeRate;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void HealHealth(int amount)
    {
        //Debug.Log("player healed: "+amount);
        _currentHealth += amount;
        if (_currentHealth > health)
            _currentHealth = health;
        UpdateUIHealth();
    }

    public bool IsMaxHealth()
    {
        return _currentHealth == health;
    }

    void Die()
    {
        //play animation
        Debug.Log("Game Over");
        SceneManager.LoadScene(0);//temp player death
    }

    //iframes
    public bool IsDamageable()
    {
        return Time.time > _nextDamageTime;
    }

    private void UpdateUIHealth()
    {
        heartsUI.UpdateHearts(_currentHealth, health, _healthPerHeart);
    }

    //amount -> number of hearts to add
    public void ChangePlayerMaxHealth(int amount)
    {
        if(amount > 0)//add hearts
        {
            for (int i = 0; i < amount; i++)
            {
                if(health <= _maxHearts * _healthPerHeart)
                {
                    heartsUI.AddHeart();
                    health += _healthPerHeart;
                    _currentHealth += _healthPerHeart;

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
                    health -= _healthPerHeart;
                    if (health <= 0)
                        Die();

                    if (_currentHealth > health)
                        _currentHealth = health;
                }
                else
                {
                    Die();
                }
            }
        }
        UpdateUIHealth();
    }

    public void DodgeIframes(float time)
    {
        //base off of dodge duration

        _nextDamageTime = time;
    }
}
