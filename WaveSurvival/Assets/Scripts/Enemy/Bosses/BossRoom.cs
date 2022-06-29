using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour
{
    public GameObject bossDoorClose;
    public BossSpawn spawner;
    public Slider bossHealth;
    public bool newBoss = false;

    /*
     * sets up the boss room for the boss fight
     */
    private void StartBossFight()
    {
        bossDoorClose.gameObject.SetActive(true);
        spawner.SpawnBoss();
        bossHealth.gameObject.SetActive(true);
        newBoss = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (newBoss)
                StartBossFight();
        }
    }

    /*
     * sets up script to summon a boss fight when the boss door is entered
     */
    public void NewBossSpawn()
    {
        newBoss = true;
    }
}
