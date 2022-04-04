using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public GameObject HexPrefab;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Hex hex = new Hex(x, y);

                Instantiate(
                    HexPrefab, 
                    hex.Position(), 
                    new Quaternion(),
                    transform
                );
            }
        }
    }
}
