using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private Camera cam;

    private float halfWidth, halfHeight;
    private Vector3 newPos;

    [SerializeField] private float camSpeed;
    [SerializeField] private float  offset;         // How far away is the trigger for transition

    private bool moveRight, moveLeft;
     

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        cam = GetComponent<Camera>();
    }
    void Start()
    {
        halfHeight = cam.orthographicSize;
        halfWidth = cam.aspect * halfHeight;
        newPos = cam.transform.position;   
    }
     
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (player.transform.position.x > (newPos.x + halfWidth + offset))
        {
            SaveManager.level++;
            player.GetComponent<PlayerOne>().stopMovement = true;
            newPos = new Vector3(transform.position.x + (halfWidth * 2), transform.position.y, transform.position.z);
            moveRight = true;
        }

        if (player.transform.position.x < (newPos.x - halfWidth - offset))
        {
            SaveManager.level--;
            player.GetComponent<PlayerOne>().stopMovement = true;
            newPos = new Vector3(transform.position.x - (halfWidth * 2), transform.position.y, transform.position.z);
            moveLeft = true;
        }

        if (moveRight)
        {
            if (transform.position.x >= newPos.x)
            {
                player.GetComponent<PlayerOne>().stopMovement = false;
                transform.position = newPos; 
                moveRight = false; 
            }

            cam.transform.position = Vector3.MoveTowards(transform.position,newPos, camSpeed * Time.deltaTime);             //Vector3.Lerp(transform.position, newPos, camSpeed * Time.deltaTime); Lerp Idea - vise realisticno/smooth
        }
        
        if (moveLeft)
        {
            if (transform.position.x <= newPos.x)
            {
                player.GetComponent<PlayerOne>().stopMovement = false;
                transform.position = newPos; 
                moveLeft = false; 
            }

            cam.transform.position  = Vector3.MoveTowards(transform.position, newPos, camSpeed * Time.deltaTime);
        }
    }  
}
