using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMe : MonoBehaviour
{
    [Header("DESTROY OBJECT")] 
    [SerializeField] private float suicideTime;
    void Start()
    {
        Destroy(this.gameObject, suicideTime);
    } 


}
