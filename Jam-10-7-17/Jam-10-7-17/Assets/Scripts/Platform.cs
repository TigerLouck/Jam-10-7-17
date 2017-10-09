using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Platform Class
 * This class is used for platforms where the player can pass through.
 * 
 * Notes:
 *  - The player object must be named "Player" for this script to work.
 *  
 * Original Credit: https://pastebin.com/KArtkV1E
 */
public class Platform : MonoBehaviour {

    public string playerName = "Player";
    public float offset;

    private Collider2D collider;
    private GameObject player;
    private GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
        //Find player by name
        player = GameObject.Find(playerName);
        if (player == null) Debug.LogError("(One Way Platform) Please enter correct player name in Inspector for: " + gameObject.name);
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Method for ignoring collision with enemies
    void IgnoreEnemyCollision()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Physics2D.IgnoreCollision(collider, enemy.GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if player is under the platform. Collide only if the player is above the platform.
        if (player != null)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else if (player.transform.position.y < this.transform.position.y + offset) gameObject.GetComponent<Collider2D>().enabled = false;
            else gameObject.GetComponent<Collider2D>().enabled = true;
        }

        //IgnoreEnemyCollision();
    }
}
