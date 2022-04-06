using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GenerateOcean();

        ElevateArea(33, 3, 4);

        UpdateHexVisuals();
    }

    void ElevateArea(int q, int r, int range)
    {
        Hex centerHex = GetHexAt(q, r);

        // centerHex.Elevation = 0.5f;

        Hex[] areaHexes = GetHexesWithinRangeOf(centerHex, range);

        foreach (Hex hex in areaHexes)
        {
            if (hex != null)
            {
                hex.Elevation = 0.5f;
            }
        }
    }
}
