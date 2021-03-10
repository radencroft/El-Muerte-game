using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    [Header("FIND PLAYER")]
    [SerializeField] private string playerName;
    [SerializeField] private float xOffset;
    [SerializeField] private GameObject mark;
    private PlayerOne player;
    private bool inRange;

    [Header("DIALOGUE")]
    [SerializeField] private string[] dialogue;
    private bool canTalk;
    public bool startTalking;

    [Header("PLAYER CHECK")]
    [SerializeField] Vector2 areaSize;
    [SerializeField] LayerMask whatIsPlayer;

    [Header("DIALOGUE UI")]
    [SerializeField] private string backgroundBoxName;
    [SerializeField] private string textBoxName;
    private GameObject dialogueUI;
    private Text textBox;

    [Header("TEXT")]
    [SerializeField] private float txtSpeed;
    [SerializeField] private float fasterTxtSpeed;
    private float speed;

    private int currentLine;                                //Trenutni line osobe koja prica

    List<char> sentance = new List<char>();
    private bool nextSentance;
    [SerializeField] private float sentanceWait;            //pause between sentances

    [SerializeField] private Color clrPlayer, clrSpeaker;
    [SerializeField] private bool[] speakersLines;

    [Header("AUDIO")]
    [SerializeField] private int playerVoiceSpeed;
    [SerializeField] private int speakerVoiceSpeed;
    private int voiceCounter;

    private void Awake()
    {
        player = GameObject.Find(playerName).GetComponent<PlayerOne>();
        dialogueUI = GameObject.Find(backgroundBoxName);
        textBox = GameObject.Find(textBoxName).GetComponent<Text>();
    }

    void Start()
    {
        currentLine = 0;
        nextSentance = true;
        textBox.color = clrPlayer;
        dialogueUI.SetActive(false);
        textBox.text = "";
    }

    void Update()
    {
        PlayerCheck();
        Speak();
    }

    private void PlayerCheck()
    {
        Vector3 checkPos = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z);
        canTalk = Physics2D.OverlapBox(checkPos, areaSize, 0f, whatIsPlayer); 
    }

    private void Speak()
    {
        if (inRange)
        {
            if (currentLine < dialogue.Length)
            {
                if (canTalk )
                {
                    if (Input.GetButtonDown("Use") || startTalking)
                    {
                        startTalking = false;
                        if (nextSentance)
                        {
                            nextSentance = false;
                            speed = txtSpeed;

                            GameObject.FindGameObjectWithTag("player").GetComponent<PlayerOne>().inDialogue = true;
                            dialogueUI.SetActive(true);
                            sentance.Clear();                                                   //isprazni listu
                            foreach (char c in dialogue[currentLine]) { sentance.Add(c); }      //napuni listu slovima trenutne recenice
                            StartCoroutine(addLetter(speed, sentance));
                        }
                        else
                        {
                            speed = fasterTxtSpeed;
                        }
                    }
                    else if (Input.GetButtonDown("Ability") && nextSentance)
                    {
                        StartCoroutine(exitDialogue());
                    }
                } 
            }
            else
            {
                StartCoroutine(exitDialogue());
            }
        }
    }

    IEnumerator addLetter(float s, List<char> sentance)
    {
        textBox.text = "";

        for (int i = 0; i < sentance.Count; i++)
        {
            voiceCounter++;

            //NPC TALKS
            if (speakersLines[currentLine] == true)
            {
                textBox.color = clrSpeaker;

                if (voiceCounter == speakerVoiceSpeed)
                {
                    FindObjectOfType<AudioManager>().Play("npc");
                    voiceCounter = 0;
                }
            }
            //PLAYER TALKS
            else
            {
                textBox.color = clrPlayer;

                if (voiceCounter == playerVoiceSpeed)         // Na koje slovo da ispusti zvuk npr. svako cetvrto
                {
                    FindObjectOfType<AudioManager>().Play("player");
                    voiceCounter = 0;
                }
            }

            //dodaj slovo
            textBox.text += sentance[i];
            if ((sentance[i] == '.' || sentance[i] == '?' || sentance[i] == '!' || sentance[i] == ',') && i < sentance.Count - 3)
            {
                yield return new WaitForSeconds(sentanceWait);
            }

            yield return new WaitForSeconds(speed);
        }
        currentLine++;
        nextSentance = true;
    }

    IEnumerator exitDialogue()
    {
        GameObject.FindGameObjectWithTag("player").GetComponent<PlayerOne>().inDialogue = false;
        dialogueUI.SetActive(false);
        textBox.text = "";
        currentLine = 0;
        voiceCounter = 0;
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && other.GetComponent<PlayerJump>().grounded)
        {
            inRange = true;
            startTalking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && other.GetComponent<PlayerJump>().grounded)
        {
            inRange = false;
            Destroy(this.gameObject);
        }
    }


}
