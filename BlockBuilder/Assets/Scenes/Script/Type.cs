using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type<T>
{
    public int ID;
    public T mesh;
    Type(int id, T ms){
        ID = id;
        mesh = ms;
    }
}
