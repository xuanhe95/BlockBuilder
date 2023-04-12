using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Possibility<T>{ // 每个Grid会带一个Prossibility
    public List<Type<T>> types{set; get;} // dictionary?
    public Possibility(){
        types = new List<Type<T>>();
    }
    public void Add(Type<T> type, int times){
        for(int i = 0; i < times; i++){
            types.Add(type);
        }
    }
    public void Remove(Type<T> type){
        types.Remove(type);
    }
    public void RemoveAll(Type<T> type){
        types.RemoveAll(data => type == data );
    }

    public Type<T> GetType(System.Random random){
        return types[random.Next(types.Count)];
    }
}
