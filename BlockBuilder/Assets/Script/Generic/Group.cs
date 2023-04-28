using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public T Type;
    public List<T> Types;
    public int ID;
    public List<Unit<P, T>> Units { get; set; }
    public Choice<T> Choices { get; set; }

    public Group(int id, List<T> types)
    {
        Types = types;
        ID = id;
        Units = new List<Unit<P, T>>();

        //Debug.Log(Types);
    }

    public void AddSubTypes(List<T> types)
    {
        Types = types;
    }

    public void AddUnit(Unit<P, T> unit)
    {
        Units.Add(unit);
        unit.Group = this;
    }

    public List<T> GetChoices()
    {
        return Choices.Types;
    }


    public int Size()
    {
        return Units.Count;
    }

    public T GetObject()
    {
        return Types[0];
    }

    public Unit<P, T> GetUnit(int id)
    {
        return Units[id];
    }

    public void Select(System.Random random)
    {
        foreach (Unit<P, T> unit in Units)
        {
            unit.Select();
        }

        SetType(random);
    }

    public void SetType(System.Random random)
    {


        int i = random.Next(Units[0].Choices.Types.Count);


        foreach (Unit<P, T> unit in Units)
        {
            unit.SetType(i);
        }

    }


    public void Choose(System.Random random)
    {
        Types = Choices.ListedTypes[random.Next(Choices.Types.Count)];
        for(int i = 0; i < 4; i++)
        {
            Units[i].SetType(Types[i]);
        }
    }




}