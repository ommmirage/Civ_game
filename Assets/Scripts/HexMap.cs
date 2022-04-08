using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] GameObject hexPrefab;

    [SerializeField] int numColumns = 40;
    public int NumColumns { get { return numColumns; } }

    [SerializeField] int numRows = 20;
    public int NumRows { get { return numRows; } }

    [SerializeField] Material matOcean;
    [SerializeField] Material matPlains;
    [SerializeField] Material matGrasslands;
    [SerializeField] Material matMountains;

    [SerializeField] Mesh meshWater;

    GameObject[,] hexObjects;

    Hex[,] hexes;

    float MountainHeight = 1.05f;
    float HillHeight = 0.65f;

    public Hex GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array is not yet instantiated.");
            return null;
        }

        if ((y < 0) || (y >= numRows))
            return null;

        x = x % numColumns;

        if (x < 0)
            x += numColumns;
        
        return hexes[x, y];
    }

    protected void GenerateOcean()
    {
        hexObjects = new GameObject[numColumns, numRows];
        hexes = new Hex[numColumns, numRows];

        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                Hex hex = new Hex(x, y);

                hexes[x, y] = hex;

                Vector3 inworldPos = hex.PositionFromCamera(numColumns, numRows);

                GameObject hexObject = Instantiate(
                    hexPrefab, 
                    inworldPos,
                    new Quaternion(),
                    transform
                );

                hexObject.GetComponentInChildren<TextMesh>().text = x + ", " + y;

                hexObjects[x, y] = hexObject;
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

    public void UpdateHexVisuals()
    {
        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                GameObject hexObject = hexObjects[x, y];
                Hex hex = hexes[x, y];

                MeshRenderer meshRenderer = hexObject.GetComponentInChildren<MeshRenderer>();

                if (hex.Elevation > MountainHeight)
                {
                    meshRenderer.material = matMountains;
                }
                else if (hex.Elevation > HillHeight)
                {
                    meshRenderer.material = matPlains;
                }
                else if (hex.Elevation > 0)
                {
                    meshRenderer.material = matGrasslands;
                }
                else
                {
                    meshRenderer.material = matOcean;
                }

                // MeshFilter meshFilter = hexObject.GetComponentInChildren<MeshFilter>();
                // meshFilter.mesh = meshWater;
            }
        }
    }

    public Hex[] GetHexesWithinRangeOf(Hex centralHex, int range)
    {
        List<Hex> results = new List<Hex>();

        for (int dx = -range; dx < range-1; dx++)
        {
            for(int dy = Mathf.Max(-range+1, -dx-range); dy < Mathf.Min(range, -dx+range-1); dy++)
            {
                results.Add(GetHexAt(centralHex.Q + dx, centralHex.R + dy));
            }
        }

        return results.ToArray();
    }
}
