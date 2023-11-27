using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject Dagger;
    public GameObject Score;
    public GameObject Life;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = gameObject.transform.position;
        if (collision.CompareTag("Whip"))
        {
            int i = Random.Range(1, 4);
            switch (i) {
                case 1:
                    Instantiate(Dagger, pos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Score, pos, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Life, pos, Quaternion.identity);
                    break;
            }
            Destroy(gameObject);
        }
    }
}

