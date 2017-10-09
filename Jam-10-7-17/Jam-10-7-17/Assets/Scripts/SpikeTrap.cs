using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Spike Trap Class
 * This class is specifically used for the Spike Trap Blocks.
 * The script spawns an attached prefab for the Spike object one block above the object it is on.
 * The OnTrigger boolean can be set to true if the trigger should only register once the player collides with the block.
 * 
 * Notes:
 *  - All time variables are measured in game ticks
 *  - The player object must be named "Player" for this script to work.
 *  - The player object must have a Collider2D and Rigidbody2D.
 */
public class SpikeTrap : MonoBehaviour {

    public GameObject spikeObject;
    public bool onTrigger;
    public float startDelayTicks;
    public float timeDelayTicks;
    public float warnDurationTicks;
    public float spikeDurationTicks;

    private int counter = 0;
    private bool startDelayDone = false;
    private string spikeState = "Down";
    private bool triggered = false;
    private GameObject player;
    private GameObject spikeObjectSpawn;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    // Method for collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player" && onTrigger)
        {
            triggered = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Start delay
        if(!startDelayDone && counter > startDelayTicks)
        {
            startDelayDone = true;
            counter = 0;
        }

        // Starts running after start delay
        if(startDelayDone)
        {
            // Enabled for players stepping on the tile
            if(triggered && onTrigger)
            {
                spikeState = "Triggered";
                counter = 0;
                Vector3 spikePosition = transform.position;
                spikePosition += new Vector3(0, 0.5f, 0);
                spikeObjectSpawn = Instantiate(spikeObject, spikePosition, Quaternion.identity);
                spikeObjectSpawn.GetComponent<Spikes>().enabled = false;
            }
            else if(spikeState == "Down" && counter >= timeDelayTicks && !onTrigger)
            {
                
                spikeState = "Triggered";
                counter = 0;
                Vector3 spikePosition = transform.position;
                spikePosition += new Vector3(0, 0.5f, 0);
                spikeObjectSpawn = Instantiate(spikeObject, spikePosition, Quaternion.identity);
                spikeObjectSpawn.GetComponent<Spikes>().enabled = false;
            }
            else if(spikeState == "Triggered" && counter >= warnDurationTicks)
            {
                spikeState = "Up";
                counter = 0;
                Vector3 spikePosition = transform.position;
                spikePosition += new Vector3(0, 1, 0);
                spikeObjectSpawn.transform.position = spikePosition;
                spikeObjectSpawn.GetComponent<Spikes>().enabled = true;
            }
            else if(spikeState == "Up" && counter >= spikeDurationTicks)
            {
                spikeState = "Down";
                counter = 0;
                Destroy(spikeObjectSpawn);
            }
        }

        counter++;
	}
}
