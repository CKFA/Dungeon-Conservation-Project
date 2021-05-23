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

    public int maxDmgUpgradeTime = 5;
    public int maxRangeUpgradeTime = 5;
    public int maxRateUpgradeTime = 5;
    private int upgradeTime = 0;

    [HideInInspector]
    public int firstGradedTime = 0;
    [HideInInspector]
    public int secondGradedTime = 0;
    [HideInInspector]
    public int thirdGradedTime = 0;

    public MeshRenderer partToChange;
    public Material firstGradedColour;
    public Material secondGradedColour;
    public Material thirdGradedColour;

    private bool isFirstGraded = false;
    private bool isSecondGraded = false;
    private bool isThirdGraded = false;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        range = startRange;
        rate = startRate;
        damage = startDamage;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        firstGradedTime = 0;
        secondGradedTime = 0;
        thirdGradedTime = 0;

        firstGradedTime = Mathf.RoundToInt(maxDmgUpgradeTime + maxRangeUpgradeTime + maxRateUpgradeTime / 3);
        secondGradedTime = Mathf.RoundToInt(maxDmgUpgradeTime + maxRangeUpgradeTime + maxRateUpgradeTime / 2);
        thirdGradedTime = maxDmgUpgradeTime + maxRangeUpgradeTime + maxRateUpgradeTime;

        if(secondGradedTime == firstGradedTime)
        {
            secondGradedTime++;
        }
        if(thirdGradedTime == secondGradedTime)
        {
            secondGradedTime--;
        }
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

        if (nearestEnemy != null && shortestDistance <= startRange)
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
                Shoot();
                fireCountDown = 1f / rate;
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
        upgradeTime++;
        damage += upgradeDamage;
    }

    public void UpgradeRange()
    {
        upgradeTime++;
        range += upgradeRange;
    }
    public void UpgradeRate()
    {
        upgradeTime++;
        rate += upgradeRate;
    }

    public void ChangeColorChecker() // for upgrading
    {
        if ((upgradeTime > firstGradedTime) && (!isFirstGraded))
        {
            isFirstGraded = true;
            partToChange.material = firstGradedColour;
            return;
        }
        else if ((upgradeTime > firstGradedTime) && (!isSecondGraded))
        {
            isFirstGraded = true;
            partToChange.material = secondGradedColour;
            return;
        }
        else if ((upgradeTime > firstGradedTime) && (!isThirdGraded))
        {
            isFirstGraded = true;
            partToChange.material = thirdGradedColour;
        }
    }


    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, startRange);
    //}
}
