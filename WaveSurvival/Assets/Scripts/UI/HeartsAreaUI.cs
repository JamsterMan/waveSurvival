using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsAreaUI : MonoBehaviour
{
    public GameObject heartPrefab;

    private HeartUI[] hearts = new HeartUI[10];//max 10 hearts 

    private int numOfHearts = 0;
    private readonly int maxHearts = 10;


    public void SetUpHeartsUI(int numHearts)
    {
        //numOfHearts = numHearts;

        for (int i = 0; i < numHearts; i++)
        {
            //hearts[i] = Instantiate(heartPrefab,gameObject.transform).GetComponent<HeartUI>();
            AddHeart();
        }
    }

    public void AddHeart()
    {
        if (numOfHearts + 1 <= maxHearts)
        {
            hearts[numOfHearts] = Instantiate(heartPrefab, gameObject.transform).GetComponent<HeartUI>();
            numOfHearts++;
        }
    }

    public void RemoveHeart()
    {
        if (numOfHearts + 1 <= maxHearts)
        {
            numOfHearts--;
            Destroy(hearts[numOfHearts].gameObject);
        }
    }

    public void UpdateHearts(int currentHealth, int health, int healthPerHeart)
    {
        int fullHearts = currentHealth / healthPerHeart;
        int halfHearts = currentHealth % healthPerHeart;
        for (int i = 0; i < fullHearts; i++)
        {
            hearts[i].ChangeHeartSprite(HeartState.full);
        }
        if (halfHearts == 1)
        {
            hearts[fullHearts].ChangeHeartSprite(HeartState.half);
        }
        for (int i = fullHearts+halfHearts; i < numOfHearts; i++)
        {
            hearts[i].ChangeHeartSprite(HeartState.empty);
        }
    }

}
