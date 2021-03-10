using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<AudioManager>().Play("pick up");
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<Inventory>().PickUpWeapon(itemName);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "shadow")
        {
            other.gameObject.GetComponent<ShadowInventory>().PickUpWeapon(itemName);
            Destroy(this.gameObject);
        }
    }
}
