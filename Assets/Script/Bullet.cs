using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [HideInInspector]
    public int damage;
    public int startDamage = 50;
    public int maxDamage = 100;
    public float speed = 70f;
    public float explosionRange = 0f;
    public GameObject hitEffect;
    public GameObject disappearEffect;    
    public void Seek(Transform _target)
    {
        target = _target;
        damage = startDamage;
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

        if(explosionRange >0f)
        {
            Explode();
        }
        else

        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        EnemyAI e = enemy.GetComponent<EnemyAI>();
        if (e!= null)
        {
            e.TakeDamage(startDamage);
        }
    }

    void BulletHitEffect()
    {
        GameObject effectIns = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
    }

    void BulletDisappearEffect()
    {
        GameObject effectIns = (GameObject)Instantiate(disappearEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
    }
}
