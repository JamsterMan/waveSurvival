using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGold : MonoBehaviour
{
    public int maxGold = 100;
    public Text goldUI;
    [SerializeField]private int currentGold = 0;

    public void AddGold(int amount)
    {
        currentGold += amount;
        if (currentGold > maxGold)
            currentGold = maxGold;

        //Update UI
        UpdateGoldUI();
    }

    public bool RemoveGold(int amount)
    {
        if (currentGold - amount < 0)
            return false;

        currentGold -= amount;

        //Update UI
        UpdateGoldUI();

        return true;
    }

    public bool HasEnoughGold(int amount)
    {
        return currentGold - amount >= 0;
    }

    private void UpdateGoldUI()
    {
        goldUI.text = "" + currentGold;
    }
}
