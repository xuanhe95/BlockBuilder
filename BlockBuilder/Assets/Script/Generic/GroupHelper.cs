using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public T GetType()
    {
        return Units[0].Type;
    }

    public Group<P, T> GetLeft()
    {
        Debug.Log("get Left");
        return Units[0].Relatives[Direction.Left].Group;
    }
    public Group<P, T> GetRight()
    {
        Debug.Log("get Right");
        return Units[3].Relatives[Direction.Right].Group;
    }
    public Group<P, T> GetForward()
    {
        return Units[3].Relatives[Direction.Forward].Group;
    }
    public Group<P, T> GetBack(){
        return Units[0].Relatives[Direction.Back].Group;
    }

    public bool CheckEqual(T typeA, T typeB)
    {
        return typeA.Equals(typeB);
    }

    public bool CheckEqual(T typeA, T typeB, T typeC)
    {
        return CheckEqual(typeA, typeB) && CheckEqual(typeB, typeC);
    }

    public bool CheckEqual(T typeA, T typeB, T typeC, T typeD)
    {
        return CheckEqual(typeA, typeB, typeC) && CheckEqual(typeA, typeD);
    }

    public bool CheckEqual(T typeA, T typeB, T typeC, T typeD, T typeE)
    {
        return CheckEqual(typeA, typeB, typeC) && CheckEqual(typeA, typeD, typeE);
    }
}
