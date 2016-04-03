using UnityEngine;
using System.Collections;
using System.Linq;

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
    public bool isFiring;
    public float laserDamage = 13f;
    public float maxLaserSize = 18f;
    public GameObject damageEffect;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    private float effectCooldown;
    private float warmUpTime;
    private float graduallyIncreasingLaserSize;

    public void Start()
    {
        this.graduallyIncreasingLaserSize = 1;
        this.effectCooldown = 0;
        this.warmUpTime = this.inputWarmUpTime;
    }

    public void Update()
    {
        if (this.isFiring)
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
            this.graduallyIncreasingLaserSize = 1;
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
        
        // This check prevents spawning too much of the effect.
        if (this.effectCooldown <= 0)
        {
            this.effectCooldown = this.inputEffectCooldown;
            var newEffect = GameObject.Instantiate(
                this.warmUpEffect, this.laserSpawn.position, this.laserSpawn.rotation) as GameObject;

            if (newEffect != null)
            {
                newEffect.transform.parent = this.transform;
            }
        }

        this.effectCooldown -= Time.deltaTime;
    }

    private void FireTheLaser()
    {
        // Create the laser from the prefabs
        this.InstantiateWeaponPart(ref this.start, ref this.laserStart);
        this.InstantiateWeaponPart(ref this.middle, ref this.laserMiddle);
        this.InstantiateWeaponPart(ref this.end, ref this.laserEnd);

        this.graduallyIncreasingLaserSize++;
        float currentLaserSize = Mathf.Min(this.maxLaserSize, this.graduallyIncreasingLaserSize);

        // Raycast at the right as our sprite has been design for that
        Vector3 laserDirection = this.laserSpawn.forward;
        
        RaycastHit[] hits = Physics.RaycastAll(this.laserSpawn.position, laserDirection);
        RaycastHit hit = this.CheckHits(hits, currentLaserSize);
        
        if (hit.collider != null)
        {
            //Debug.Log("Hitting " + hit.collider.gameObject.name);

            // Get the distance to target
            currentLaserSize = Mathf.Min(
                Vector3.Distance(hit.point, this.laserSpawn.position),
                this.graduallyIncreasingLaserSize);

            // Damage target
            DestroyByHitpoints hitpointsClass = hit.collider.GetComponent<DestroyByHitpoints>();
            if (hitpointsClass != null)
            {
                hitpointsClass.TakeDamage(this.laserDamage);
                this.PlayDamageEffect();
            }
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

    private void PlayDamageEffect()
    {
        // TODO: Implement me.
    }

    private RaycastHit CheckHits(RaycastHit[] hits, float maxDistance)
    {
        RaycastHit resultHit = new RaycastHit();

        if (hits.Length == 0)
        {
            return resultHit;
        }

        resultHit = hits.Where(hit => hit.collider != null)
            .Where(hit => hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Shield"))
            .OrderBy(hit => Vector3.Distance(hit.point, this.laserSpawn.position))
            .FirstOrDefault();

        return resultHit;
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