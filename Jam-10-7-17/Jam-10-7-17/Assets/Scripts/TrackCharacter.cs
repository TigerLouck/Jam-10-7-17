using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Track Character Class
 * This class is used for the camera to track the player (or other objects).
 * The character and camera are public in the case that they are needed to be changed in-game (For instance, switching cameras or switching focus).
 * 
 * Notes:
 *  - This camera tracking is known as Position-locking.
 *  - Other camera tracking types can be found here: https://www.youtube.com/watch?v=pdvCO97jOQk
 */
public class TrackCharacter : MonoBehaviour {

    public GameObject character;
    public GameObject camera;
    public float xOffset;
    public float yOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = new Vector3(character.transform.position.x + xOffset, character.transform.position.y + yOffset, camera.transform.position.z);
	}
}
