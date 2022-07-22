using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGold : MonoBehaviour
{
    public int maxGold = 100;
    public Text goldUI;
    [SerializeField]private int _currentGold = 0;

    public void AddGold(int amount)
    {
        _currentGold += amount;
        if (_currentGold > maxGold)
            _currentGold = maxGold;

        //Update UI
        UpdateGoldUI();
    }

    public bool RemoveGold(int amount)
    {
        if (_currentGold - amount < 0)
            return false;

        _currentGold -= amount;

        //Update UI
        UpdateGoldUI();

        return true;
    }

    public bool HasEnoughGold(int amount)
    {
        return _currentGold - amount >= 0;
    }

    private void UpdateGoldUI()
    {
        goldUI.text = "" + _currentGold;
    }
}
