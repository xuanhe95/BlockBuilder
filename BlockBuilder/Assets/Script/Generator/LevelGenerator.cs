using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Generator : MonoBehaviour
{
    private List<Level<GameObject, GameObject>> levels = new List<Level<GameObject, GameObject>>();

    void LevelBuilder(int width, int length, int height)
    {
        //Choice<GameObject> choices = ChoiceGenerator();
        for (int i = 0; i < height; i++)
        {
            if (i == 0) // base level
            {
                levels.Add(
                    LevelGenerator(
                        i,
                        width,
                        length,
                        2,
                        GeoMap[(int)Geo.Water],
                        ChoiceGenerator(),
                        baseRule
                    )
                );
            }
            else // rest levels
            {
                Level<GameObject, GameObject> downLevel = levels[levels.Count - 1];
                Level<GameObject, GameObject> upLevel = LevelGenerator(
                    i,
                    width,
                    length,
                    2,
                    GeoMap[(int)Geo.Empty],
                    ChoiceGenerator(),
                    midRule
                );
                downLevel.Up = upLevel;
                upLevel.Down = downLevel;

                for (int id = 0; id < upLevel.Units.Count; id++)
                {
                    Unit<GameObject, GameObject> upUnit = upLevel.Units[id];
                    Unit<GameObject, GameObject> downUnit = downLevel.Units[id];
                    upUnit.Relatives[Direction.Down] = downUnit;
                    downUnit.Relatives[Direction.Up] = upUnit;
                }
                levels.Add(upLevel);
            }
        }
    }

    Level<GameObject, GameObject> LevelGenerator(
        int levelID,
        int width,
        int length,
        double height,
        Type<GameObject> initGo,
        Choice<GameObject> choices,
        Rule<GameObject> rule
    )
    {
        int id = 0;
        Level<GameObject, GameObject> level = new Level<GameObject, GameObject>(
            levelID,
            width,
            length,
            height
        );
        //levels.Add(level);
        level.SetRule(rule);
        //GameObject go = levelID == 0 ? Water : Empty;

        for (int i = 0; i < width; i++)
        {
            Unit<GameObject, GameObject> backUnit = null;
            for (int j = 0; j < length; j++)
            {
                GameObject location = new GameObject();
                //location.transform.rotation = Quaternion.Euler(0f,90f,0f);
                location.transform.position = new Vector3(i, levelID, j);
                Unit<GameObject, GameObject> unit = new Unit<GameObject, GameObject>(
                    id,
                    location,
                    ChoiceGenerator(),
                    level
                );
                level.AddUnit(unit);

                if (backUnit != null)
                    backUnit.Relatives[Direction.Forward] = unit;
                unit.Relatives[Direction.Back] = backUnit;
                backUnit = unit;

                id++;
            }
        }

        for (int i = 0; i < length; i++)
        {
            Unit<GameObject, GameObject> leftUnit = level.Units[i];
            for (int j = i + length; j < width * length; j += length)
            {
                Unit<GameObject, GameObject> rightUnit = level.Units[j];
                leftUnit.Relatives[Direction.Right] = rightUnit;
                rightUnit.Relatives[Direction.Left] = leftUnit;
                leftUnit = rightUnit;
            }
        }

        for (int i = 0; i < width; i += 2)
        {
            for (int j = 0; j < length; j += 2)
            {
                Group<GameObject, GameObject> group = new Group<GameObject, GameObject>(
                    i * length / 2 + j / 2,
                    initGo,
                    ChoiceGenerator()
                );
                group.AddUnit(level.Units[i * length + j]);
                group.AddUnit(level.Units[i * length + j + 1]);
                group.AddUnit(level.Units[(i + 1) * length + j]);
                group.AddUnit(level.Units[(i + 1) * length + j + 1]);

                SetRotation(group.GetUnit(0), 0f, 0f, 0f);
                SetRotation(group.GetUnit(1), 0f, 90f, 0f);
                SetRotation(group.GetUnit(2), 0f, 180f, 0f);
                SetRotation(group.GetUnit(3), 0f, 270f, 0f);

                level.Groups.Add(i * length / 2 + j / 2, group);

                group.SetTypes();
            }
        }

        return level;
    }
}
