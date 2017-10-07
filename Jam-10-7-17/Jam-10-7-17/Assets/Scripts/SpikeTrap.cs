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
 *  - Starting Delay is advised to be set above 0.5 seconds. The Start method occurs during level loading but Update does not, which can cause time delay issues.
 *  - The player object must be named "Player" for this script to work.
 */
public class SpikeTrap : MonoBehaviour {

    public GameObject spikeObject;
    public bool onTrigger;
    public float startDelay;
    public float timeDelay;
    public float spikeDuration;

    private bool isActive = true;
    private bool triggered = false;
    private GameObject player;
    private GameObject spikeObjectSpawn;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        StartCoroutine(StartDelay());
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
        // Triggers spikes on player touch
        if(!isActive && triggered && onTrigger)
        {
            isActive = true;
            StartCoroutine(SpawnSpikes());
        }
        // Triggers spikes over time
		else if(!isActive)
        {
            isActive = true;
            StartCoroutine(SpawnSpikes());
        }
	}

    // Delayed method for when the spikes first spawn
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        isActive = false;
    }

    // Delayed method for spawning spikes
    IEnumerator SpawnSpikes()
    {
        // Spawns spikes once the time is up and starts the delay to destroy them
        yield return new WaitForSeconds(timeDelay);
        Vector3 spikePosition = transform.position;
        spikePosition += new Vector3(0, 1, 0);
        spikeObjectSpawn = Instantiate(spikeObject, spikePosition, Quaternion.identity);
        StartCoroutine(RemoveSpikes());
    }

    // Delayed method for removing spikes
    IEnumerator RemoveSpikes()
    {
        // Destroys the spawned spikes once the time is up
        yield return new WaitForSeconds(spikeDuration);
        Destroy(spikeObjectSpawn);
        isActive = false;
    }
}
