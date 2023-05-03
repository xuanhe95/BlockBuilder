using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System;

public partial class Group<P, T>
{
    public Choice<T> CreateTempChoices()
    {
        List<Type<T>> tempChoices = new List<Type<T>>();

        Choices.Types.ForEach(i => tempChoices.Add(i));
        Choice<T> newChoices = new Choice<T>(tempChoices);
        return newChoices;
    }

    public void Select(System.Random random)
    {
        SetType(GetChoices(), random);
    }

    public void RegulateAdd(Choice<T> choices)
    {
        int dir = CheckBackToBack(Map[(int)Geo.Land].GetName());

        switch (dir)
        {
            case Direction.Left:
                AddChoice(choices, Map[(int)Geo.Bridge2], 15);
                break;
            case Direction.Forward:
                AddChoice(choices, Map[(int)Geo.Bridge1], 15);
                break;
            default:
                break;
        }

        dir = CheckAdjacent(Map[(int)Geo.Land].GetName());

        switch (dir)
        {
            case Direction.Left:
                AddChoice(choices, Map[(int)Geo.T1D], 12);
                break;
            case Direction.Right:
                AddChoice(choices, Map[(int)Geo.T1B], 12);
                break;
            case Direction.Forward:
                AddChoice(choices, Map[(int)Geo.T1C], 12);
                break;
            case Direction.Back:
                AddChoice(choices, Map[(int)Geo.T1A], 12);
                break;
            default:
                break;
        }

        dir = CheckHalfSurround(Map[(int)Geo.Land].GetName());
        switch (dir)
        {
            case Direction.Left:
                AddChoice(choices, Map[(int)Geo.T2D], 12);
                break;
            case Direction.Right:
                AddChoice(choices, Map[(int)Geo.T2B], 12);
                break;
            case Direction.Forward:
                AddChoice(choices, Map[(int)Geo.T2C], 12);
                break;
            case Direction.Back:
                AddChoice(choices, Map[(int)Geo.T2A], 12);
                break;
            default:
                break;
        }
    }

    public void AddChoice(Choice<T> choices, Type<T> type, int times)
    {
        Debug.Log("ADD " + times + " Times");
        choices.Add(type, times);
    }

    public void Regulate(Choice<T> choices)
    {
        RegulateSide(choices, Direction.Left);
        RegulateSide(choices, Direction.Right);
        RegulateSide(choices, Direction.Forward);
        RegulateSide(choices, Direction.Back);
        RegulateSide(choices, Direction.Up);
    }

    public void RegulateSide(Choice<T> choices, int direction)
    {
        HashSet<Type<T>> allowedTypes = GetAllowedTypes(direction);
        if (allowedTypes != null)
        {
            choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
        }
        else
        {
            choices.Types.Clear();
        }
    }

    public HashSet<Type<T>> GetAllowedTypes(int direction)
    {
        switch (direction)
        {
            case Direction.Left:
                if (GetLeft() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetLeft().GetTypes(), direction);
            case Direction.Right:
                if (GetRight() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetRight().GetTypes(), direction);
            case Direction.Forward:
                if (GetForward() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetForward().GetTypes(), direction);
            case Direction.Back:
                if (GetBack() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetBack().GetTypes(), direction);
            case Direction.Up:
                if (GetDown() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetDown().GetTypes(), Direction.Down);
            case Direction.Down:
                if (GetUp() == null)
                    return null;
                return GetLevel().GetRule().GetAllowedTypes(GetUp().GetTypes(), Direction.Up);
            default:
                return null;
        }
    }
}
