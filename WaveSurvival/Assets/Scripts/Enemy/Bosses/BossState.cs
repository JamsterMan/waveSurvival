using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    private enum State {chase, attack, charge, p2Chase, p2Attack, p2Charge}
    private State bossState = State.chase;

    private void Update()
    {
        if(bossState == State.chase)//chase player
        {

        }
        else if (bossState == State.attack)//attack the player
        {

        }
        else if (bossState == State.charge)//charge at the player
        {

        }
        else if (bossState == State.p2Chase)//faster chase
        {

        }
        else if (bossState == State.p2Attack)//stronger attack
        {

        }
        else if (bossState == State.p2Charge)//faster charge
        {

        }
    }

}
