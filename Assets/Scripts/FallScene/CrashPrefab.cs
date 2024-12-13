using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashPrefab : MonoBehaviour
{
    public GameObject crashEffectPrefab;
    private void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Baby") || collision.gameObject.CompareTag("Floor"))
        {
            //Debug.Log("hit the floor");
            if (crashEffectPrefab != null)
            {
               Instantiate(crashEffectPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
