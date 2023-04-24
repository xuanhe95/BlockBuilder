using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group<P, T>
{
    public T Type;
    public int ID;
    public List<Unit<P, T>> Units{get; set;}
    public Possibility<T> LeftChoices{get; set;}
    public Possibility<T> RightChoices{get; set;}
    public Possibility<T> ForwardChoices{get; set;}
    public Possibility<T> BackChoices{get; set;}

    public Possibility<T> Choices{get; set;}
    public Group(int id, T type)

    {
        Type = type;
        ID = id;
        Units = new List<Unit<P, T>>();
    }
    public void AddUnit(Unit<P, T> unit)
    {
        Units.Add(unit);
        unit.Group = this;
    }
    public int Size()
    {
        return Units.Count;
    }
    public T GetObject()
    {
        return Type;
    }

    public void Select(System.Random random){
        foreach(Unit<P, T> unit in Units){
            unit.Select();
        }
        SetType(random);
    }

    public void SetType(System.Random random){


        int i = random.Next(Units[0].Choices.Types.Count);
        

        foreach(Unit<P, T> unit in Units){
            unit.SetType(i);
        }

    }



    public void Cal()
    {
        Unit<P, T> upLeft = Units[Direction.Left];
        Unit<P, T> upRight = Units[Direction.Right];
        Unit<P, T> downLeft = Units[Direction.Forward];
        Unit<P, T> downRight = Units[Direction.Back];

        //
        Unit<P, T> upLeftSide = upLeft.Relatives[Direction.Up];
        Unit<P, T> upRightSide = upRight.Relatives[Direction.Up];

        Unit<P, T> downLeftSide = downLeft.Relatives[Direction.Down];
        Unit<P, T> downRightSide = downRight.Relatives[Direction.Down];

        Unit<P, T> leftUpSide = upLeft.Relatives[Direction.Left];
        Unit<P, T> leftDownSide = downLeft.Relatives[Direction.Left];

        Unit<P, T> rightUpSide = upRight.Relatives[Direction.Right];
        Unit<P, T> rightDownSide = downRight.Relatives[Direction.Right];

        HashSet<T> allowedTypesForUpSide = Level.Rules.UpConditions[upLeftSide.Type];
        HashSet<T> allowedTypesForRightSide = Level.Rules.RightConditions[rightUpSide.Type];
        HashSet<T> allowedTypesForLeftSide = Level.Rules.LeftConditions[leftUpSide.Type];
        HashSet<T> allowedTypesForDownSide = Level.Rules.DownConditions[downRightSide.Type];
        




        Choices.Types.RemoveAll(type => !allowedTypesForThis.Contains(type));
        //移除所有不在规则里的
        }



    }
}
