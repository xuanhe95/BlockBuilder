using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice<T>
{ // 每个Grid会带一个Prossibility
    public List<Type<T>> Types { set; get; }

    public Choice()
    {
        Types = new List<Type<T>>();
    }

    public Choice(List<Type<T>> types){
        Types = types;
    }

    public void Add(Type<T> type)
    {
        Types.Add(type);
    } 

    public void Add(Type<T> type, int times)
    {
        Debug.Log("ADD " + times);
        for (int i = 0; i < times; i++)
        {
            Types.Add(type);
        }
    }

    public void RemoveAll(Type<T> type)
    {
        Types.RemoveAll(data => type.Equals(data));
    }

    public Type<T> GetRandomType(System.Random random)
    {
        Debug.Log(Types.Count);
        if (Types.Count == 0)
            return null;
        Type<T> type = Types[random.Next(Types.Count)];
        //Debug.Log(type);
        return type;
    }

    public Type<T> GetType(int i)
    {
        return Types[i];
    }

    public int Size()
    {
        return Types.Count;
    }
}
