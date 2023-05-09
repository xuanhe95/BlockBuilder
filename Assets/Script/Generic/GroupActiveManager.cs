using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{

    public void SetTypes()
    {
        SetUnitTypes();
        Debug.Log("SET TYPE " + Type.GetName());
    }


    public void SetType(Type<T> type)
    {
        Type = type;
        SetUnitTypes();
    }

    public void SetType(Choice<T> choices, System.Random random)
    {
        Type<T> temp = choices.GetRandomType(random);
        if (temp == null)
            return;
        Type = temp;
        SetUnitTypes();
    }

    public void SetUnitTypes()
    {
        //Debug.Log(Type + "Size " + Type.Types.Count);
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log(Type.GetType(i));
            Units[i].SetType(Type.GetType(i));
        }
    }



    // public Choice<T> GetChoices()
    // {
    //     Choice<T> choices = CreateTempChoices();
    //     Regulate(choices);
    //     RegulateAdd(choices);
    //     return choices;
    // }

    // public List<Type<T>> GetChoicesSet()
    // {
    //     Choice<T> choices = CreateTempChoices();
    //     Regulate(choices);
    //     RegulateAdd(choices);
    //     return choices.GetSet();
    // }
}
