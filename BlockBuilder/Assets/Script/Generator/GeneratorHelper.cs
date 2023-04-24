using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator
{
    public void SetRotation(Unit<GameObject, GameObject> unit, float x, float y, float z)
    {
        unit.Vector.transform.rotation = Quaternion.Euler(x, y, z);
    }

    public Quaternion GetRotation(Unit<GameObject, GameObject> unit)
    {
        return unit.Vector.transform.rotation;
    }
}
