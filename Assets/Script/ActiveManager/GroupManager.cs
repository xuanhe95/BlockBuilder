using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GroupManager : MonoBehaviour
{
    private Dictionary<int, Type<GameObject>> GeoMap;
    private Dictionary<GameObject, Type<GameObject>> ModMap;
    private Group<GameObject, GameObject> Group;

    public GroupManager(Group<GameObject, GameObject> group,
        Dictionary<int, Type<GameObject>> geoMap, 
        Dictionary<GameObject, Type<GameObject>> modMap){
        Group = group;
        GeoMap = geoMap;
        ModMap = modMap;
    }

    public Choice<GameObject> CreateTempChoices()
    {
        List<Type<GameObject>> tempChoices = new List<Type<GameObject>>();
        Group.GetChoices().GetTypes().ForEach(i => tempChoices.Add(i));
        Choice<GameObject> newChoices = new Choice<GameObject>(tempChoices);
        return newChoices;
    }

    public void Select(System.Random random)
    {
        Group.SetType(Group.GetChoices(), random);
    }

    public void RegulateAdd(Choice<GameObject> choices)
    {
        foreach (GameObject go in ModMap.Keys)
        {
            RuleCreator rg = go.GetComponent<RuleCreator>();
            if (rg == null) continue;
            foreach (GameObject relative in rg.H)
            {
                int direction = CheckH(ModMap[go]);
                switch (direction)
                {
                    case Direction.Left:
                        AddChoice(choices, ModMap[relative], 15);
                        break;
                    case Direction.Forward:
                        AddChoice(choices, ModMap[relative], 15);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void AddChoice(Choice<GameObject> choices, Type<GameObject> type, int times)
    {
        //Debug.Log("ADD " + times + " Times");
        choices.Add(type, times);
    }

    public void Regulate(Choice<GameObject> choices)
    {
        RegulateSide(choices, Direction.Left);
        RegulateSide(choices, Direction.Right);
        RegulateSide(choices, Direction.Forward);
        RegulateSide(choices, Direction.Back);
        RegulateSide(choices, Direction.Up);
    }

    public void RegulateSide(Choice<GameObject> choices, int direction)
    {
        HashSet<Type<GameObject>> allowedTypes = GetAllowedTypes(direction);
        if (allowedTypes != null)
        {
            choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
        }
        else
        {
            choices.Types.Clear();
        }
    }

    public HashSet<Type<GameObject>> GetAllowedTypes(int direction)
    {
        switch (direction)
        {
            case Direction.Left:
                if (Group.GetLeft() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetLeft().GetTypes(), direction);
            case Direction.Right:
                if (Group.GetRight() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetRight().GetTypes(), direction);
            case Direction.Forward:
                if (Group.GetForward() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetForward().GetTypes(), direction);
            case Direction.Back:
                if (Group.GetBack() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetBack().GetTypes(), direction);
            case Direction.Up:
                if (Group.GetDown() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetDown().GetTypes(), Direction.Down);
            case Direction.Down:
                if (Group.GetUp() == null)
                    return null;
                return Group.GetLevel().GetRule().GetAllowedTypes(Group.GetUp().GetTypes(), Direction.Up);
            default:
                return null;
        }
    }

}
