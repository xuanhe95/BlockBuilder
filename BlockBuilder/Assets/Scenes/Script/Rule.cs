using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule<T>{  // 每个level有一条rule
    public Dictionary<Type<T>, HashSet<Type<T>>> rules;
    public Rule(){
        rules = new Dictionary<Type<T>, HashSet<Type<T>>>();
    }
    public bool AddRule(Type<T> type, HashSet<Type<T>> adjType){
        if(rules.ContainsKey(type)) return false;
        rules.Add(type, adjType);
        return true;
    }
    public bool ClearRule(Type<T> type){
        return rules.Remove(type);
    }
}