using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GroupManager : MonoBehaviour
{
    private Dictionary<int, Type<GameObject>> GeoMap;
    private Dictionary<GameObject, Type<GameObject>> ModMap;
    private Group<GameObject, GameObject> Group;

    public void Initialize(Group<GameObject, GameObject> group,
        Dictionary<int, Type<GameObject>> geoMap, 
        Dictionary<GameObject, Type<GameObject>> modMap){
                    Group = group;
        GeoMap = geoMap;
        ModMap = modMap;
        }
    public Group<GameObject, GameObject> GetGroup(){
        return Group;
    }
    public void SetEmpty(){
        Group.SetType(GeoMap[(int) Geo.Empty]);
    }

    public Choice<GameObject> CreateTempChoices()
    {
        List<Type<GameObject>> tempChoices = new List<Type<GameObject>>();
        Group.GetChoices().GetTypes().ForEach(i => tempChoices.Add(i));
        Choice<GameObject> newChoices = new Choice<GameObject>(tempChoices);
        return newChoices;
    }

    // public void Select(System.Random random)
    // {
    //     Group.SetType(Group.GetChoices(), random);
    // }

    public void Select(System.Random random){
        Group.SetType(CreateTempChoices(), random);
    }

    public void RegulateAdd(Choice<GameObject> choices)
    {
        foreach (GameObject go in ModMap.Keys)
        {
            RuleCreator rg = go.GetComponent<RuleCreator>();
            if (rg == null) continue;
            AddH(choices, rg, go);
            AddL(choices, rg, go);
            AddC(choices, rg, go);
        }
    }

    private void AddH(Choice<GameObject> choices, RuleCreator rc, GameObject go){
        int dir = CheckH(ModMap[go]);
        switch(dir){
            case Direction.Left:
                foreach(GameObject relative in rc.HL){
                    AddChoice(choices, ModMap[relative], 15);
                }
            break;
            case Direction.Forward:
                foreach(GameObject relative in rc.HF){
                    AddChoice(choices, ModMap[relative], 15);
                }
            break;
        }
    }

    private void AddL(Choice<GameObject> choices, RuleCreator rc, GameObject go){
        int dir = CheckL(ModMap[go]);
        switch(dir){
            case Direction.Left:
                foreach(GameObject relative in rc.LL){
                    AddChoice(choices, ModMap[relative], 12);

                }                    break;
            case Direction.Right:
                foreach(GameObject relative in rc.LR){
                    AddChoice(choices, ModMap[relative], 12);

                }                    break;
            case Direction.Forward:
                foreach(GameObject relative in rc.LF){
                    AddChoice(choices, ModMap[relative], 12);

                }                    break;
            case Direction.Back:
                foreach(GameObject relative in rc.LB){
                    AddChoice(choices, ModMap[relative], 12);

                }                    break;
        }
    }

    private void AddC(Choice<GameObject> choices, RuleCreator rc, GameObject go){
        int dir = CheckC(ModMap[go]);
        switch(dir){
            case Direction.Left:
                foreach(GameObject relative in rc.CL){
                    AddChoice(choices, ModMap[relative], 12);
                }
                                    break;
            case Direction.Right:
                foreach(GameObject relative in rc.CR){
                    AddChoice(choices, ModMap[relative], 12);

                }
                                    break;
            case Direction.Forward:
                foreach(GameObject relative in rc.CF){
                    AddChoice(choices, ModMap[relative], 12);
                    
                }
                break;
            case Direction.Back:
                foreach(GameObject relative in rc.CB){
                    AddChoice(choices, ModMap[relative], 12);

                }
                                    break;
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
