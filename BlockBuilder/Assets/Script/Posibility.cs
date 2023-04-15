using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Possibility<T>{ // 每个Grid会带一个Prossibility
    public List<T> types{set; get;} // dictionary?
    public Possibility(){
        types = new List<T>();
    }

    public void Add(T type){
        types.Add(type);
    }
    public void Add(T type, int times){
        for(int i = 0; i < times; i++){
            types.Add(type);
        }
    }
    public void Remove(T type){
        types.Remove(type);
    }
    public void RemoveAll(T type){
        types.RemoveAll(data => type.Equals(data) );
    }

    public T GetType(System.Random random){
        return types[random.Next(types.Count)];
    }

    public int Size(){
        return types.Count;
    }
}
