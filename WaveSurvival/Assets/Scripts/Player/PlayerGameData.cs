using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameData : MonoBehaviour
{
    public int MenuGold { get; private set; } = 0;

    public static PlayerGameData instance = null;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseMenuGold(int amount)
    {
        MenuGold += amount;
        Debug.Log("gold added, menuGold = " + MenuGold);
    }

    public void DecreaseMenuGold(int amount)
    {
        MenuGold -= amount;

        Debug.Log("gold removed, menuGold = " + MenuGold);
    }
}