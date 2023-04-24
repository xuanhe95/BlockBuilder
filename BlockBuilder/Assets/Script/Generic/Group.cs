using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public T Type;
    public int ID;
    public List<Unit<P, T>> Units { get; set; }
    public Choice<T> Choices { get; set; }

    public Group(int id, T type)
    {
        Type = type;
        ID = id;
        Units = new List<Unit<P, T>>();
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
        return Type;
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




}