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
        Units = new List<Unit<P, T>>();
    }

    public void AddUnit(Unit<P, T> unit)
    {
        Units.Add(unit);
        unit.Group = this;
    }

    public List<Type<T>> GetChoices()
    {
        return Choices.Types;
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

    public void Select(System.Random random)
    {
        Regulate(random);
        Type = Choices.GetRandomType(random);
        Debug.Log(Type + "Size " + Type.Types.Count);
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(Type.GetType(i));
            Units[i].SetType(Type.GetType(i));
        }
    }




}