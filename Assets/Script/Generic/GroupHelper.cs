using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public Level<P, T> GetLevel()
    {
        return Units[0].Level;
    }

    public Group<P, T> GetAdjacentGroup(int direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return GetLeft();
            case Direction.Right:
                return GetRight();
            case Direction.Forward:
                return GetForward();
            case Direction.Back:
                return GetBack();
            case Direction.Up:
                return GetUp();
            case Direction.Down:
                return GetDown();
            default:
                return null;
        }
    }

    public Group<P, T> GetLeft()
    {
        Debug.Log("get Left");
        Unit<P, T> unit = Units[0].Relatives[Direction.Left];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetRight()
    {
        Debug.Log("get Right");
        Unit<P, T> unit = Units[3].Relatives[Direction.Right];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetForward()
    {
        //Debug.Log("get Forward");
        Unit<P, T> unit = Units[3].Relatives[Direction.Forward];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetBack()
    {
        //Debug.Log("get Back");
        Unit<P, T> unit = Units[0].Relatives[Direction.Back];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetUp()
    {
        //Debug.Log("get Up");
        if (Units[0].Level.Up == null)
            return null;
        Unit<P, T> unit = Units[0].Level.Up.Units[Units[0].ID];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetDown()
    {
        //Debug.Log("get Down");
        if (Units[0].Level.Down == null)
            return null;
        Unit<P, T> unit = Units[0].Level.Down.Units[Units[0].ID];
        if (unit == null)
            return null;
        return unit.Group;
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

    public bool CheckEqual(Group<P, T> groupA, Group<P, T> groupB)
    {
        if (groupA == null || groupB == null)
        {
            return true;
        }
        return groupA.GetType().Equals(groupB.GetType());
    }

    public int CheckEqualToType(T type)
    {
        int i = 0;
        if (CheckEqual(GetLeft().GetType(), type))
            i++;
        if (CheckEqual(GetRight().GetType(), type))
            i++;
        if (CheckEqual(GetForward().GetType(), type))
            i++;
        if (CheckEqual(GetBack().GetType(), type))
            i++;
        return i;
    }

    public int CheckBackToBack(T type)
    {
        if (CheckHalfSurround(type) != -1)
            return -2;
        else if (CheckEqual(GetRight().GetType(), GetLeft().GetType(), type))
            return Direction.Left;
        else if (CheckEqual(GetForward().GetType(), GetBack().GetType(), type))
            return Direction.Forward;
        else
            return -1;
    }

    public int CheckAdjacent(T type)
    {
        if (CheckHalfSurround(type) != -1)
            return -2;
        if (CheckEqual(GetLeft().GetType(), GetForward().GetType(), type))
            return Direction.Left;
        else if (CheckEqual(GetForward().GetType(), GetRight().GetType(), type))
            return Direction.Forward;
        else if (CheckEqual(GetRight().GetType(), GetBack().GetType(), type))
            return Direction.Right;
        else if (CheckEqual(GetBack().GetType(), GetLeft().GetType(), type))
            return Direction.Back;
        else
            return -1;
    }

    public int CheckHalfSurround(T type)
    {
        if (CheckSurround(type) != -1)
            return -2;
        else if (
            CheckEqual(GetForward().GetType(), GetRight().GetType(), GetBack().GetType(), type)
        )
            return Direction.Forward;
        else if (CheckEqual(GetRight().GetType(), GetBack().GetType(), GetLeft().GetType(), type))
            return Direction.Right;
        else if (CheckEqual(GetBack().GetType(), GetLeft().GetType(), GetForward().GetType(), type))
            return Direction.Back;
        else if (
            CheckEqual(GetLeft().GetType(), GetForward().GetType(), GetRight().GetType(), type)
        )
            return Direction.Left;
        else
            return -1;
    }

    public int CheckSurround(T type)
    {
        Group<P, T> left = GetLeft();
        Group<P, T> forward = GetForward();
        Group<P, T> right = GetRight();
        Group<P, T> back = GetBack();

        if (
            CheckEqual(GetLeft(), GetRight())
            && CheckEqual(GetForward(), GetBack())
            && CheckEqual(GetForward(), GetLeft())
        )
            return Direction.Left;
        else
            return -1;
    }

    public Group<P, T> FindRelativeGroup(int direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                return GetForward();
            case Direction.Back:
                return GetBack();
            case Direction.Left:
                return GetLeft();
            case Direction.Right:
                return GetRight();
            case Direction.Up:
                return GetUp();
            case Direction.Down:
                return GetDown();
            default:
                return null;
        }
    }
}
