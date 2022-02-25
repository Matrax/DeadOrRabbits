using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int rabbitFounded;
    public double playerTime;

    // Start is called before the first frame update
    void Start()
    {
        this.playerTime = 0;
        this.rabbitFounded = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Update time played
        this.playerTime += Time.smoothDeltaTime;
    }
}
