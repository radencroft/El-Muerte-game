using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float selectedTime;
    private bool canStart; 
    void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (Input.GetButtonDown("Select") && canStart)
        {
            canStart = false;
            FindObjectOfType<AudioManager>().Play("select");

            FindObjectOfType<Blink>().blinkTime /= 4;
            FindObjectOfType<Blink>().txt.enabled = true;
            StartCoroutine(loadScene());
        }
    }
     

    public void StartBlinking()
    {
        FindObjectOfType<Blink>().canBlink = true;
        canStart = true;
    }

    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(selectedTime);
        SceneController sceneManager = FindObjectOfType<SceneController>();
        sceneManager.ChangeScene();
    }

    private void ExitGame()
    {
        if (Input.GetButtonDown("Escape"))
        {
            Application.Quit();
        }
    }
}
