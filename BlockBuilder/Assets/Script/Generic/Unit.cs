using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit<P, T>
{
    public int ID;
    public P Vector; //hold position models
    public T Type;  //hold graphic models
    public Choice<T> Choices;  // choices now had
    public Group<P, T> Group{get; set;}

    public Unit<P, T>[] Relatives{get; set;}
    public Level<P, T> Level{get; set;}   // point to level
    private bool selected;

    public Unit(int id, P vector, T type, Choice<T> choices, Level<P, T> level)
    {
        ID = id;
        Vector = vector;
        Type = type;
        Choices = choices;
        Level = level;
        Relatives = new Unit<P, T>[6];
        selected = false;
        Group = null;
    }

    public bool SetRandomType(System.Random random)
    {
        //if(selected) return false;
        Type = Choices.GetRandomType(random);
        //Debug.Log("SET TYPE " + Type);
        selected = true;
        return true;
    }

    public bool SetType(int i)
    {
        //if(selected) return false;
        Type = Choices.GetType(i);
        Debug.Log("SET TYPE " + Type);
        selected = true;
        return true;
    }

    public bool isSelected()
    {
        return selected;
    }

    public P GetVector()
    {
        return Vector;
    }



    public T GetObject()
    {
        return Type;
    }
    public void PreSelect()
    {
        if(selected) return;
        Unify();

        foreach(Unit<P, T> relative in Relatives)
        {
            //Debug.Log("CHECK" + relative);
            if(relative == null){
                //Debug.Log("wrong");
                continue;
            }
            if(relative.Group == Group) continue;
            //Debug.Log(Level.Rules.Conditions);

            foreach(T type in Level.Rules.Conditions.Keys)
            {
                //Debug.Log("TYPE CHECK");
                //Debug.Log(type);
            }

            HashSet<T> allowedTypesForThis = Level.Rules.Conditions[relative.Type];
            Choices.Types.RemoveAll(type => !allowedTypesForThis.Contains(type));
            //移除所有不在规则里的
        }




        //SetRandomType(random);
        



        //Up.Choices.Types.RemoveAll(type => !Level.Rules.UpConditions[Type].Contains(type));
        //Down.Choices.Types.RemoveAll(type => !Level.Rules.DownConditions[Type].Contains(type));
    }

    public void Select()
    {
        PreSelect();
        Unify();
        AfterSelect();
        //Unify();
    }


    public void AfterSelect()
    {
        

        foreach(Unit<P, T> relative in Relatives)
        {
            if(relative == null) continue;
            if(relative.Group == Group) continue;
            HashSet<T> allowedTypes = Level.Rules.Conditions[Type];
            relative.Choices.Types.RemoveAll(type => !allowedTypes.Contains(type));
            //移除所有不在规则里的
        }


        

    }


    public void Unify()
    {
        foreach(Unit<P, T> relative in Relatives)
        {
            if(relative == null)
            {
                continue;
            }
            if(relative.Group != Group) continue;



            //foreach(T type in relative.Choices.Types)
            //{
                Choices.Types.RemoveAll(type => !relative.Choices.Types.Contains(type));
            //}


            //HashSet<T> allowedTypesForThis = Level.Rules.Conditions[relative.Type];
            //Choices.Types.RemoveAll(type => !allowedTypesForThis.Contains(type));
            //移除所有不在规则里的
        }
    }
}

