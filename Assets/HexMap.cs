using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] public GameObject hexPrefab;
    [SerializeField] public Material[] hexMaterials;

    [SerializeField] int numColumns = 40;
    [SerializeField] int numRows = 20;

    GameObject[,] hexObjects;
    Hex[,] hexes;
    // public GameObject[,] HexObjects { get { return hexObjects; } }

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        hexObjects = new GameObject[numColumns, numRows];
        hexes = new Hex[numColumns, numRows];

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                hexes[x, y] = new Hex(x, y);
                Vector3 position = hexes[x, y].PositionFromCamera(numColumns, numRows);

                GameObject hexObject = Instantiate(
                    hexPrefab, 
                    position,
                    new Quaternion(),
                    transform
                );

                hexObjects[x, y] = hexObject;

                MeshRenderer meshRenderer = hexObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = hexMaterials[ Random.Range(0, hexMaterials.Length) ];
            }
        }
    }

    public void UpdateHexPositions()
    {
        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                Vector3 newPosition = hexes[x, y].PositionFromCamera(numColumns, numRows);
                hexObjects[x, y].transform.position = newPosition;
            }
        }
    }
}
