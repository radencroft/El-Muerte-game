using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [Header("HEALTH")]
    [SerializeField] private int addedHealth;
    [SerializeField] private int maxHP;

    [Header("EXPLOTION")]
    [SerializeField] private GameObject explotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player")
        {
            Instantiate(explotion, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("ammo");


            if ( other.GetComponent<PlayerOne>().health + addedHealth <= maxHP)
            {
                other.GetComponent<PlayerOne>().health += addedHealth;
            }
            else
            {
                other.GetComponent<PlayerOne>().health = maxHP;
            }
            Destroy(this.gameObject);
        }
    }
}
