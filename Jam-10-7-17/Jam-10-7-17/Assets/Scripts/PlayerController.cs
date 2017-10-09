using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int jumpPower;
	public float runSpeed;
	

	private bool gravDrawdown = false;
	private float startGrav;
	private bool swingControl = false;
	private float swordBoxX;
	private List<Collider2D> swingHits;
	// Use this for initialization
	void Start ()
	{
		startGrav = GetComponent<Rigidbody2D> ().gravityScale;
		swingHits = new List<Collider2D> ();
		swordBoxX = GetComponent<CapsuleCollider2D> ().offset.x;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//movement control
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( Input.GetAxis ( "Horizontal" ) * runSpeed, GetComponent<Rigidbody2D> ().velocity.y );
		Collider2D[] hits = new Collider2D[1];
		if (Input.GetKeyDown ( KeyCode.W ) && GetComponent<Rigidbody2D> ().OverlapCollider ( new ContactFilter2D(), hits ) > 0)
		{
			GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity + new Vector2 ( 0, jumpPower );
			gravDrawdown = true;
			GetComponent<Rigidbody2D> ().gravityScale = 0;
		}

		//player flipping
		if (Input.GetAxis("Horizontal") > 0)
		{
			GetComponent<SpriteRenderer> ().flipX = false;
			GetComponent<CapsuleCollider2D> ().offset = new Vector2 ( swordBoxX, GetComponent<CapsuleCollider2D> ().offset.y );
		}
		else if (Input.GetAxis ( "Horizontal" ) < 0)
		{
			GetComponent<SpriteRenderer> ().flipX = true;
			GetComponent<CapsuleCollider2D> ().offset = new Vector2 ( -swordBoxX, GetComponent<CapsuleCollider2D> ().offset.y );
		}

		//gravity drawdown for the player's jump
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

		//schwing da sword
		if (Input.GetKeyDown ( KeyCode.Space ) && !swingControl)
		{
			StartCoroutine ( Swing() );
		}
		if (swingControl && swingHits.Count > 0)
		{
			Debug.Log ( "swing connected!" );
			foreach (Collider2D collision in swingHits)
			{
				collision.gameObject.SendMessage ( "TakeDamage" );
				
			}
			swingControl = false;
		}
	}

	private IEnumerator Swing ()
	{
		
		Debug.Log ( "swung!" );
		//have the sword hitbox out for .2 seconds
		swingControl = true;
				
		yield return new WaitForSeconds ( .2f );
		
		swingControl = false;
		


	}

	private void OnTriggerEnter2D ( Collider2D collision )
	{
		Debug.Log ( "object in range!" );
		swingHits.Add ( collision );
		Debug.Log ( swingHits.Count );
	}
	private void OnTriggerExit2D ( Collider2D collision )
	{
		Debug.Log ( "object out of range!" );
		swingHits.Remove ( collision );
		Debug.Log ( swingHits.Count );
	}
	
}
