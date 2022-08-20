using UnityEngine;
using UnityEngine.UI;

public class MenuShop : MonoBehaviour
{
    private PlayerGameData data;
    public Text goldUI;
    private void Awake()
    {
        data = GameObject.Find("PlayerDataObject").GetComponent<PlayerGameData>();
    }

    private void OnEnable()
    {
        goldUI.text = "" + data.MenuGold;
    }
}
