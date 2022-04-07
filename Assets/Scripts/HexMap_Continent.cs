using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    [SerializeField] int minContinents = 1;
    [SerializeField] int maxContinents = 2;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        GenerateOcean();
        GenerateContinents();

        UpdateHexVisuals();
    }

    void GenerateContinents()
    {
        int numContinents = Random.Range(minContinents, maxContinents);

        for (int i = 0; i < numContinents; i++)
        {
            GenerateContinent(i);
        }
    }

    void GenerateContinent(int continentNumber)
    {
        CentralHex continentCenter = GenerateContinentCenterHex(continentNumber);

        int numSplats = Random.Range(3, 6);

        while (numSplats > 0)
        {
            if (GenerateTerrainAroundContinentCenter(continentCenter, continentNumber))
            {
                numSplats--;
            }
        }
    }

    CentralHex GenerateContinentCenterHex(int continentNumber)
    {
        Hex hex;

        do
        {
            hex = GetHexAt(continentNumber * 20, 10);
        }
        while (hex == null);

        CentralHex centralHex = new CentralHex(hex, 7);
        centralHex.ContinentNumber = continentNumber;
        ElevateArea(centralHex, continentNumber);

        return centralHex;
    }

    bool GenerateTerrainAroundContinentCenter(CentralHex continentCenter, int continentNumber)
    {
        int q = Random.Range(
            continentCenter.Q - continentCenter.Range, 
            continentCenter.Q + continentCenter.Range
            );
        int r = Random.Range(
            continentCenter.R - continentCenter.Range, 
            continentCenter.R + continentCenter.Range
            );

        Hex augmentationCenterHex = GetHexAt(q, r);

        if ((augmentationCenterHex == null) || (augmentationCenterHex.Elevation > 0))
        {
            return false;
        }

        int range = Random.Range(3, 7);

        ElevateArea(augmentationCenterHex, range, continentNumber);
        return true;
    }

    void ElevateArea(CentralHex centralHex, int continentNumber)
    {
        ElevateArea(centralHex, centralHex.Range, continentNumber);
    }

    void ElevateArea(
        Hex elevationCenterHex, 
        int range, 
        int continentNumber, 
        float centerHeight = 1.5f)
    {
        Hex[] areaHexes = GetHexesWithinRangeOf(elevationCenterHex, range);

        foreach (Hex hex in areaHexes)
        {
            if (hex != null)
            {
                // if another continent
                if ((hex.ContinentNumber != continentNumber) && (hex.ContinentNumber != -1))
                {
                    hex.Elevation = -0.5f;
                    hex.ContinentNumber = -1;
                }
                else
                {
                    hex.Elevation += Mathf.Lerp(
                        centerHeight, 
                        0f, 
                        HexMath.Distance(elevationCenterHex, hex) / range
                        );
                    hex.ContinentNumber = continentNumber;
                }
                
            }
        }
    }
}
