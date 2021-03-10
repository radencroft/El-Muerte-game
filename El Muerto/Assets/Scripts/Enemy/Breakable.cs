using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject explotion;
    [SerializeField] private GameObject item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Break();
        }
    }

    private void Break()
    {
        Instantiate(explotion, transform.position, transform.rotation);
        if (item != null) { Instantiate(item, transform.position, transform.rotation); }
        Destroy(this.gameObject);
    }
}
