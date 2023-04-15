using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level<P, T>
{  // 每个level有一个平面图
    public int ID;
    public double Height;
    public Dictionary<int, Unit<P, T>> Units{get; set;}
    public Rule<T> Rules{get; set;}
    public Level<P, T> Up {get; set;}
    public Level<P, T> Down {get; set;}



    //public Possibility<T> choices;
    public Level(int level, double height){
        ID = level;
        Height = height;
        Units = new Dictionary<int, Unit<P, T>>();
        Rules= new Rule<T>();
        Up = null;
        Down = null;
    }

    public bool AddUnit(Unit<P, T> unit){
        if(Units.ContainsKey(unit.ID)) return false;
        Units.Add(unit.ID, unit);
        return true;
    }
    public bool AddRule(Rule<T> rule){
        if(Rules != null) return false;
        Rules = rule;
        return true;
    }
}



