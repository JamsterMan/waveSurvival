using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ChaseState : State
{

    public override void EnterState(BossState boss)
    {
        Debug.Log("Entered phase 2 chase State");
    }

    public override void UpdateState(BossState boss)
    {

    }

    public override void FixedUpdateState(BossState boss)
    {

    }
}
