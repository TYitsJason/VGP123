using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickupType
    {
        Dagger = 0,
        Life = 1,
        Score = 2
    }

    public PickupType currentPickup;
    BoxCollider2D bc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            switch (currentPickup)
            {
                case PickupType.Life:
                    pc.lives++;
                    break;
                case PickupType.Dagger:
                    pc.setSubweapon(PlayerController.subweapon.Dagger);
                    break;
                case PickupType.Score:
                    break;

            }
            Destroy(gameObject);
        }
        
    }
}
