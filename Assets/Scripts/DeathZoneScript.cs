using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{

    //Death Zone attributes
    public Vector3 min;
    public Vector3 max;
    public Vector3 direction;
    public GameObject player;
    public GameObject gameOverCanvas;
    public AudioSource gameOverAudio;
    public List<Vector3> spawns;
    public float distanceFromPlayer;
    public float speed;
    public double directionTime;

    //Danger level
    public float dangerLevel1;
    public float dangerLevel2;
    public float dangerLevel3;
    public int dangerLevel;


    // Start is called before the first frame update
    void Start()
    {
        //Get random spawn
        int random = Random.Range(0, this.spawns.Capacity);
        this.transform.position = this.spawns[random];

        //Set the first direction
        this.direction = this.Direction(this.player.transform.position, this.transform.position);

        //Set game over canvas inactive
        this.gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If paused, dont move
        if(this.player.GetComponent<PlayerControlsScript>().paused == false && this.player.GetComponent<PlayerControlsScript>().dead == false)
        {
            this.directionTime += Time.smoothDeltaTime;

            //Activation of heart sound
            this.distanceFromPlayer = Vector3.Distance(this.transform.position, this.player.transform.position);
            if (this.distanceFromPlayer <= this.dangerLevel1 && this.distanceFromPlayer > this.dangerLevel2)
            {
                if (this.dangerLevel != 1)
                {
                    //Play slow heart beat sound
                    this.player.GetComponent<PlayerInteractScript>().quickHeartAudio.Stop();
                    this.player.GetComponent<PlayerInteractScript>().slowHeartAudio.Play();
                    this.dangerLevel = 1;
                }
            } else if (this.distanceFromPlayer <= this.dangerLevel2 && this.distanceFromPlayer > this.dangerLevel3)
            {
                if (this.dangerLevel != 2)
                {
                    //Play quick heart beat sound
                    this.player.GetComponent<PlayerInteractScript>().slowHeartAudio.Stop();
                    this.player.GetComponent<PlayerInteractScript>().quickHeartAudio.Play();
                    this.dangerLevel = 2;
                }
            } else if (this.distanceFromPlayer <= this.dangerLevel3) {
                if (this.dangerLevel != 3)
                {
                    //Set the player dead
                    this.player.GetComponent<PlayerControlsScript>().dead = true;

                    //Stop all heart sounds and play gameover sound
                    this.player.GetComponent<PlayerInteractScript>().slowHeartAudio.Stop();
                    this.player.GetComponent<PlayerInteractScript>().quickHeartAudio.Stop();
                    this.gameOverAudio.Play();

                    //Set the gameover canvas
                    this.gameOverCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    this.dangerLevel = 3;
                }
            } else {
                //Stop all heart sounds
                this.player.GetComponent<PlayerInteractScript>().slowHeartAudio.Stop();
                this.player.GetComponent<PlayerInteractScript>().quickHeartAudio.Stop();
                this.dangerLevel = 0;
            }

            //Change direction every 15 seconds
            if (this.directionTime > 15.0)
            {
                this.direction = this.Direction(this.player.transform.position, this.transform.position);
                this.directionTime = 0;
            }
            this.transform.position += this.direction * this.speed * Time.smoothDeltaTime;
        }

    }

    private Vector3 Direction(Vector3 a, Vector3 b)
    {
        Vector3 c = a - b;
        c.y = 0;
        return c / c.magnitude;
    }
}
