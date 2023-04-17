using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule<T>
{  // 每个level有一条rule
    public Dictionary<T, HashSet<T>> Conditions{get; set;}
    public Dictionary<T, HashSet<T>> UpConditions{get; set;}
    public Dictionary<T, HashSet<T>> DownConditions{get; set;}
    public Rule()
    {
        Conditions = new Dictionary<T, HashSet<T>>();
        UpConditions = new Dictionary<T, HashSet<T>>();
        DownConditions = new Dictionary<T, HashSet<T>>();
    }
    public bool AddRule(T type, HashSet<T> adjType)
    {
        if(Conditions.ContainsKey(type)) return false;
        Conditions.Add(type, adjType);
        return true;
    }

    public bool AddUpRule(T type, HashSet<T> adjType)
    {
        if(UpConditions.ContainsKey(type)) return false;
        UpConditions.Add(type, adjType);
        return true;
    }

    public bool AddDownRule(T type, HashSet<T> adjType)
    {
        if(DownConditions.ContainsKey(type)) return false;
        DownConditions.Add(type, adjType);
        return true;
    }



    public bool ClearRule(T type)
    {
        return Conditions.Remove(type);
    }
}