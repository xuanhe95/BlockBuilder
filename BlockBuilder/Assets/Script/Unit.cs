using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit<P, T>
{
    public int ID;
    public P Vector; //hold position models
    public T Type;  //hold graphic models
    public Possibility<T> Choices;  // choices now had

    public Unit<P, T>[] Relatives{get; set;}
    public Level<P, T> Level{get; set;}   // point to level

    public Unit(int id, P vector, T type, Possibility<T> choices, Level<P, T> level){
        ID = id;
        Vector = vector;
        Type = type;
        Choices = choices;
        Level = level;
        Relatives = new Unit<P, T>[4];
    }

    public bool SetRandomType(System.Random random)
    {
        if(Type != null) return false;
        Type = Choices.GetType(random);
        return true;
    }
    public bool HasType()
    {
        return Type != null;
    }

    public P GetVector(){
        return Vector;
    }
    public T GetObject(){
        return Type;
    }
   
}

