using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    private List<Type<T>> ChoicesSet;
    private int ChoicesID = 0;

    public void SetTypes()
    {
        SetUnitTypes();
        Debug.Log("SET TYPE " + Type.GetName());
    }

    public void Reset()
    {
        Type = Map[(int)Geo.Empty];
    }

    public void SetType(Choice<T> choices, System.Random random)
    {
        Type = choices.GetRandomType(random);
        SetUnitTypes();
    }

    public void InitSet(){
        ChoicesSet = GetChoicesSet();
    }

    public void SetNext()
    {
        if(ChoicesID == ChoicesSet.Count){
            ChoicesID = 0;
        }
        Type = ChoicesSet[ChoicesID];
    }

    //public void SetType(Choice)

    public void SetUnitTypes()
    {
        //Debug.Log(Type + "Size " + Type.Types.Count);
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(Type.GetType(i));
            Units[i].SetType(Type.GetType(i));
        }
    }

    public Choice<T> GetChoices()
    {
        Choice<T> choices = CreateTempChoices();
        Regulate(choices);
        RegulateAdd(choices);
        return choices;
    }

    public List<Type<T>> GetChoicesSet(){
        Choice<T> choices = CreateTempChoices();
        Regulate(choices);
        RegulateAdd(choices);
        return choices.GetSet();
    }
}
