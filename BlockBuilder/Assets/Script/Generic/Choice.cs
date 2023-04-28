using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Choice<T>{ // 每个Grid会带一个Prossibility
    public List<T> Types{set; get;} // dictionary?
    public List<List<T>> ListedTypes{set; get;}
    public Choice()
    {
        Types = new List<T>();
        ListedTypes = new List<List<T>>();
    }
    public void Add(List<T> listedType)
    {
        if(listedType == null) return;
        ListedTypes.Add(listedType);
    }
    public void RemoveAll(List<T> listedType)
    {
        ListedTypes.RemoveAll(data => listedType.Equals(data));
    }

    public void Add(T type){
        Types.Add(type);
    }


    public void Add(T type, int times){
        for(int i = 0; i < times; i++){
            Types.Add(type);
        }
    }
    public void Remove(T type){
        Types.Remove(type);
    }
    public void RemoveAll(T type){
        Types.RemoveAll(data => type.Equals(data) );
    }



    public T GetRandomType(System.Random random){
        return Types[random.Next(Types.Count)];
    }

    public T GetType(int i)
    {
        return Types[i];
    }

    public int Size(){
        return Types.Count;
    }
}
