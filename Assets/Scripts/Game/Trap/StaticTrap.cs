using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StaticTrap : BaseTrap
{
    protected override void OnTrapIn(Collider other)
    {
       
    }

    protected override void OnTrapOut(Collider other)
    {
        
    }
}
