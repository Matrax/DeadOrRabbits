using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractScript : MonoBehaviour
{

    public KeyCode interactKey;
    public List<AudioSource> audioSources;
    public AudioSource slowHeartAudio;
    public AudioSource quickHeartAudio;
    public GameObject winCanvas;
    public GameObject spawnSystem;
    public GameObject interactText;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.interactText.SetActive(false);
        List<GameObject> rabbits = this.spawnSystem.GetComponent<SpawnScript>().rabbits;

        foreach (GameObject rabbit in rabbits)
        {
            //If we are near a rabbit
            if (Vector3.Distance(this.transform.position, rabbit.transform.position) < 1.4f)
            {
                if(Input.GetKey(this.interactKey))
                {
                    //Random audio source played
                    int random = Random.Range(0, this.audioSources.Capacity);
                    this.audioSources[random].Play();

                    //Destroy rabbit
                    Destroy(rabbit);
                    rabbits.Remove(rabbit);
                    this.GetComponent<PlayerStats>().rabbitFounded++;

                    //Check win condition
                    if(this.GetComponent<PlayerStats>().rabbitFounded == 7)
                    {
                        //Set the player dead (to pause all controls)
                        this.GetComponent<PlayerControlsScript>().dead = true;

                        //Stop all heart sounds
                        this.GetComponent<PlayerInteractScript>().slowHeartAudio.Stop();
                        this.GetComponent<PlayerInteractScript>().quickHeartAudio.Stop();

                        //Set the gameover canvas
                        this.winCanvas.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }

                //Set interact text active
                this.interactText.SetActive(true);
                break;
            }
        }
    }
}
