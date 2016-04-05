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
    public float warmUpTime = 1f;
    public float effectCooldown = 0.33f;
    public GameObject warmUpEffect;
    
    private float currentWarmUpTime;
    private float currentEffectCooldown;

    [Header("Overheat parameters:")]
    public float overheatTime = 2f;
    public float damagingOverheatingTime = 0.5f;
    public float overheatDamageMultiplier = 1.5f;
    public GameObject overheatEffect;

    private float currentOverheatTime;
    private bool isOverheating;
    private bool isDamagingPlayer;

    [Header("Laser parameters:")]
    public bool isFiring;
    public float laserDamage = 13f;
    public float maxLaserSize = 18f;
    public GameObject damageEffect;

    private GameObject start;
    private GameObject middle;
    private GameObject end;

    private float midLaserLength;
    private float initialLaserWidth;
    private float laserWidth;

    public void Start()
    {
        this.initialLaserWidth = this.laserMiddle.transform.localScale.y;
        this.midLaserLength = 1;
        this.currentEffectCooldown = 0;
        this.currentWarmUpTime = this.warmUpTime;

        this.ResetLaserOverheating();
    }

    public void Update()
    {
        if (this.isFiring)
        {
            this.currentWarmUpTime -= Time.deltaTime;
            if (this.currentWarmUpTime <= 0)
            {
                this.StartLaserOverheating();
                this.FireTheLaser();
            }

            if (this.isOverheating)
            {
                this.PlayWarmUpEffect(ref this.overheatEffect);
            }
            else
            {
                this.PlayWarmUpEffect(ref this.warmUpEffect);
            }
        }
        else
        {
            this.ResetLaserOverheating();
            this.midLaserLength = 1;
            this.currentWarmUpTime = this.warmUpTime;
            if (this.end != null || this.middle != null || this.start != null)
            {
                GameObject.Destroy(this.end);
                GameObject.Destroy(this.middle);
                GameObject.Destroy(this.start);
            }
        }
    }

    private void StartLaserOverheating()
    {
        this.currentOverheatTime -= Time.deltaTime;
        if (this.currentOverheatTime <= 0 && !this.isOverheating)
        {
            this.isOverheating = true;
        }

        if (this.isOverheating && this.currentOverheatTime + this.damagingOverheatingTime <= 0 && !this.isDamagingPlayer)
        {
            this.laserWidth *= 0.5f;
            this.isDamagingPlayer = true;
        }

        if (this.isOverheating)
        {
            this.laserWidth = Mathf.Lerp(this.laserWidth, this.initialLaserWidth * 1.5f, 0.1f);
            
            
            Debug.Log(this.warmUpEffect.transform.localScale);
        }
    }

    private void ResetLaserOverheating()
    {
        this.currentOverheatTime = this.overheatTime;
        this.isOverheating = false;
        this.isDamagingPlayer = false;
        this.laserWidth = this.initialLaserWidth;
    }

    private void FireTheLaser()
    {
        // Create the laser from the prefabs
        this.InstantiateWeaponPart(ref this.start, ref this.laserStart);
        this.InstantiateWeaponPart(ref this.middle, ref this.laserMiddle);
        this.InstantiateWeaponPart(ref this.end, ref this.laserEnd);

        this.midLaserLength++;
        float currentLaserSize = Mathf.Min(this.maxLaserSize, this.midLaserLength);

        // Raycast to the right, since the player is shooting to the right.
        Vector3 laserDirection = this.laserSpawn.forward;
        RaycastHit[] hits = Physics.RaycastAll(this.laserSpawn.position, laserDirection);
        RaycastHit hit = this.CheckHits(hits, currentLaserSize);
        
        if (hit.collider != null)
        {
            //Debug.Log("Hitting " + hit.collider.gameObject.name);
            
            currentLaserSize = Mathf.Min(
                Vector3.Distance(hit.point, this.laserSpawn.position),
                this.midLaserLength);

            // Damage target if there is one.
            DestroyByHitpoints enemyHitpoints = hit.collider.GetComponent<DestroyByHitpoints>();
            if (enemyHitpoints != null)
            {
                if (this.isOverheating)
                {
                    enemyHitpoints.TakeDamage(this.laserDamage * this.overheatDamageMultiplier);
                }
                else
                {
                    enemyHitpoints.TakeDamage(this.laserDamage);
                }

                this.PlayDamageEffect(hit.point);
            }
        }
        
        if (this.isDamagingPlayer)
        {
            DestroyByHitpoints playerHitpoints = this.GetComponent<DestroyByHitpoints>();
            if (playerHitpoints != null)
            {
                playerHitpoints.TakeDamage(this.laserDamage);
            }
        }

        // Place things
        // -- Gather some data
        float startSpriteWidth = this.start.GetComponent<Renderer>().bounds.size.x;

        // -- the middle is after start and, as it has a center pivot, have a size of half the laser (minus start and end)
        this.middle.transform.localScale = new Vector3(
            currentLaserSize - startSpriteWidth,
            this.laserWidth,
            this.middle.transform.localScale.z);
        this.middle.transform.localPosition = new Vector3(0f, 0f, currentLaserSize / 2f);

        this.end.transform.localPosition = new Vector3(0f, 0f, currentLaserSize);
        this.end.transform.localScale = new Vector3(
            this.end.transform.localScale.x,
            this.laserWidth,
            this.end.transform.localScale.z);
    }

    private void PlayDamageEffect(Vector3 point)
    {
        if (this.damageEffect == null)
        {
            return;
        }

        GameObject.Instantiate(this.damageEffect, point, new Quaternion());
    }
    
    private void PlayWarmUpEffect(ref GameObject warmUpEffect)
    {
        if (warmUpEffect == null)
        {
            return;
        }

        // This check prevents spawning too much of the effect.
        if (this.currentEffectCooldown <= 0)
        {
            this.currentEffectCooldown = this.effectCooldown;
            var newEffect = GameObject.Instantiate(
                warmUpEffect, this.laserSpawn.position, this.laserSpawn.rotation) as GameObject;

            if (newEffect != null)
            {
                newEffect.transform.parent = this.transform;
            }
        }

        this.currentEffectCooldown -= Time.deltaTime;
    }

    private RaycastHit CheckHits(RaycastHit[] hits, float maxDistance)
    {
        RaycastHit resultHit = new RaycastHit();

        if (hits.Length == 0)
        {
            return resultHit;
        }

        resultHit = hits
            .Where(hit => hit.collider != null)
            .Where(hit => hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Shield"))
            .ToDictionary(hit => Vector3.Distance(hit.point, this.laserSpawn.position), hit => hit)
            .OrderBy(pair => pair.Key)
            .FirstOrDefault(pair => pair.Key <= maxDistance).Value;

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