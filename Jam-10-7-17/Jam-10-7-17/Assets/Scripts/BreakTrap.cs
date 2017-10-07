using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Break Trap Class
 * This class is specifically used for the Break Trap Blocks.
 * The script will cause the attached Game Object to remove its collider after a delay once the player collides with the block.
 * 
 * Notes:
 *  - The player object must be named "Player" for this script to work.
 */
public class BreakTrap : MonoBehaviour {

    public float breakDelay;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    // Method for detecting collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            StartCoroutine(BreakDelay());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Delayed method for when the block breaks
    IEnumerator BreakDelay()
    {
        // Destroys the collision object so that the player will fall through
        yield return new WaitForSeconds(breakDelay);
        Destroy(gameObject.GetComponent<Collider2D>());
    }
}
