using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator animator;
    public MenuScript pauseMenu;

    public int attackDamage = 5;
    public float attackRange = 0.5f;
    public float attackRate = 2f;

    public GameObject energyBlast;
    public float rangedAttackRate = 3f;
    public readonly int maxBlastAmmo = 10;//max player can have
    public int currMaxBlastAmmo = 3;//max player can shoot
    public int chargePerAmmo = 20;
    public Transform firePoint;
    public AmmoAreaUI ammoUI;

    private float _nextAttackTime = 0f;
    private float _nextRangedAttackTime = 0f;
    private int _blastAmmo;
    private int _ammoCharge = 0;//when this = chargePerAmmo, increase ammo by one

    private PlayerGameData _data;

    private void Start()
    {
        _data = GameObject.Find("PlayerDataObject").GetComponent<PlayerGameData>();
        attackDamage += _data.GetBonusDamage();

        _blastAmmo = currMaxBlastAmmo;

        ammoUI.SetUpAmmoUI(_blastAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            if (Time.time > _nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    _nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            if (Time.time > _nextRangedAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    //check if ammo is not 0
                    if (_blastAmmo > 0)
                    {
                        RangedAttack();
                        _nextAttackTime = Time.time + 1f / rangedAttackRate;
                    }
                }
            }
        }
    }

    /*
     * plays the animation for a melee attack
     * sees if any thing was hit, then deal damage
     */
    void Attack()
    {
        //play animations
        animator.SetBool("Attack", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D hitEnemy in hitEnemies)
        {
            hitEnemy.GetComponent<Enemy>().DealDamage(attackDamage);
            //add to next ammo charge
            if (_blastAmmo < currMaxBlastAmmo)
            {
                _ammoCharge += attackDamage;
                if (_ammoCharge >= chargePerAmmo)
                {
                    _ammoCharge = 0;
                    _blastAmmo++;
                }
                //update UI
                ammoUI.UpdateAmmoUI(_blastAmmo, _ammoCharge, chargePerAmmo);
            }
            
        }
    }

    //fires projectile ia prefab
    void RangedAttack()
    {
        //play animations
        //animator.SetBool("Attack", true);

        Instantiate(energyBlast, firePoint.position, firePoint.rotation);//, this.transform);

        //decrease ammo
        _blastAmmo--;
        //update UI
        ammoUI.UpdateAmmoUI(_blastAmmo, _ammoCharge, chargePerAmmo);
    }


    public void ChangePlayerDamage(int amount)
    {
        if (attackDamage + amount > 0)
            attackDamage += amount;
        else
            attackDamage = 1;//minAttack Value
    }

    public void ChangePlayerAttackRate(float amount)
    {
        if (attackRate + amount > 0)
            attackRate += amount;
        else
            attackRate = 1;//minAttack Value
    }

    public void ChangePlayerRangedAttackDamage(float amount)
    {
        //make bellets get damage value from this script first
        Debug.Log("ChangePlayerRangedAttackDamage: not implemented");
    }

    public void ChangePlayerRangedAttackRate(float amount)
    {
        if (rangedAttackRate + amount > 0)
            rangedAttackRate += amount;
        else
            rangedAttackRate = 1;//minAttack Value
    }
    public void ChangePlayerRangedAttackRange(float amount)
    {
        //make bellets get damage value from this script first
        Debug.Log("ChangePlayerRangedAttackRange: not implemented");
    }
    public void ChangePlayerMaxBlastAmmo(int amount)
    {
        if(amount > 0)//adding ammo
        {
            if (currMaxBlastAmmo + amount < maxBlastAmmo)
            {
                _blastAmmo += amount;
                currMaxBlastAmmo += amount;
                ammoUI.AddAmmo();
            }
            else
            {
                currMaxBlastAmmo = maxBlastAmmo;//max Value
                if (_blastAmmo + amount <= currMaxBlastAmmo)
                    _blastAmmo += amount;
            }
        }
        else if(amount < 0)//removing ammo
        {
            if (currMaxBlastAmmo + amount >= 0)
            {
                currMaxBlastAmmo += amount;//amount is negitive so still add it on
                ammoUI.RemoveAmmo();
            }
            else
                currMaxBlastAmmo = 1;//min Value
        }
        ammoUI.UpdateAmmoUI(_blastAmmo, _ammoCharge, chargePerAmmo);
    }
    public void ChangePlayerChargePerAmmo(int amount)
    {
        if (chargePerAmmo + amount > 0)
            chargePerAmmo += amount;
        else
            chargePerAmmo = 1;//minAttack Value
    }

    //draws circle in editor to show attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
