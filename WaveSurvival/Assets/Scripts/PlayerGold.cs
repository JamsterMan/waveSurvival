using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int maxGold = 100;
    private int currentGold = 0;

    public void AddGold(int amount)
    {
        currentGold += amount;
        if (currentGold > maxGold)
            currentGold = maxGold;

        //Update UI
        Debug.Log("total Gold: " + currentGold);
    }

    public bool RemoveGold(int amount)
    {
        if (currentGold - amount < 0)
            return false;

        currentGold -= amount;
        //Update UI

        return true;
    }
}
