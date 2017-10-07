using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int jumpPower;
	public float runSpeed;

	private bool gravDrawdown = false;
	private float startGrav;
	// Use this for initialization
	void Start ()
	{
		startGrav = GetComponent<Rigidbody2D> ().gravityScale;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( Input.GetAxis ( "Horizontal" ) * runSpeed, GetComponent<Rigidbody2D> ().velocity.y );
		Collider2D[] hits = new Collider2D[1];
		if (Input.GetKeyDown ( KeyCode.W ) && GetComponent<Rigidbody2D> ().OverlapCollider ( new ContactFilter2D(), hits ) > 0)
		{
			GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity + new Vector2 ( 0, jumpPower );
			gravDrawdown = true;
			GetComponent<Rigidbody2D> ().gravityScale = 0;
		}

		if (gravDrawdown && GetComponent<Rigidbody2D>().gravityScale < startGrav)
		{
			if (Input.GetKey(KeyCode.W))
			{
				GetComponent<Rigidbody2D> ().gravityScale += .05f;

			}
			else
			{
				GetComponent<Rigidbody2D> ().gravityScale = startGrav;
			}
		}
		else
		{
			gravDrawdown = false;
		}
	}
}
