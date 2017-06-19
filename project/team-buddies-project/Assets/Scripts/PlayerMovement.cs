using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;
	public float jumpForce;
	
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void Move (Vector3 dir) {
		if (dir.magnitude > 0) {
			dir = dir.normalized;
			rb.MovePosition(transform.position + dir * movementSpeed * Time.deltaTime);
			// Walk animation
		}		
	}
	
	public void Jump () {
		rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		// Jump animation
	}
}
