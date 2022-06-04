using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image ammoEmpty;

    public void SetAmmoEmptyFill(float val)
    {
        if (val > 1 || val < 0)
            return;

        ammoEmpty.fillAmount = val;
    }
}
