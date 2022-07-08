using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAreaUI : MonoBehaviour
{
    public GameObject ammoPrefab;

    private ImageFillUI[] ammo = new ImageFillUI[10];//max 10 hearts 

    private int currMaxAmmo = 0;
    private readonly int maxAmmo = 10;


    public void SetUpAmmoUI(int ammoMax)
    {
        for (int i = 0; i < ammoMax; i++)
        {
            AddAmmo();
        }
    }

    public void AddAmmo()
    {
        if (currMaxAmmo + 1 <= maxAmmo)
        {
            ammo[currMaxAmmo] = Instantiate(ammoPrefab, gameObject.transform).GetComponent<ImageFillUI>();
            currMaxAmmo++;
        }
    }

    public void RemoveAmmo()
    {
        if (currMaxAmmo + 1 <= maxAmmo)
        {
            currMaxAmmo--;
            Destroy(ammo[currMaxAmmo].gameObject);
        }
    }


    public void UpdateAmmoUI( int currAmmo, int ammoCharge, int chargePerAmmo)
    {
        
        
        for (int i = 0; i < currAmmo; i++)
        {
            ammo[i].SetImageFill(0f);
        }
        if (currAmmo < currMaxAmmo)
        {
            float val = ((float)ammoCharge) / ((float)chargePerAmmo);
            ammo[currAmmo].SetImageFill(1-val);
        }
        for (int i = currAmmo+1; i < currMaxAmmo; i++)
        {
            ammo[i].SetImageFill(1f);
        }
    }
}
