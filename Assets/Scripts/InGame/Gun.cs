using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Item
{
    public Transform shootingPoint;
    public abstract override void Use();

    public Transform GetShootingPoint()
    {
        return shootingPoint;
    }
}
