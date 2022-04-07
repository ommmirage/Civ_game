using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMath
{
    public static float Distance(Hex a, Hex b)
    {
        int rDistance = a.R - b.R;
        int qDistance = a.Q - b.Q;

        return Mathf.Sqrt(rDistance * rDistance + qDistance * qDistance);
    }
}
