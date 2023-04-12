using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlock : MonoBehaviour
{
    public MeshPrefab mesh;
    public int level;
    public List<string> bitmask;
    private void Start()
    {
        
    }

    public void InitialUnit(List<List<Vector3>> listList)
    {
        foreach (List<Vector3> VARIABLE in listList)
        {
            //initial
        }
    }
}
