using UnityEngine;

/// <summary>
/// Holding the limits for use when
/// limiting movement to the screen.
/// </summary>
[System.Serializable]
public class Boundary
{
    public float xMin;

    public float xMax;

    public float yMin;

    public float yMax;

    public Boundary(Transform sceneBoundary, Bounds playerBounds)
    {
        this.xMin = (sceneBoundary.localScale.x / 2 * -1) + playerBounds.size.x;
        this.xMax = (sceneBoundary.localScale.x / 2) - playerBounds.size.x;
        this.yMin = (sceneBoundary.localScale.y / 2 * -1) + playerBounds.size.y;
        this.yMax = (sceneBoundary.localScale.y / 2) - playerBounds.size.y;
    }
}
