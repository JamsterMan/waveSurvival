using UnityEngine;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour
{
    private PlayerGameData _data;
    public Text goldUI;
    public Text damageLevelUI;

    public int upgradeCost = 15;

    private void Awake()
    {
        _data = GameObject.Find("PlayerDataObject").GetComponent<PlayerGameData>();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void DamageUpgradeButton()
    {
        if (_data.MenuGold >= upgradeCost)
        {
            _data.DecreaseMenuGold(upgradeCost);
            _data.IncreaseDamageLevel();

            UpdateText();
        }
    }

    private void UpdateText()
    {
        goldUI.text = "" + _data.MenuGold;
        damageLevelUI.text = _data.damageLevel + " ";
    }
}
