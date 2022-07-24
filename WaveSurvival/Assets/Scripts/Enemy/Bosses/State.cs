using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void EnterState(BossState boss);

    public abstract void UpdateState(BossState boss);

    public abstract void FixedUpdateState(BossState boss);

    public abstract void CheckStateSwitch(BossState boss);
}
