using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    //public T Type;
    public Type<T> Type;
    public int ID;
    public List<Unit<P, T>> Units { get; set; }
    public Choice<T> Choices { get; set; }
    public GameObject Instance;


    public Group(int id, Type<T> type, Choice<T> choice)
    {
        Type = type;
        ID = id;
        Choices = choice;
        //Map = map;
        //ModMap = modMap;
        Units = new List<Unit<P, T>>();
    }

    public Choice<T> GetChoices(){
        return Choices;
    }
    

    public void AddUnit(Unit<P, T> unit)
    {
        Units.Add(unit);
        unit.Group = this;
    }

    public int Size()
    {
        return Units.Count;
    }

    public T GetObject()
    {
        return Type.GetName();
    }

    public Type<T> GetTypes()
    {
        return Type;
    }

    public Unit<P, T> GetUnit(int id)
    {
        return Units[id];
    }

    public T GetType()
    {
        return Type.GetName();
    }

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


}
