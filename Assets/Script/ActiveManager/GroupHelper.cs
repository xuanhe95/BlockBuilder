using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GroupManager : MonoBehaviour
{
    public bool CheckEqual(Type<GameObject> typeA, Type<GameObject> typeB)
    {
        return typeA.Equals(typeB);
    }

    public bool CheckEqual(Type<GameObject> typeA, Type<GameObject> typeB, Type<GameObject> typeC)
    {
        return CheckEqual(typeA, typeB) && CheckEqual(typeB, typeC);
    }

    public bool CheckEqual(Type<GameObject> typeA, Type<GameObject> typeB,
                Type<GameObject> typeC, Type<GameObject> typeD)
    {
        return CheckEqual(typeA, typeB, typeC) && CheckEqual(typeA, typeD);
    }

    public bool CheckEqual(Type<GameObject> typeA, Type<GameObject> typeB, 
        Type<GameObject> typeC, Type<GameObject> typeD, Type<GameObject> typeE)
    {
        return CheckEqual(typeA, typeB, typeC) && CheckEqual(typeA, typeD, typeE);
    }

    public bool CheckEqual(Group<GameObject, GameObject> groupA, Group<GameObject, GameObject> groupB)
    {
        if (groupA == null || groupB == null)
        {
            return true;
        }
        return groupA.GetTypes().Equals(groupB.GetTypes());
    }

    public int CheckEqualToType(Type<GameObject> type)
    {
        int i = 0;
        if (CheckEqual(Group.GetLeft().GetTypes(), type))
            i++;
        if (CheckEqual(Group.GetRight().GetTypes(), type))
            i++;
        if (CheckEqual(Group.GetForward().GetTypes(), type))
            i++;
        if (CheckEqual(Group.GetBack().GetTypes(), type))
            i++;
        return i;
    }
    public int CheckH(Type<GameObject> type)
    {
        if (CheckC(type) != -1)
            return -2;
        else if (CheckEqual(Group.GetRight().GetTypes(), Group.GetLeft().GetTypes(), type))
            return Direction.Left;
        else if (CheckEqual(Group.GetForward().GetTypes(), Group.GetBack().GetTypes(), type))
            return Direction.Forward;
        else
            return -1;
    }

    public int CheckL(Type<GameObject> type)
    {
        if (CheckC(type) != -1)
            return -2;
        if (CheckEqual(Group.GetLeft().GetTypes(), Group.GetForward().GetTypes(), type))
            return Direction.Left;
        else if (CheckEqual(Group.GetForward().GetTypes(), Group.GetRight().GetTypes(), type))
            return Direction.Forward;
        else if (CheckEqual(Group.GetRight().GetTypes(), Group.GetBack().GetTypes(), type))
            return Direction.Right;
        else if (CheckEqual(Group.GetBack().GetTypes(), Group.GetLeft().GetTypes(), type))
            return Direction.Back;
        else
            return -1;
    }

    public int CheckC(Type<GameObject> type)
    {
        if (CheckO(type) != -1)
            return -2;
        else if (
            CheckEqual(Group.GetForward().GetTypes(), Group.GetRight().GetTypes(), Group.GetBack().GetTypes(), type)
        )
            return Direction.Forward;
        else if (CheckEqual(Group.GetRight().GetTypes(), Group.GetBack().GetTypes(), Group.GetLeft().GetTypes(), type))
            return Direction.Right;
        else if (CheckEqual(Group.GetBack().GetTypes(), Group.GetLeft().GetTypes(), Group.GetForward().GetTypes(), type))
            return Direction.Back;
        else if (
            CheckEqual(Group.GetLeft().GetTypes(), Group.GetForward().GetTypes(), Group.GetRight().GetTypes(), type)
        )
            return Direction.Left;
        else
            return -1;
    }

    public int CheckO(Type<GameObject> type)
    {
        Group<GameObject, GameObject> left = Group.GetLeft();
        Group<GameObject, GameObject> forward = Group.GetForward();
        Group<GameObject, GameObject> right = Group.GetRight();
        Group<GameObject, GameObject> back = Group.GetBack();

        if (
            CheckEqual(Group.GetLeft(), Group.GetRight())
            && CheckEqual(Group.GetForward(), Group.GetBack())
            && CheckEqual(Group.GetForward(), Group.GetLeft())
        )
            return Direction.Left;
        else
            return -1;
    }

    public Group<GameObject, GameObject> FindRelativeGroup(int direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                return Group.GetForward();
            case Direction.Back:
                return Group.GetBack();
            case Direction.Left:
                return Group.GetLeft();
            case Direction.Right:
                return Group.GetRight();
            case Direction.Up:
                return Group.GetUp();
            case Direction.Down:
                return Group.GetDown();
            default:
                return null;
        }
    }
}
