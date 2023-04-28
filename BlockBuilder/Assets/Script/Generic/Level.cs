using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level<P, T>
{  // 每个level有一个平面图
    public int ID;
    public double Height;
    public int Length;
    public int Width;
    public Dictionary<int, Unit<P, T>> Units{get; set;}
    public Dictionary<int, Group<P, T>> Groups{get; set;}
    public Rule<T> Rules{get; set;}
    public Level<P, T> Up {get; set;}
    public Level<P, T> Down {get; set;}



    //public Possibility<T> choices;
    public Level(int level, int width, int length, double height){
        ID = level;
        Height = height;
        Units = new Dictionary<int, Unit<P, T>>();
        Groups = new Dictionary<int, Group<P, T>>();
        Rules= new Rule<T>();
        Up = null;
        Down = null;

        Length = length;
        Width = width;

    }

    public bool AddUnit(Unit<P, T> unit){
        if(Units.ContainsKey(unit.ID)) return false;
        Units.Add(unit.ID, unit);
        return true;
    }
    public bool AddGroup(Group<P, T> group){
        if(Units.ContainsKey(group.ID)) return false;
        Groups.Add(group.ID, group);
        return true;
    }
    public bool SetRule(Rule<T> rule){
        //if(Rules != null) return false;
        foreach(T type in rule.Conditions.Keys)
        {
            //Debug.Log("Added: " + type);
        }
        Rules = rule;
        return true;
    }
}



