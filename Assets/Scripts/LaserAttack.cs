using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour
{
    [Header("Laser pieces:")]
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    [Header("Laser spawn transform:")]
    public Transform laserSpawn;

    [Header("Warm up parameters:")]
    public float inputWarmUpTime = 1f;
    public float inputEffectCooldown = 0.33f;
    public GameObject warmUpEffect;

    [Header("Other parameters:")]
    public float maxLaserSize = 18f;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    private float effectCooldown;
    private float warmUpTime;

    public void Start()
    {
        this.effectCooldown = 0;
        this.warmUpTime = this.inputWarmUpTime;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            this.warmUpTime -= Time.deltaTime;
            if (this.warmUpTime <= 0)
            {
                this.FireTheLaser();
            }

            this.PlayWarmUpEffect();
        }
        else
        {
            this.warmUpTime = this.inputWarmUpTime;
            if (this.end != null || this.middle != null || this.start != null)
            {
                GameObject.Destroy(this.end);
                GameObject.Destroy(this.middle);
                GameObject.Destroy(this.start);
            }
        }
    }

    private void PlayWarmUpEffect()
    {
        if (this.warmUpEffect == null)
        {
            return;
        }

        if (this.effectCooldown <= 0)
        {
            this.effectCooldown = this.inputEffectCooldown;
            var newEffect = GameObject.Instantiate(
                this.warmUpEffect, this.laserSpawn.position, this.laserSpawn.rotation) as GameObject;
            newEffect.transform.parent = this.transform;
        }

        this.effectCooldown -= Time.deltaTime;
    }

    private void FireTheLaser()
    {
        // Create the laser from the prefabs
        this.InstantiateWeaponPart(ref this.start, ref this.laserStart);
        this.InstantiateWeaponPart(ref this.middle, ref this.laserMiddle);
        this.InstantiateWeaponPart(ref this.end, ref this.laserEnd);

        float currentLaserSize = this.maxLaserSize;

        // Raycast at the right as our sprite has been design for that
        Vector3 laserDirection = this.laserSpawn.forward;
        RaycastHit hit;
        bool isHittingTarget = Physics.Raycast(this.laserSpawn.position, laserDirection, out hit, this.maxLaserSize);
        bool hitTargetIsNotParent = isHittingTarget && !this.CompareTag(hit.collider.tag);
        if (hitTargetIsNotParent)
        {
            //Debug.Log("Touching " + hit.collider.gameObject.name);

            // -- Get the laser length
            currentLaserSize = Vector3.Distance(hit.point, this.laserSpawn.position);
        }

        // Place things
        // -- Gather some data
        float startSpriteWidth = this.start.GetComponent<Renderer>().bounds.size.x;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        this.middle.transform.localScale = new Vector3(
            currentLaserSize - startSpriteWidth,
            this.middle.transform.localScale.y,
            this.middle.transform.localScale.z);
        this.middle.transform.localPosition = new Vector3(0f, 0f, currentLaserSize / 2f);
        
        if (this.end != null)
        {
            this.end.transform.localPosition = new Vector3(0f, 0f, currentLaserSize);
        }
    }

    private void InstantiateWeaponPart(ref GameObject destination, ref GameObject source)
    {
        if (destination == null)
        {
            destination = GameObject.Instantiate(source);
            destination.transform.parent = this.laserSpawn;
            destination.transform.localPosition = Vector3.zero;
        }
    }
}