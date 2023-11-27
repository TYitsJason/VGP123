using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class EnemyTurret : Enemy
{
    public float projectileFireRate;
    float timeSinceLastFire = 0;
    public bool canFire = false;
    public ActivationRange ar;
    public Transform tr;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (projectileFireRate <= 0) projectileFireRate = 1;
        tr = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log("Enemy Turret took " + damage.ToString() + " damage.");

        if (_health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name != "Fire" && canFire)
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }
        if (canFire)
        {
            if (tr.position.x <= gameObject.transform.position.x) sr.flipX = false; 
            else sr.flipX = true;
        }
    }
}
