using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ManageUI : MonoBehaviour
{
    private Inventory inventory;
    private PlayerOne player;
    private Animator animPlayer;

    private int bulletCount;
    private int hpCount;

    [Header("ICON")]
    [SerializeField] private Image icon;
    [SerializeField] private Sprite falcon, gun;

    [Header("BULLETS")]
    [SerializeField]  private List<Image> bullets = new List<Image>();

    [Header("HP")]
    [SerializeField] private List<Image> bars = new List<Image>();


    [Header("EXIT")]
    [SerializeField] private Image exitPrompt;
    [SerializeField] private Text[] exitTxts;
    [SerializeField] private string menuScene;
    private bool inPrompt;


    private void Awake()
    {
        animPlayer = GameObject.FindGameObjectWithTag("player").GetComponent<Animator>();
        player = FindObjectOfType<PlayerOne>();
    }

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        bulletCount = bullets.Count;
        icon.sprite = falcon;

        hpCount = player.health;
    }
     
    void Update()
    {
        Preview(bulletCount, "bullets", bullets);
        Preview(hpCount, "health", bars);
         
        IconPreview();
        Prompt();
    }
    private void Preview(int count, string what, List<Image> previewItem)
    {
        if (what == "health")
        {
            hpCount = player.health;
        }
        else
        {
            count =  inventory.bullets;
        }

        for (int i = 0; i < count; i++)
        {
            previewItem[i].enabled = true;
        }

        for (int i = count; i < previewItem.Count; i++)
        {
            previewItem[i].enabled = false;
        }
    }
     
    private void IconPreview()
    {
        if (animPlayer.GetLayerWeight(1) == 1)
        {
            icon.sprite = gun;
        }
        else
        {
            icon.sprite = falcon;
        }
    }

    private void Prompt()
    {
        if (Input.GetButtonDown("Exit") && !inPrompt)
        {
            inPrompt = true;
            exitPrompt.enabled = true;

            foreach (Text txt in exitTxts)
            {
                txt.enabled = true;
            }

            return;
        }
        else if (Input.GetButtonDown("Exit") &&  inPrompt)
        {
            inPrompt = false;
            exitPrompt.enabled = false;

            foreach (Text txt in exitTxts)
            {
                txt.enabled = false;
            }

            return;
        }

        if (inPrompt && Input.GetButtonDown("Select"))
        {
            FindObjectOfType<Inventory>().DropWeapon("pistol A");
            FindObjectOfType<SceneController>().LoadScene(menuScene);
        } 
    }
     
}
