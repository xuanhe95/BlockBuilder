using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public T GetType()
    {
        return Units[0].Type;
    }

    public Group<P, T> GetAdjacentGroup(int direction)
    {
        switch(direction)
        {
            case Direction.Left: return GetLeft();
            case Direction.Right: return GetRight();
            case Direction.Forward: return GetForward();
            case Direction.Back: return GetBack();
            case Direction.Up: return GetUp();
            case Direction.Down: return GetDown();
            default: return null;
        }
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
        Debug.Log("get Forward");
        return Units[3].Relatives[Direction.Forward].Group;
    }
    public Group<P, T> GetBack(){
        Debug.Log("get Back");
        return Units[0].Relatives[Direction.Back].Group;
    }
    public Group<P, T> GetUp()
    {
        Debug.Log("get Up");
        return Units[0].Level.Up.Units[Units[0].ID].Group;
    }
    public Group<P, T> GetDown()
    {
        Debug.Log("get Down");
        return Units[0].Level.Down.Units[Units[0].ID].Group;
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
