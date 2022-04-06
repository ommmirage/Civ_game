using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] GameObject hexPrefab;

    [SerializeField] int numColumns = 40;
    [SerializeField] int numRows = 20;

    [SerializeField] Material matOcean;
    [SerializeField] Material matPlains;
    [SerializeField] Material matGrasslands;
    [SerializeField] Material matMountains;

    [SerializeField] Mesh meshWater;

    GameObject[,] hexObjects;
    Hex[,] hexes;

    protected void GenerateOcean()
    {
        hexObjects = new GameObject[numColumns, numRows];
        hexes = new Hex[numColumns, numRows];

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                hexes[x, y] = new Hex(x, y);
                Vector3 inworldPos = hexes[x, y].PositionFromCamera(numColumns, numRows);

                GameObject hexObject = Instantiate(
                    hexPrefab, 
                    inworldPos,
                    new Quaternion(),
                    transform
                );

                hexObject.GetComponentInChildren<TextMesh>().text = x + ", " + y;

                hexObjects[x, y] = hexObject;

                MeshRenderer meshRenderer = hexObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = matOcean;

                MeshFilter meshFilter = hexObject.GetComponentInChildren<MeshFilter>();
                meshFilter.mesh = meshWater;
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
