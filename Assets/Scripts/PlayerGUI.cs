using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerGUI : MonoBehaviour
{

    //Player GUI attributes
    public TMP_Text fpsCounter;
    public TMP_Text rabbitCounter;
    public TMP_Text timerText;
    public GameObject escapeCanvas;
    public bool showEscapeCanvas;

    //Private timers
    private float fpsTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.fpsTimer = 0;
        this.showEscapeCanvas = false;

        //Set cursor and canvas inactive
        this.escapeCanvas.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<PlayerControlsScript>().dead == false)
        {
            //Update RabbitCounter
            this.rabbitCounter.text = "Lapins: " + this.GetComponent<PlayerStats>().rabbitFounded;

            //Update Timer
            int seconds = (int)this.GetComponent<PlayerStats>().playerTime;
            this.timerText.text = "Temps: " + seconds;

            //Update FPSCounter
            this.fpsTimer += Time.smoothDeltaTime;
            if (this.fpsTimer >= 1f && this.fpsCounter != null)
            {
                int fps = (int)(1 / Time.smoothDeltaTime);
                this.fpsCounter.text = "FPS: " + fps;
                this.fpsTimer = 0f;
            }

            //Show or hide the escape canvas
            if (Input.GetKeyDown("escape"))
            {
                this.showEscapeCanvas = !this.showEscapeCanvas;
                this.escapeCanvas.SetActive(this.showEscapeCanvas);
                Cursor.visible = this.showEscapeCanvas;

                if (this.showEscapeCanvas == true)
                {
                    this.GetComponent<PlayerControlsScript>().paused = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    this.GetComponent<PlayerControlsScript>().paused = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

}
