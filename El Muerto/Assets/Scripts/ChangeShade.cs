using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShade : MonoBehaviour
{ 
    private SpriteRenderer sr;  
    private Color color;
    private float alpha;
    private float alphaStartValue;                  //univerzalnost - da bi radila ma koja god bila pocetna alpha vrednost

    private int i;                                  //counter - prolazak kroz sva vremena
    private bool resume;
    private bool decrease;


    [Header("\"Ja cu load-ovati sledecu scenu, no worries bro\"")]
    [SerializeField] private bool waitAndLoad = false;

    [ConditionalHide("waitAndLoad", true)] 
    [SerializeField] private float seconds;            //cekanje pre load scene 

    [Header("Time Between Transitions #APPEAR")]
    [SerializeField] private float[] appearTime;

    [Header("Time Between Transitions #WAIT")]
    [SerializeField] private float transitionTime;

    [Header("Time Between Transitions #DISAPPEAR")]
    [SerializeField] private float[] disappearTime;




    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        color = sr.color;
        alpha = sr.color.a;
        alphaStartValue = 1f;

        i = 0;
        resume = true;
        decrease = false;
    }

    private void Update()
    {
        if (!decrease)
        {
            if (alpha < 1f && resume)
            {
                if (i < appearTime.Length - 1) { ++i; }
                resume = false;
                StartCoroutine(increaseShade(1f / appearTime.Length, appearTime[i]));
            }

            if(alpha >= 1f) 
            {
                StartCoroutine("wait");
            }
        }
        else
        {
            if (alpha > 0 && resume)
            {
                if (i < disappearTime.Length - 1) { ++i; } 
                resume = false;
                StartCoroutine(decreaseShade(alphaStartValue / disappearTime.Length, disappearTime[i]));
            }
        }


        if (alpha <= 0 && decrease && waitAndLoad)
        {
            SceneController sceneManager = FindObjectOfType<SceneController>();
            StartCoroutine(loadScene());
        }
    }

    private IEnumerator decreaseShade(float percent, float time)
    {
        yield return new WaitForSeconds(time);
        alpha -= percent; 
        sr.color = new Color(color.r, color.g, color.b, alpha);
        resume = true;
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(transitionTime);
        decrease = true;
    }

    private IEnumerator increaseShade(float percent, float time)
    {
        yield return new WaitForSeconds(time);
        alpha += percent;
        sr.color = new Color(color.r, color.g, color.b, alpha);
        resume = true;
    }



    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(seconds);
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.ChangeScene();
    }
}
