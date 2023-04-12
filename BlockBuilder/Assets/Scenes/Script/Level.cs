using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level<P, T>
{  // 每个level有一个平面图
    public int level;
    public Dictionary<int, Unit<P, T>> units;
    public Rule<T> rule;
    public Level<P, T> up {get; set;}
    public Level<P, T> down {get; set;}
    //public Possibility<T> choices;
    public Level(int levelID){
        level = levelID;
        units = new Dictionary<int, Unit<P, T>>();
        rule = new Rule<T>();
        up = null;
        down = null;
    }
}