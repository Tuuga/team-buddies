using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	enum RotationMode { Movement, Mouse };
	RotationMode currentRotationMode = RotationMode.Movement;

	public float gravity;
	public bool grounded = true; // public for debug

	PlayerMovement movement;
	PlayerRotation rotation;
	Rigidbody rb;

	void Start () {
		movement = GetComponent<PlayerMovement>();
		rotation = GetComponent<PlayerRotation>();
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		// Movement
		var moveDir = new Vector3();
		if (Input.GetKey(KeyCode.W)) {
			moveDir += Vector3.forward;
		}
		if (Input.GetKey(KeyCode.A)) {
			moveDir += Vector3.left;
		}
		if (Input.GetKey(KeyCode.S)) {
			moveDir += Vector3.back;
		}
		if (Input.GetKey(KeyCode.D)) {
			moveDir += Vector3.right;
		}

		// Jump
		if (Input.GetKeyDown(KeyCode.Space) && grounded) {
			movement.Jump();
			grounded = false;
		}

		// if moving to any direction
		if (moveDir.magnitude > 0) {
			movement.Move(moveDir);

			// Rotation
			if (currentRotationMode == RotationMode.Movement) {
				rotation.Rotate(Quaternion.LookRotation(moveDir, Vector3.up));
			} else if (currentRotationMode == RotationMode.Mouse) {
				// Rotate to mouse position
			}
		}
	}

	void FixedUpdate () {
		rb.AddForce(Vector3.down * gravity);
	}

	void OnCollisionEnter (Collision c) {
		grounded = true;
	}
}
