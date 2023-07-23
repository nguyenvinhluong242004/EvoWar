using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sq : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }    
    }
}
