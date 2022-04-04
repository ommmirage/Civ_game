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

    public Hex(int q, int r) {
        this.q = q;
        this.r = r;
        s = -(q + r);
    }

    // Returns the world-space position of this hex
    public Vector3 Position()
    {
        float radius = 1f;
        float height = radius * 2;
        float width = WIDTH_MULTIPLIER * height;

        float horizontalSpacing = width;
        float verticalSpacing = height * 0.75f;

        return new Vector3(
            horizontalSpacing * (q + r / 2f),
            0,
            verticalSpacing * r
        );
    }
}
