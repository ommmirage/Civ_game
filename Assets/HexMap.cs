using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    [SerializeField] public GameObject hexPrefab;
    [SerializeField] public Material[] hexMaterials;

    [SerializeField] int numRows = 20;
    [SerializeField] int numColumns = 40;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        for (int x = 0; x < numColumns; x++)
        {
            for (int y = 0; y < numRows; y++)
            {
                Hex hex = new Hex(x, y);

                GameObject hexObject = Instantiate(
                    hexPrefab, 
                    hex.Position(), 
                    new Quaternion(),
                    transform
                );

                MeshRenderer meshRenderer = hexObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = hexMaterials[ Random.Range(0, hexMaterials.Length) ];
            }
        }

        
    }
}
