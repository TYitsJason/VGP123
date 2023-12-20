using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRange : MonoBehaviour
{
    public EnemyTurret et;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            et.canFire = true;
            Destroy(gameObject);
        }
    }
}
