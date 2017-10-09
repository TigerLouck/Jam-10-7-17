using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Destroy Self Class
 * This class is used for objects that are meant to be removed after a time interval.
 * 
 * Notes:
 *  - The time variables are measured in game ticks
 *  - The object will be destroyed, meaning any attached components will be destroyed as well.
 */
public class DestroySelf : MonoBehaviour {

    public int timeTicks;

    private int counter = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(counter >= timeTicks)
        {
            Destroy(gameObject);
        }

        counter++;
	}
}
