using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [HideInInspector]
    public float damage;
    public float speed = 70f;
    public float explosionRange = 0f;
    public GameObject hitEffect;
    public GameObject disappearEffect;    
    public void Seek(Transform _target)
    {
        target = _target;
        damage = GetComponentInParent<Tower>().damage;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            BulletDisappearEffect();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        BulletHitEffect();

        if(explosionRange > 0f)
        {
            Explode();
        }
        else

        {
            Damage(target);
        }
    }

    void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Tower t = GetComponentInParent<Tower>();
                if (t.isSlowDown)
                {
                    collider.GetComponent<EnemyAI>().Slow(t.slowPrecent);
                }
                Damage(collider.transform);
            }
        }
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyAI e = enemy.GetComponent<EnemyAI>();
        Tower t = GetComponentInParent<Tower>();
        if (e!= null)
        {
            if (t.isSlowDown)
            {
                e.Slow(t.slowPrecent);
            }
            e.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    void BulletHitEffect()
    {
        GameObject effectIns = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);
    }

    void BulletDisappearEffect()
    {
        GameObject effectIns = (GameObject)Instantiate(disappearEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);
    }
}
