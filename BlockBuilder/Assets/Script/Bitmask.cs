using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Bitmask
{
    public static int GetBitMask(GridElement[] nearGE)
    {
        int bitmask = 0;

        for (int i = 0; i < 8; i++)
        {
            if (nearGE[i] != null)
            {
                if (nearGE[i].getEnabled())
                {
                    bitmask +=  MathF.Pow(2,i).ConvertTo<int>();
                }
            }
        }

        return bitmask;

    }
}
