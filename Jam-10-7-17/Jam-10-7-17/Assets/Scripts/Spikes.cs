using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Spikes Class
 * This class is used for just spikes and are meant to hurt the player.
 * In the case that it should be used to hurt for more than just the player, set the PlayerOnly boolean to false.
 * 
 * Notes:
 *  - The player object must be named "Player" for this script to work.
 *  - The affected Game Objects must have a Stats script on them.
 *  - The affected Game Objects must have a Collider2D and Rigidbody2D.
 */
public class Spikes : MonoBehaviour {

    public int damage;
    public bool playerOnly;
    public GameObject blood;

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}

    // Method for detecting trigger collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            player.GetComponent<Stats>().health -= damage;
            GameObject bloodClone = Instantiate(blood, player.transform.position, Quaternion.identity);
        }

        if(col.gameObject.GetComponent<Stats>() != null && !playerOnly)
        {
            col.gameObject.GetComponent<Stats>().health -= damage;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
