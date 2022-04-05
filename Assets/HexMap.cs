using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] public GameObject hexPrefab;
    [SerializeField] public Material[] hexMaterials;

    [SerializeField] int numColumns = 40;
    [SerializeField] int numRows = 20;

    Hex[,] map;
    public Hex[,] Map { get { return map; } }

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        map = new Hex[numColumns, numRows];

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                map[x, y] = new Hex(x, y);

                Vector3 posFromCamera = map[x, y].PositionFromCamera(numRows, numColumns);

                GameObject hexObject = Instantiate(
                    hexPrefab, 
                    posFromCamera,
                    // map[x,y].Position(), 
                    new Quaternion(),
                    transform
                );

                MeshRenderer meshRenderer = hexObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = hexMaterials[ Random.Range(0, hexMaterials.Length) ];
            }
        }

    }
}
