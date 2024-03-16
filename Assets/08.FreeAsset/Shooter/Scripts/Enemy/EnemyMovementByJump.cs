using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementByJump : EnemyMovement
{
    public void jump()
    {
        if (base.nav.enabled)
        {
            base.nav.isStopped = false;
        }
    }
    public void landing()
    {
        if (base.nav.enabled)
        {
            base.nav.isStopped = true;
        }
    }
}
