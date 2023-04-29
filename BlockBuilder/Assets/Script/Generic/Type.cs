using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type<T>
{
    public T Parent;
    public List<T> Types;
    public Type(List<T> types)
    {
        Types = types;
        Parent = types[0];
    }

    public T GetType(int id)
    {
        return Types[id+1];
    }
    public T GetName()
    {
        return Parent;
    }

    public int Size()
    {
        return Types.Count-1;
    }
}
