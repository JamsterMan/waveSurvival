using UnityEngine;

public class AmmoAreaUI : MonoBehaviour
{
    public GameObject ammoPrefab;

    private ImageFillUI[] _ammo = new ImageFillUI[10];//max 10 hearts 

    private int _currMaxAmmo = 0;
    private readonly int _maxAmmo = 10;


    public void SetUpAmmoUI(int ammoMax)
    {
        for (int i = 0; i < ammoMax; i++)
        {
            AddAmmo();
        }
    }

    public void AddAmmo()
    {
        if (_currMaxAmmo + 1 <= _maxAmmo)
        {
            _ammo[_currMaxAmmo] = Instantiate(ammoPrefab, gameObject.transform).GetComponent<ImageFillUI>();
            _currMaxAmmo++;
        }
    }

    public void RemoveAmmo()
    {
        if (_currMaxAmmo + 1 <= _maxAmmo)
        {
            _currMaxAmmo--;
            Destroy(_ammo[_currMaxAmmo].gameObject);
        }
    }


    public void UpdateAmmoUI( int currAmmo, int ammoCharge, int chargePerAmmo)
    {
        
        
        for (int i = 0; i < currAmmo; i++)
        {
            _ammo[i].SetImageFill(0f);
        }
        if (currAmmo < _currMaxAmmo)
        {
            float val = ((float)ammoCharge) / ((float)chargePerAmmo);
            _ammo[currAmmo].SetImageFill(1-val);
        }
        for (int i = currAmmo+1; i < _currMaxAmmo; i++)
        {
            _ammo[i].SetImageFill(1f);
        }
    }
}
