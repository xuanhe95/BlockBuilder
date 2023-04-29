using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public partial class Group<P, T>
{
    public void Regulate(System.Random random)
    {
        HashSet<Type<T>> allowedTypesForLeft = GetLevel().Rules.Conditions[GetLeft().Type];
        HashSet<Type<T>> allowedTypesForRight = GetLevel().Rules.Conditions[GetRight().Type];
        HashSet<Type<T>> allowedTypesForForward = GetLevel().Rules.Conditions[GetForward().Type];
        HashSet<Type<T>> allowedTypesForBack = GetLevel().Rules.Conditions[GetBack().Type];

        HashSet<Type<T>> allowedTypesForUp = GetLevel().Rules.UpConditions[GetUp().Type];
        HashSet<Type<T>> allowedTypesForDown = GetLevel().Rules.DownConditions[GetDown().Type];

        List<T> choices = new List<T>();
        foreach(T type in choices)
        {
            choices.Add(type);
        }

        Choices.Types.RemoveAll(type => !allowedTypesForLeft.Contains(type));
        Choices.Types.RemoveAll(type => !allowedTypesForRight.Contains(type));
        Choices.Types.RemoveAll(type => !allowedTypesForForward.Contains(type));
        Choices.Types.RemoveAll(type => !allowedTypesForBack.Contains(type));
        Choices.Types.RemoveAll(type => !allowedTypesForUp.Contains(type));

        SetRandomType(random);
        //GetLeft().Choices.Types;
    }

    public Level<P, T> GetLevel()
    {
        return Units[0].Level;
    }

    public void SetRandomType(System.Random random)
    {
        Type = Choices.GetRandomType(random);
        Debug.Log("SET TYPE " + Type);
        SetTypes();
    }

    public Type<T> GetType(int id)
    {
        return Choices.Types[id];
    }


    public void SetTypes()
    {
        for(int i = 1; i < Type.Types.Count; i++)
        {
            Units[i-1].SetType(Type.Types[i]);
        }
        Debug.Log("SET TYPE " + Type.GetName());
    }
}
