using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [Header("AMMO")]
    [SerializeField] private int addedBullets;
    [SerializeField] private int maxBullets;

    [Header("EXPLOTION")]
    [SerializeField] private GameObject explotion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && other.GetComponent<Inventory>().weapon_id != 0)
        {
            Instantiate(explotion, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("ammo");

            if (other.GetComponent<Inventory>().bullets + addedBullets <= maxBullets)
            {
                other.GetComponent<Inventory>().AddBullet(addedBullets);
            }
            else
            {
                other.GetComponent<Inventory>().EqualsBullets(maxBullets);
            } 
            Destroy(this.gameObject);
        }
    }
}
