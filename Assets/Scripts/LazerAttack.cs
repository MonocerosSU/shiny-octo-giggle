using UnityEngine;
using System.Collections;

public class LazerAttack : MonoBehaviour
{
    [Header("Laser pieces")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    public Transform laserSpawn;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    public void Update()
    {
        // Create the laser start from the prefab
        if (this.start == null)
        {
            this.start = GameObject.Instantiate(this.laserStart);
            this.start.transform.parent = this.laserSpawn.transform;
            this.start.transform.localPosition = Vector3.zero;
        }

        // Laser middle
        if (this.middle == null)
        {
            this.middle = GameObject.Instantiate(this.laserMiddle);
            this.middle.transform.parent = this.laserSpawn.transform;
            this.middle.transform.localPosition = Vector3.zero;
        }

        // Define an "infinite" size, not too big but enough to go off screen
        float maxLaserSize = 20f;
        float currentLaserSize = maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector3 laserDirection = this.laserSpawn.transform.forward;
        RaycastHit hit;
        bool isHittingTarget = Physics.Raycast(this.laserSpawn.transform.position, laserDirection, out hit, maxLaserSize);
        bool hitTargetIsNotParent = isHittingTarget && !this.CompareTag(hit.collider.tag);
        if (hitTargetIsNotParent)
        {
            // We touched something!
            Debug.Log("Touching " + hit.collider.gameObject.name);

            // -- Get the laser length
            currentLaserSize = Vector3.Distance(hit.point, this.laserSpawn.transform.position);

            // -- Create the end sprite
            if (this.end == null)
            {
                this.end = GameObject.Instantiate(this.laserEnd);
                this.end.transform.parent = this.laserSpawn.transform;
                this.end.transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            // Nothing hit
            // -- No more end
            if (this.end != null)
            {
                Destroy(this.end);
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
        this.middle.transform.localPosition = new Vector3(0f, 0f, currentLaserSize / 2f);

        // End?
        if (this.end != null)
        {
            this.end.transform.localPosition = new Vector3(0f, 0f, currentLaserSize);
        }
    }
}