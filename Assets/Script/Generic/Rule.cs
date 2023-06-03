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

    public Dictionary<Type<T>, HashSet<Type<T>>> GetRules(int direction)
    {
        switch(direction)
        {
            case Direction.Left:
            case Direction.Right:
            case Direction.Forward:
            case Direction.Back:
                return Conditions;
            case Direction.Up:
                return DownConditions;
            case Direction.Down:
                return UpConditions;
            default:
                return Conditions;

        }
    }

    public HashSet<Type<T>> GetAllowedTypes(Type<T> type, int direction)
    {
        if(type == null) return null;
        HashSet<Type<T>> allowedTypes = null;
        GetRules(direction).TryGetValue(type, out allowedTypes);
        
        return allowedTypes;

    }

    public bool AddDownRule(Type<T> type, Type<T> connection)
    {
        return true;
    }
}