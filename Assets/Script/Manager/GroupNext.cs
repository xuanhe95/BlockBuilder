using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GroupManager : MonoBehaviour
{
    private List<Type<GameObject>> ChoicesSet;
    private int ChoicesID = 0;
    private HashSet<Group<GameObject, GameObject>> visited = new HashSet<Group<GameObject, GameObject>>();

    public List<Type<GameObject>> GetChoicesSet()
    {
        Choice<GameObject> choices = CreateTempChoices();
        Regulate(choices);
        RegulateAdd(choices);
        return choices.GetSet();
    }
    public void InitSet()
    {
        ChoicesSet = GetChoicesSet();
    }

    public void SetNext()
    {
        if (ChoicesID == ChoicesSet.Count)
        {
            ChoicesID = 0;
        }
        Group.Type = ChoicesSet[ChoicesID];
    }





}



