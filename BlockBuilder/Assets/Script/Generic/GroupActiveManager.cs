using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{

    public int CheckEqualToType(T type)
    {
        int i = 0;
        if(CheckEqual(GetLeft().GetType(), type)) i++;
        if(CheckEqual(GetRight().GetType(), type)) i++;
        if(CheckEqual(GetForward().GetType(), type)) i++;
        if(CheckEqual(GetBack().GetType(), type)) i++;
        return i;
    }
    public int CheckBackToBack(T type)
    {
        if(CheckHalfSurround(type) != -1) return -2;
        else if(CheckEqual(GetRight().GetType(), GetLeft().GetType(), type)) return Direction.Left;
        else if(CheckEqual(GetForward().GetType(), GetBack().GetType(), type)) return Direction.Forward;
        else return -1;
    }
    public int CheckAdjacent(T type)
    {
        if(CheckHalfSurround(type) != -1) return -2;
        if(CheckEqual(GetLeft().GetType(), GetForward().GetType(), type)) return Direction.Left;
        else if(CheckEqual(GetForward().GetType(), GetRight().GetType(), type)) return Direction.Forward;
        else if(CheckEqual(GetRight().GetType(), GetBack().GetType(), type)) return Direction.Right;
        else if(CheckEqual(GetBack().GetType(), GetLeft().GetType(), type)) return Direction.Back;
        else return -1;
    }

    public int CheckHalfSurround(T type)
    {
        if(CheckSurround(type) != -1) return -2;
        else if(CheckEqual(GetForward().GetType(), GetRight().GetType(), GetBack().GetType(), type)) return Direction.Forward;
        else if(CheckEqual(GetRight().GetType(), GetBack().GetType(),GetLeft().GetType(), type)) return Direction.Right;
        else if(CheckEqual(GetBack().GetType(),GetLeft().GetType(), GetForward().GetType(), type)) return Direction.Back;
        else if(CheckEqual(GetLeft().GetType(), GetForward().GetType(), GetRight().GetType(), type)) return Direction.Left;
        else return -1;
    }

    public int CheckSurround(T type)
    {
        if(CheckEqual(GetLeft().GetType(), GetForward().GetType(), GetRight().GetType(), GetBack().GetType())) return Direction.Left;
        else return -1;
    }

    public Group<P, T> FindRelativeGroup(Group<P, T> group, int direction)
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
