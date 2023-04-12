using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit<P, T>
{
    public P point;
    //public Type type;
    public Possibility<T> choices;
    public Type<T>? type { get; set; }
    public int id;
    public List<Unit<P, T>> adj;

    public Level<P, T> level;
    public Unit<P, T> up{get; set;}
    public Unit<P, T> down{get; set;}

    public Unit(P pt){
        point = pt;
        level = new Level<P, T>(1);
    }
 
    public int GetDegree(){
        return adj.Count;
    }

    public Level<P, T> GetLevel(){
        return level;
    }

    public void SetType(System.Random random)
    {
        //type = choices.GetType(random);
    }
    public bool hasType()
    {
        return type != null;
    }

    public P getP(){
        return point;
    }
   
}
