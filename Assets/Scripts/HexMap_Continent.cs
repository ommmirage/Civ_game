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
    }
}
