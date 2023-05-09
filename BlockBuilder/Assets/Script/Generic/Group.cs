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




}
