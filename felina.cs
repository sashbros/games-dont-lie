using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class felina : MonoBehaviour
{
    public GameObject felinaEffect;
    private int count = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count==0)
            {
                Instantiate(felinaEffect, collision.gameObject.transform.position, Quaternion.identity);
                count++;
            }
        }
    }
}
