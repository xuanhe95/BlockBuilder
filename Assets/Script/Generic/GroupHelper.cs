using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Group<P, T>
{
    public Level<P, T> GetLevel()
    {
        return Units[0].Level;
    }

    public Group<P, T> GetAdjacentGroup(int direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return GetLeft();
            case Direction.Right:
                return GetRight();
            case Direction.Forward:
                return GetForward();
            case Direction.Back:
                return GetBack();
            case Direction.Up:
                return GetUp();
            case Direction.Down:
                return GetDown();
            default:
                return null;
        }
    }

    public Group<P, T> GetLeft()
    {
        //Debug.Log("get Left");
        Unit<P, T> unit = Units[0].Relatives[Direction.Left];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetRight()
    {
        //Debug.Log("get Right");
        Unit<P, T> unit = Units[3].Relatives[Direction.Right];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetForward()
    {
        //Debug.Log("get Forward");
        Unit<P, T> unit = Units[3].Relatives[Direction.Forward];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetBack()
    {
        //Debug.Log("get Back");
        Unit<P, T> unit = Units[0].Relatives[Direction.Back];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetUp()
    {
        //Debug.Log("get Up");
        if (Units[0].Level.Up == null)
            return null;
        Unit<P, T> unit = Units[0].Level.Up.Units[Units[0].ID];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> GetDown()
    {
        //Debug.Log("get Down");
        if (Units[0].Level.Down == null)
            return null;
        Unit<P, T> unit = Units[0].Level.Down.Units[Units[0].ID];
        if (unit == null)
            return null;
        return unit.Group;
    }

    public Group<P, T> FindRelativeGroup(int direction)
    {
        switch (direction)
        {
            case Direction.Forward:
                return GetForward();
            case Direction.Back:
                return GetBack();
            case Direction.Left:
                return GetLeft();
            case Direction.Right:
                return GetRight();
            case Direction.Up:
                return GetUp();
            case Direction.Down:
                return GetDown();
            default:
                return null;
        }
    }
}
