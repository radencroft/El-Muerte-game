using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArea : MonoBehaviour
{
    [HideInInspector] public bool canSpawn;
    [HideInInspector] public bool isSpawn; 
    [SerializeField] private float respawnTime; 


    [Header("DETECT PLAYER")]
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("SHADOW CLONE")]
    [SerializeField] private GameObject shadowClone;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject deSpawnClone;

    private GameObject player; 


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }
     
    private void Update()
    {
        Spawn();
        SwitchPlaces();
    }

    private void Spawn()
    {
        canSpawn = Physics2D.OverlapBox(transform.position, checkSize, 0f, whatIsPlayer);

        if (canSpawn && !isSpawn && Input.GetButtonDown("Ability") && !Input.GetButton("Up"))
        { 
            isSpawn = true;
            Instantiate(shadowClone, spawnPoint.position, Quaternion.identity);
            return;
        }
        
        if (isSpawn && Input.GetButtonDown("Ability") && !Input.GetButton("Up"))
        {
            isSpawn = false;
            if (GameObject.FindGameObjectWithTag("shadow") != null)
            {
                Instantiate(deSpawnClone, GameObject.FindGameObjectWithTag("shadow").transform.position, Quaternion.identity);
            }
            Destroy(GameObject.FindGameObjectWithTag("shadow")); 
        }
    }



    private void SwitchPlaces()
    {
        if (canSpawn && isSpawn && Input.GetButtonDown("Ability") && Input.GetButton("Up"))
        { 
            Transform clone = GameObject.FindGameObjectWithTag("shadow").transform;

            player.transform.position = clone.transform.position; 
             
            if (GameObject.FindGameObjectWithTag("shadow") != null)
            {
                Instantiate(deSpawnClone, GameObject.FindGameObjectWithTag("shadow").transform.position, Quaternion.identity);
            }
            Destroy(GameObject.FindGameObjectWithTag("shadow"));
              
        }
    }

}
