using System.Collections;
using System.Collections.Generic;

public abstract class Command
{
    public CommandType type;
}

public enum CommandType
{
    Move,
    Charge,
    Shoot,
    Item,
    Damage,
    Repulse,
}