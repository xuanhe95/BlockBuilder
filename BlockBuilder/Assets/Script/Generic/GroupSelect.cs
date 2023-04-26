using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public partial class Group<P, T>
{
    public void Regulate(System.Random random)
    {
        HashSet<T> allowedTypesForLeft = GetLevel().Rules.Conditions[GetLeft().Type];
        HashSet<T> allowedTypesForRight = GetLevel().Rules.Conditions[GetRight().Type];
        HashSet<T> allowedTypesForForward = GetLevel().Rules.Conditions[GetForward().Type];
        HashSet<T> allowedTypesForBack = GetLevel().Rules.Conditions[GetBack().Type];

        HashSet<T> allowedTypesForUp = GetLevel().Rules.UpConditions[GetUp().Type];
        HashSet<T> allowedTypesForDown = GetLevel().Rules.DownConditions[GetDown().Type];

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

    public void SetType(int id)
    {
        Type = Choices.GetType(id);
        Debug.Log("SET TYPE " + Type);
    }
    public void SetRandomType(System.Random random)
    {
        Type = Choices.GetRandomType(random);
        Debug.Log("SET TYPE " + Type);
    }

    public T GetType(int id)
    {
        return Choices.Types[id];
    }
}
