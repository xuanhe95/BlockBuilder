using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule<T>
{  // 每个level有一条rule
    public Dictionary<T, HashSet<T>> rules;
    public Rule()
    {
        rules = new Dictionary<T, HashSet<T>>();
    }
    public bool AddRule(T type, HashSet<T> adjType)
    {
        if(rules.ContainsKey(type)) return false;
        rules.Add(type, adjType);
        return true;
    }
    public bool ClearRule(T type)
    {
        return rules.Remove(type);
    }
}