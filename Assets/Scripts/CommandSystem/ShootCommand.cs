using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command
{
    public bool isCharging;
    public ShootCommand(bool stillCharging)
    {
        type = CommandType.Shoot;
        isCharging = stillCharging;
    }
}
