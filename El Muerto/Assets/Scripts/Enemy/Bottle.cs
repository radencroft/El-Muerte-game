using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private GameObject explotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Break(); 
        //HIT
        if (other.gameObject.tag == "player")
        {
            other.GetComponent<PlayerOne>().TakeDamage(1);
            Break();
        }
    }

    private void Break()
    {
        Instantiate(explotion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
