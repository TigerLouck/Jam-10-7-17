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
 *  - The player object must have a Collider2D and Rigidbody2D.
 */
public class BreakTrap : MonoBehaviour {

    public float fallDistancePerTick;
    public int breakAnimationTicks;

    private GameObject player;
    private bool triggered = false;
    private int counter = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    // Method for detecting collision
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            triggered = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(triggered)
        {
            Destroy(gameObject.GetComponent<Collider2D>());
            gameObject.transform.position += new Vector3(0, -fallDistancePerTick, 0);

            if(counter > breakAnimationTicks)
            {
                Destroy(gameObject);
            }

            counter++;
        }
	}
}
