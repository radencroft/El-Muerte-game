using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [Header("UI")]
    public bool text;
    public bool image;

    private SpriteRenderer sr;
    private Text txt;
    private Image img;

    private Color color;
    private float alpha;
    private float alphaStartValue;                  //univerzalnost - da bi radila ma koja god bila pocetna alpha vrednost

    private int i;                                  //counter - prolazak kroz sva vremena
    private bool resume;
    private bool decrease;


    [Header("\"Ja cu load-ovati sledecu scenu, no worries bro\"")]
    [SerializeField] private bool waitAndLoad = false;
    [SerializeField] private float seconds;            //cekanje pre load scene 

    [Header("Time Between Transitions #APPEAR")]
    [SerializeField] private float[] appearTime; 

    [Header("Time Between Transitions #DISAPPEAR")]
    [SerializeField] private float[] disappearTime;
     


    private void Awake()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            sr = GetComponent<SpriteRenderer>();
        }


        if (GetComponent<Text>() != null)
        {
            txt = GetComponent<Text>();
        }


        if (GetComponent<Image>() != null)
        {
            img = GetComponent<Image>();
        }
    }

    void Start()
    {
        if (!text && !image)
        {
            color = sr.color;
            alpha = sr.color.a;
        }

        if (text)
        {
            color = txt.color;
            alpha = txt.color.a;
        } 

        if (image)
        {
            color = img.color;
            alpha = img.color.a;
        }


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

            if (alpha >= 1f  )
            { 

                if (Input.GetButtonDown("Select"))
                {
                    decrease = true;
                }
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
            StartCoroutine(loadScene());
        }
    }

    private IEnumerator decreaseShade(float percent, float time)
    {
        yield return new WaitForSeconds(time);
        alpha -= percent;

        if (text)
        {
            txt.color = new Color(color.r, color.g, color.b, alpha);
        }

        if(!image && !text)
        {
            sr.color = new Color(color.r, color.g, color.b, alpha);
        }

        if (image)
        { 
            img.color = new Color(color.r, color.g, color.b, alpha);
        }

        resume = true;
    } 

    private IEnumerator increaseShade(float percent, float time)
    {
        yield return new WaitForSeconds(time);
        alpha += percent;

        if (text)
        {
            txt.color = new Color(color.r, color.g, color.b, alpha);
        }

        if(!image && !text)
        {
            sr.color = new Color(color.r, color.g, color.b, alpha);
        }

        if (image)
        {
            img.color = new Color(color.r, color.g, color.b, alpha);
        }

        resume = true;
    }


    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(seconds);
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.LoadScene("Level_0" + SaveManager.level);
    }
}
