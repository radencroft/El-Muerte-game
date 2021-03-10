using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : MonoBehaviour
{
    private Transform player;

    [Header("MOVEMENT")]
    [SerializeField] private float flyDistance;
    [SerializeField] private float speed;

    private Vector3 startPos, endPos;
    public bool facingRight;
    private bool returnToMe, flip;

    [SerializeField] private string myName;

    private void Awake()
    {
        

        if (!facingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Start()
    {
        float distance = facingRight ? flyDistance : (-flyDistance);

        startPos = transform.position;  
        endPos = new Vector3(startPos.x + distance, startPos.y, startPos.z);
    }
     
    void Update()
    {
        TrackPlayer();
        Move(); 
    }

    private void TrackPlayer()
    {
        player = GameObject.FindGameObjectWithTag(myName).gameObject.transform.GetChild(0);
    }

    private void Move()
    {

        if (facingRight)
        {
            if (transform.position.x < endPos.x && !returnToMe)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            }
            else
            {
                returnToMe = true;
                Flip();

                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);

                if (transform.position.x <= startPos.x)
                {
                    {
                        FindObjectOfType<PlayerOne>().isFalcon = false;
                        if (FindObjectOfType<ShadowClone>() != null)
                        {
                            FindObjectOfType<ShadowClone>().isFalcon = false;
                        } 
                    }
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            if (transform.position.x > endPos.x && !returnToMe)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            }
            else
            {
                returnToMe = true;
                Flip();

                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                 

                if (transform.position.x >= startPos.x)
                {
                    {
                        FindObjectOfType<PlayerOne>().isFalcon = false;
                        if (FindObjectOfType<ShadowClone>() != null)
                        {
                            FindObjectOfType<ShadowClone>().isFalcon = false;
                        }
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
    private void Flip()
    {
        if (!flip)
        {
            flip = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
