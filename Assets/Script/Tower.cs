using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;
    private EnemyAI targetEnemy;

    
    [HideInInspector]
    public float damage;
    [Header("Damage")]
    public float startDamage = 15f;
    public float upgradeDamage = 5f;

    
    [HideInInspector]
    public float range;
    [Header("Range")]
    public float startRange = 15f;
    public float upgradeRange = 5f;

    [Header("SlowDown")]
    public bool isSlowDown = false;
    public float slowPrecent = .5f;

    [Header("Normal Bullet")]
    public GameObject bulletPrefab;
    public bool isBullet;
    public bool isRocket;
    [HideInInspector]
    public float rate;
    [Header("Rate")]
    public float startRate = 1f;
    public float upgradeRate = 1f;

    private float fireCountDown = 0f;

    [Header("Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    public Light laserLightEffect;

    [Header("Upgrade")]

    public int maxDmgUpgradeTime;
    public int maxRangeUpgradeTime;
    public int maxRateUpgradeTime;

    public int firstGradedTime;
    public int secondGradedTime;
    public int thirdGradedTime;

    public float dmgBuff;
    public float rangeBuff;
    public float rateBuff;

    public MeshRenderer partToChange;
    public Material firstGradedColour;
    public Material secondGradedColour;
    public Material thirdGradedColour;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;
    private bool isLaserAlreadyedPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        range = startRange;
        rate = startRate;
        damage = startDamage;

        dmgBuff = 1;
        rangeBuff = 1;
        rateBuff = 1;

        firstGradedTime = 0;
        secondGradedTime = 0;
        thirdGradedTime = 0;


        firstGradedTime = (maxDmgUpgradeTime + maxRangeUpgradeTime + maxRateUpgradeTime) / 3;
        secondGradedTime = firstGradedTime * 2;
        thirdGradedTime = maxDmgUpgradeTime + maxRangeUpgradeTime + maxRateUpgradeTime;

        if(secondGradedTime == firstGradedTime)
        {
            secondGradedTime++;
        }
        if(thirdGradedTime == secondGradedTime)
        {
            secondGradedTime--;
        }

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= (range * rangeBuff))
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyAI>();
        }
        else
            target = null;

    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser) // close laser
            {
                if (lineRenderer.enabled)
                {
                    laserEffect.Stop();
                    isLaserAlreadyedPlayed = false;
                    lineRenderer.enabled = false;
                    laserLightEffect.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser) // turn on laser
        {
            Laser();
        }
        else // shoot by normal bullet
        {
            if (fireCountDown <= 0f)
            {
                if(isBullet)
                {
                    AudioManager.instance.Play("Bullet");
                }
                if(isRocket)
                {
                    AudioManager.instance.Play("Launch");
                }
                Shoot();
                fireCountDown = 1f / (rate * rateBuff);
            }

            fireCountDown -= Time.deltaTime;
        }

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (!isLaserAlreadyedPlayed)
        {
            AudioManager.instance.Play("Laser");
            isLaserAlreadyedPlayed = true;
        }
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        if(isSlowDown)
        {
            targetEnemy.Slow(slowPrecent);
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserEffect.Play();
            laserLightEffect.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.position = target.position + dir.normalized;

        laserEffect.transform.rotation = Quaternion.LookRotation(dir);


    }
    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGo.transform.parent = gameObject.transform;
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void UpgradeDamage()
    {
        damage += upgradeDamage;
    }

    public void UpgradeRange()
    {
        range += upgradeRange;
    }
    public void UpgradeRate()
    {
        rate += upgradeRate;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
