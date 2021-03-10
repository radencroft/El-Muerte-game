using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [HideInInspector] public Text txt;
    private bool show;

    public float blinkTime;
    private float time;

    [SerializeField] private Text start;
    public bool canBlink;

    void Start()
    {
        txt = GetComponent<Text>(); 
    }
     
    void Update()
    {
        time = blinkTime;

        if (canBlink)
        {
            canBlink = false;
            StartCoroutine(blink());
        }

        if (show)
        {
            txt.enabled = true;
            start.enabled = true;
        }
        else
        {
            txt.enabled = false;
        }
    }

    IEnumerator blink()
    {
        yield return new WaitForSeconds(time);
        show = !show;
        canBlink = true;
    }
}
