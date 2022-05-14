using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int maxGold = 100;
    [SerializeField]private int currentGold = 0;

    public void AddGold(int amount)
    {
        currentGold += amount;
        if (currentGold > maxGold)
            currentGold = maxGold;

        //Update UI
        Debug.Log("Gold Added, total Gold: " + currentGold);
    }

    public bool RemoveGold(int amount)
    {
        if (currentGold - amount < 0)
            return false;

        currentGold -= amount;

        //Update UI
        Debug.Log("Gold Removed, total Gold: " + currentGold);

        return true;
    }

    public bool HasEnoughGold(int amount)
    {
        return currentGold - amount >= 0;
    }
}
