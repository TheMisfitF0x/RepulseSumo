using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCommand : Command
{//Command to apply an impulse to an entity over time. This can and should be used over a damage command w/ KB in most cases that aren't generic damage KB.
    //Specifically designed for repulsor shield but most certainly can apply elsewhere.
    //Duration of the impulse in milliseconds. 0 basically stands for an instant shove. 
    public ChargeCommand()
    {
        
    }
}
