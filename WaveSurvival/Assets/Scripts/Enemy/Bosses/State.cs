using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /*
     * what to do when entering the state
     */
    public abstract void EnterState(BossState boss);

    /*
     * what to do each time update is called
     */
    public abstract void UpdateState(BossState boss);

    /*
     * what to do each time fixed update is called
     */
    public abstract void FixedUpdateState(BossState boss);

    /*
     * checks conditions to see when to change states
     */
    public abstract void CheckStateSwitch(BossState boss);
}
