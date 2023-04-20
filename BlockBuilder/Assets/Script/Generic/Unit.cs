using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit<P, T>
{
    const int LEFT = 0;
    const int RIGHT = 1;
    const int FORWARD = 2;
    const int BACK = 3;

    public int ID;
    public P Vector; //hold position models
    public T Type;  //hold graphic models
    public Possibility<T> Choices;  // choices now had
    public Group<P, T> Group{get; set;}

    public Unit<P, T>[] Relatives{get; set;}
    public Unit<P, T> Up{get; set;}
    public Unit<P, T> Down{get; set;}
    public Unit<P, T> Forward{get; set;}
    public Unit<P, T> Back{get; set;}
    public Unit<P, T> Left{get; set;}
    public Unit<P, T> Right{get; set;}

    public Level<P, T> Level{get; set;}   // point to level
    private bool selected;

    public Unit(int id, P vector, T type, Possibility<T> choices, Level<P, T> level)
    {
        ID = id;
        Vector = vector;
        Type = type;
        Choices = choices;
        Level = level;
        Relatives = new Unit<P, T>[4];
        selected = false;
    }

    public bool SetRandomType(System.Random random)
    {
        if(selected) return false;
        Type = Choices.GetType(random);
        selected = true;
        return true;
    }
    public bool isSelected()
    {
        return selected;
    }

    public P GetVector()
    {
        return Vector;
    }
    public T GetObject()
    {
        return Type;
    }
    public void Select(System.Random random)
    {
        if(selected) return;

        foreach(Unit<P, T> relative in Relatives)
        {
            if(relative.Group == Group) continue;
            //HashSet<T> allowedTypesForThis = Level.Rules.Conditions[relative.Type];
            //Choices.Types.RemoveAll(type => !allowedTypesForThis.Contains(type));
            //移除所有不在规则里的
        }


        SetRandomType(random);



        foreach(Unit<P, T> relative in Relatives)
        {
            if(relative.Group == Group) continue;
            HashSet<T> allowedTypes = Level.Rules.Conditions[Type];
            relative.Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            //移除所有不在规则里的
        }


        

        Up.Choices.Types.RemoveAll(type => !Level.Rules.UpConditions[Type].Contains(type));
        Down.Choices.Types.RemoveAll(type => !Level.Rules.DownConditions[Type].Contains(type));
    }
}

