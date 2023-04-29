using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice<T>{ // 每个Grid会带一个Prossibility
    public List<Type<T>> Types{set; get;}
    public Choice()
    {
        Types = new List<Type<T>>();
    }
    public void Add(Type<T> type)
    {
        Types.Add(type);
    }
    public void RemoveAll(Type<T> type)
    {
        Types.RemoveAll(data => type.Equals(data));
    }

    public Type<T> GetRandomType(System.Random random){
        return Types[random.Next(Types.Count)];
    }

    public Type<T> GetType(int i)
    {
        return Types[i];
    }

    public int Size(){
        return Types.Count;
    }
}
