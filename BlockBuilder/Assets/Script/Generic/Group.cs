using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group<P, T>
{
    public T Type;
    public int ID;
    public List<Unit<P, T>> Units { get; set; }
    //public Possibility<T> LeftChoices { get; set; }
    //public Possibility<T> RightChoices { get; set; }
    //public Possibility<T> ForwardChoices { get; set; }
    //public Possibility<T> BackChoices { get; set; }

    public Possibility<T> Choices { get; set; }

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

    public Group<P, T> getLeft()
    {
        Debug.Log("get left");
        return Units[0].Relatives[Direction.Left].Group;
    }
    public Group<P, T> getRight()
    {
        Debug.Log("get Right");
        return Units[3].Relatives[Direction.Right].Group;
    }
    public Group<P, T> getForward()
    {
        return Units[3].Relatives[Direction.Forward].Group;
    }
    public Group<P, T> getBack(){
        return Units[0].Relatives[Direction.Back].Group;
    }


    public int Size()
    {
        return Units.Count;
    }

    public T GetObject()
    {
        return Type;
    }

    public void Select(System.Random random)
    {
        foreach (Unit<P, T> unit in Units)
        {
            unit.Select();
        }

        SetType(random);
    }

    public void SetType(System.Random random)
    {


        int i = random.Next(Units[0].Choices.Types.Count);


        foreach (Unit<P, T> unit in Units)
        {
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

        //HashSet<T> allowedTypesForUpSide = Level.Rules.UpConditions[upLeftSide.Type];
        //HashSet<T> allowedTypesForRightSide = Level.Rules.RightConditions[rightUpSide.Type];
        //HashSet<T> allowedTypesForLeftSide = Level.Rules.LeftConditions[leftUpSide.Type];
        //HashSet<T> allowedTypesForDownSide = Level.Rules.DownConditions[downRightSide.Type];









        //Choices.Types.RemoveAll(type => !allowedTypesForThis.Contains(type));
        //移除所有不在规则里的




    }


    public int checkBackToBack(T type)
    {
        if (checkSurround(type) != -1 || checkFullSurround(type) != -1) return -2;
        if (getRight().Type.Equals(getLeft().Type) && getLeft().Type.Equals(type))
        {
            return Direction.Left;
        }
        else if (getForward().Type.Equals(getBack().Type) && getBack().Type.Equals(type))
        {
            return Direction.Forward;
        }

        return -1;
    }

    public int checkAdjacent(T type)
    {
        if (checkSurround(type) != -1 || checkFullSurround(type) != -1) return -2;

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


        if (upLeftSide == leftUpSide && upLeftSide.Type.Equals(type))
        {
            return Direction.Left;
        }
        else if (upLeftSide == rightUpSide && upLeftSide.Type.Equals(type))
        {
            return Direction.Up;
        }
        else if (rightUpSide == downLeftSide && rightUpSide.Type.Equals(type))
        {
            return Direction.Right;
        }
        else if (downLeftSide == leftUpSide && downLeftSide.Type.Equals(type))
        {
            return Direction.Down;
        }
        else return -1;
    }

    public int checkSurround(T type)
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

        if (upLeftSide == rightUpSide && rightUpSide == downLeftSide && downLeftSide.Type.Equals(type))
        {
            return Direction.Up;
        }
        else if (rightUpSide == downLeftSide && downLeftSide == leftDownSide && leftDownSide.Type.Equals(type))
        {
            return Direction.Right;
        }
        else if (downLeftSide == leftDownSide && leftDownSide == upLeftSide && upLeftSide.Type.Equals(type))
        {
            return Direction.Down;
        }
        else if (leftDownSide == upLeftSide && upLeftSide == rightUpSide && rightUpSide.Type.Equals(type))
        {
            return Direction.Left;
        }

        return -1;
    }

    public int checkFullSurround(T type)
    {
        Unit<P, T> upLeft = Units[1];
        Unit<P, T> upRight = Units[3];
        Unit<P, T> downLeft = Units[0];
        Unit<P, T> downRight = Units[2];

        //
        Unit<P, T> upLeftSide = upLeft.Relatives[Direction.Up];
        Unit<P, T> upRightSide = upRight.Relatives[Direction.Up];

        Unit<P, T> downLeftSide = downLeft.Relatives[Direction.Down];
        Unit<P, T> downRightSide = downRight.Relatives[Direction.Down];

        Unit<P, T> leftUpSide = upLeft.Relatives[Direction.Left];
        Unit<P, T> leftDownSide = downLeft.Relatives[Direction.Left];

        Unit<P, T> rightUpSide = upRight.Relatives[Direction.Right];
        Unit<P, T> rightDownSide = downRight.Relatives[Direction.Right];
        if (upLeftSide.Type.Equals(type) &&
            rightUpSide.Type.Equals(type) &&
            downLeftSide.Type.Equals(type) &&
            leftUpSide.Type.Equals(type))
        {
            return Direction.Left;
        }

        return -1;
    }



    public Group<P, T> FindRelativeGroup(Group<P, T> group, int direction)

    {

        switch (direction)
        {
            case Direction.Forward :
                return getForward();
            case Direction.Back :
                return getBack();
            case Direction.Left :
                return getLeft();
            case Direction.Right :
                return getRight();
            default:
                Debug.Log("NULL");
                return null;
        }
    }
}