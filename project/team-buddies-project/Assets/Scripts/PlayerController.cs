using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float gravity;
	public bool grounded = true; // public for debug

	PlayerMovement movement;
	PlayerRotation rotation;
	PlayerAttacking attacking;
	Rigidbody rb;

	void Start () {
		movement = GetComponent<PlayerMovement>();
		rotation = GetComponent<PlayerRotation>();
		attacking = GetComponent<PlayerAttacking>();
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

		// if moving to any direction
		movement.Move(moveDir);

		// Jump
		if (Input.GetKeyDown(KeyCode.Space) && grounded) {
			movement.Jump();
			grounded = false;
		}		

		// Rotation
		if (attacking.GetHasWeapon()) {
			rotation.RotateToMousePosition();
		} else {
			rotation.RotateToDirection(moveDir);
		}

		// Attacking
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			attacking.Attack();
		}
		
		// DEBUG MODESWITCH
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			attacking.hasWeapon = false;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			attacking.hasWeapon = true;
		}
	}

	void FixedUpdate () {
		rb.AddForce(Vector3.down * gravity);
	}

	void OnCollisionEnter (Collision c) {
		grounded = true;
	}
}
