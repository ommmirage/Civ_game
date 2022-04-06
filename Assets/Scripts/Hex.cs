using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The hex class defines the grid position, world position, size,
// neighbors of a Hex Tile. 

public class Hex
{
    // readonly means that variable is only set in the contructor
    public readonly int q;
    public readonly int r;
    public readonly int s;

    // static means that const belongs to the type, not the object
    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    float radius = 1f;

    public Hex(int q, int r) {
        this.q = q;
        this.r = r;
        s = -(q + r);
    }

    // Returns the world-space position of this hex
    public Vector3 Position()
    {
        float horizontalSpacing = Width();
        float verticalSpacing = Height() * 0.75f;

        return new Vector3(
            horizontalSpacing * (q + r / 2f),
            0,
            verticalSpacing * r
        );
    }

    public float Height()
    {
        return radius * 2;
    }

    public float Width()
    {
        return WIDTH_MULTIPLIER * Height();
    }

    public float VerticalSpacing()
    {
        return Height() * 0.75f;
    }

    public float HorizontalSpacing()
    {
        return Width();
    }

    public Vector3 PositionFromCamera(float numColumns, float numRows)
    {
        Vector3 position = Position();

        float mapWidth = numColumns * HorizontalSpacing();

        float cameraPosX = Camera.main.transform.position.x;

        float mapWidthsFromCenter = (position.x - cameraPosX) / mapWidth;

        // We want widthAmountFromCamera to be in [-0,5; 0,5]
        if (mapWidthsFromCenter > 0.5f)
        {
            mapWidthsFromCenter -= 1f;
            position.x = mapWidthsFromCenter * mapWidth + cameraPosX;
        }
        else if (mapWidthsFromCenter < -0.5f)
        {
            mapWidthsFromCenter += 1f;
            position.x = mapWidthsFromCenter * mapWidth  + cameraPosX;
        }

        return position;
    }
}
