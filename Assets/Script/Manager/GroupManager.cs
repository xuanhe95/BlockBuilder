using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GroupManager : MonoBehaviour
{
    private Dictionary<int, Type<GameObject>> GeoMap;
    private Dictionary<GameObject, Type<GameObject>> ModMap;
    private Dictionary<int, GameObject> GoMap;
    private Group<GameObject, GameObject> Group;

    public void Initialize(Group<GameObject, GameObject> group,
        Dictionary<int, Type<GameObject>> geoMap, 
        Dictionary<GameObject, Type<GameObject>> modMap,
        Dictionary<int, GameObject> goMap){
                    Group = group;
                    GeoMap = geoMap;
                    ModMap = modMap;
                    GoMap = goMap;
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

    public Choice<GameObject> CreateSelectChoices()
    {
        List<Type<GameObject>> choices = GetChoicesSet();
        Choice<GameObject> newChoices = new Choice<GameObject>(choices);
        return newChoices;
    }

    // public void Select(System.Random random)
    // {
    //     Group.SetType(Group.GetChoices(), random);
    // }

    public void Select(System.Random random){
        Group.SetType(CreateSelectChoices(), random);
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

    //新的生成方法

    public string GetDirection(){
        if(Group == null) return "No Group";
        string dir = "";
        Group<GameObject, GameObject> forward = Group.GetForward();
        Group<GameObject, GameObject> back = Group.GetBack();
        Group<GameObject, GameObject> left = Group.GetLeft();
        Group<GameObject, GameObject> right = Group.GetRight();
        if(forward != null && forward.GetTypes() != GeoMap[(int) Geo.Empty]){
            dir += "1";
        }
        else{
            dir+="0";
        }

        if(back != null && back.GetTypes() != GeoMap[(int) Geo.Empty]){
            dir += "1";
        }
        else{
            dir+="0";
        }

        if(left != null && left.GetTypes() != GeoMap[(int) Geo.Empty]){
            dir += "1";
        }
        else{
            dir+="0";
        }
        if(right != null && right.GetTypes() != GeoMap[(int) Geo.Empty]){
            dir += "1";
        }
        else{
            dir+="0";
        }
        return dir;

    }


    public List<Type<GameObject>> Preview(GroupManager down){
        RuleCreator rc = GoMap[down.GetGroup().GetTypes().id].GetComponent<RuleCreator>();
        List<GameObject> goTypes = rc.Up;
        HashSet<GameObject> wait = new HashSet<GameObject>();
        string dir = GetDirection();
        foreach(GameObject goType in goTypes){
            RuleCreator rules = goType.GetComponent<RuleCreator>();
            
            List<GameObject> list = rules.FixedRules;
        

        switch(dir){
            case "0000":
                wait.Add(list[0]);
                wait.Add(list[1]);
                wait.Add(list[2]);
                wait.Add(list[3]);
                break;
            case "0001":
                wait.Add(list[4]);
                break;
            case "0010":
                wait.Add(list[5]);
                break;
            case "0100":
                wait.Add(list[6]);
                break;
            case "1000":
                wait.Add(list[7]);
                break;
            case "0011":
                wait.Add(list[8]);
                wait.Add(list[9]);
                break;
            case "0110":
                wait.Add(list[12]);
                break;
            case "0101":
                wait.Add(list[15]);
                break;
            case "1001":
                wait.Add(list[13]);
                break;
            case "1010":
                wait.Add(list[14]);
                break;
            case "1100":
                wait.Add(list[10]);
                wait.Add(list[11]);
                break;
            case "1110":
                wait.Add(list[16]);
                break;
            case "1101":
                wait.Add(list[17]);
                break;
            case "1011":
                wait.Add(list[18]);
                break;
            case "0111":
                wait.Add(list[19]);
                break;
            case "1111":
                wait.Add(list[20]);
                wait.Add(list[21]);
                wait.Add(list[22]);
                wait.Add(list[23]);
                break;
            default:
                return new List<Type<GameObject>>();
        }

        }



        //GameObject go = gos[Random.Range(0, gos.Count)];
        
        //Debug.Log("Direction: " + dir);
        //int index = Group.GetTypes().id;
        //Debug.Log("ID: " + index);
        //Debug.Log(GoMap.Count);
        //GameObject go = GoMap[index];
        //Debug.Log(go);
        //RuleCreator rules = GoMap[index].GetComponent<RuleCreator>();
        //Debug.Log(rules);
        //Debug.Log(go.name);
        
        //int selectedIndex = Random.Range(0, wait.Count);
        //Debug.Log(selectedIndex);
        //GameObject select = wait[selectedIndex];  
        //Debug.Log(select.name);  
        List<Type<GameObject>> selections = new List<Type<GameObject>>();
        foreach(GameObject g in wait){
            selections.Add(ModMap[g]);
        }

        return selections;
        //Group.SetType(CreateSelectChoices(), random);
    }

    public void Select(GroupManager down){
        //if(down.GetGroup().GetTypes() == GeoMap[(int) Geo.Empty]) return;
        List<Type<GameObject>> list = Preview(down);
        if(list.Count == 0) return;
        int selectedIndex = Random.Range(0, list.Count);
        Group.SetType(list[selectedIndex]);
    }

    public void Select(Type<GameObject> type){
        //if(Group.FindRelativeGroup(Direction.Down).GetTypes() == GeoMap[(int) Geo.Empty]) return;
        Group.SetType(type);
    }
}
