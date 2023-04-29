using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public partial class Group<P, T>
{

    public void Select(System.Random random)
    {
        Regulate();
        if(Choices.GetRandomType(random) == null) return;
        Type = Choices.GetRandomType(random);
        Debug.Log(Type + "Size " + Type.Types.Count);
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(Type.GetType(i));
            Units[i].SetType(Type.GetType(i));
        }
    }

    public void Regulate()
    {
        HashSet<Type<T>> allowedTypes;
        

        if(GetLeft() != null)
        {
            allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetLeft().GetTypes(), Direction.Left);
            if(allowedTypes != null){
                Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            }
            else
            {
                Choices.Types.Clear();
            }
        }
        if(GetRight() != null)
        {
            allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetRight().GetTypes(), Direction.Right);
            if(allowedTypes != null){
                Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            }
            else
            {
                Choices.Types.Clear();
            }
        }
        if(GetForward() != null)
        {
            allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetForward().GetTypes(), Direction.Forward);
            if(allowedTypes != null){
                Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            }
            else
            {
                Choices.Types.Clear();
            }
        }
        if(GetBack() != null)
        {
            allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetBack().GetTypes(), Direction.Back);
            if(allowedTypes != null){
                Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            }
            else
            {
                Choices.Types.Clear();
            }
        }
        if(GetDown() != null)
        {
            allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetDown().GetTypes(), Direction.Down);
            if(allowedTypes != null){
                Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            }
            else
            {
                Choices.Types.Clear();
            }
        }
        // if(GetUp() != null)
        // {
        //     allowedTypes = GetLevel().GetRule().GetAllowedTypes(GetUp().GetTypes(), Direction.Up);
        //     if(allowedTypes != null){
        //         Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
        //     }
        //     else
        //     {
        //         Choices.Types.Clear();
        //     }
        // }
        
        
    }

    public Level<P, T> GetLevel()
    {
        return Units[0].Level;
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
