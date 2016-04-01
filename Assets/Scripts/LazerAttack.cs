using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    public void Update()
    {
        // Create the laser start from the prefab
        if (this.start == null)
        {
            this.start = Instantiate(this.laserStart);
            this.start.transform.parent = this.transform;
            this.start.transform.localPosition = Vector2.zero;
        }

        // Laser middle
        if (this.middle == null)
        {
            this.middle = Instantiate(this.laserMiddle);
            this.middle.transform.parent = this.transform;
            this.middle.transform.localPosition = Vector2.zero;
        }

        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 20f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector2 laserDirection = this.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize);

        if (hit.collider != null)
        {
            // We touched something!

            // -- Get the laser length
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            // -- Create the end sprite
            if (this.end == null)
            {
                this.end = Instantiate(laserEnd) as GameObject;
                this.end.transform.parent = this.transform;
                this.end.transform.localPosition = Vector2.zero;
            }
        }
        else
        {
            // Nothing hit
            // -- No more end
            if (this.end != null)
            {
                GameObject.Destroy(this.end);
            }
        }

        // Place things
        // -- Gather some data
        float startSpriteWidth = this.start.GetComponent<Renderer>().bounds.size.x;
        //float endSpriteWidth = 0f;
        //if (this.end != null)
        //{
        //    endSpriteWidth = this.end.GetComponent<Renderer>().bounds.size.x;
        //}

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        this.middle.transform.localScale = new Vector3(currentLaserSize - startSpriteWidth, this.middle.transform.localScale.y, this.middle.transform.localScale.z);
        this.middle.transform.localPosition = new Vector2(currentLaserSize / 2f, 0f);

        // End?
        if (this.end != null)
        {
            this.end.transform.localPosition = new Vector2(currentLaserSize, 0f);
        }
    }
}