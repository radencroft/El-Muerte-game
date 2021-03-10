using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicFunctions : MonoBehaviour
{ 

    public void StopMove()
    {
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayerOne>().stopMovement = true;
        
        if (GameObject.FindGameObjectWithTag("shadow") != null)
        {
            GameObject.FindGameObjectWithTag("shadow").GetComponent<ShadowClone>().stopMovement = true;
        }
    }

    public void StartMove()
    {
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayerOne>().stopMovement = false;
        
        if (GameObject.FindGameObjectWithTag("shadow") != null)
        {
            GameObject.FindGameObjectWithTag("shadow").GetComponent<ShadowClone>().stopMovement = false;
        }
    }

    public void RemoveMe()
    {
        Destroy(this.gameObject);
    }
}
