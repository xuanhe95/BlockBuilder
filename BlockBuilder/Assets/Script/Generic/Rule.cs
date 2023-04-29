using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule<T>
{  // 每个level有一条rule
    public Dictionary<Type<T>, HashSet<Type<T>>> Conditions{get; set;}
    public Dictionary<Type<T>, HashSet<Type<T>>> UpConditions{get; set;}
    public Dictionary<Type<T>, HashSet<Type<T>>> DownConditions{get; set;}
    public Rule()
    {
        Conditions = new Dictionary<Type<T>, HashSet<Type<T>>>();
        UpConditions = new Dictionary<Type<T>, HashSet<Type<T>>>();
        DownConditions = new Dictionary<Type<T>, HashSet<Type<T>>>();
    }
//*
    public bool AddRule(Type<T> type, Type<T> connection)
    {   

        if(!Conditions.ContainsKey(type))
        {
            Conditions.Add(type, new HashSet<Type<T>>());
        }
        return Conditions[type].Add(connection);

    }
    public bool AddUpRule(Type<T> type, Type<T> connection)
    {   

        if(!UpConditions.ContainsKey(type))
        {
            UpConditions.Add(type, new HashSet<Type<T>>());
        }
        return UpConditions[type].Add(connection);

    }

    public bool AddDownRule(Type<T> type, Type<T> connection)
    {
        return true;
    }
}