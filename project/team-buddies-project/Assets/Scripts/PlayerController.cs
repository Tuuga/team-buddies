using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public LayerMask boxMask;
	public float gravity;
	public bool grounded = true;	// public for debug

	public BoxBehaviour currentHeldBox;

	PlayerMovement movement;
	PlayerRotation rotation;
	PlayerAttacking attacking;
	PlayerBoxCheck boxCheck;
	Rigidbody rb;

	void Start () {
		movement = GetComponent<PlayerMovement>();
		rotation = GetComponent<PlayerRotation>();
		attacking = GetComponent<PlayerAttacking>();
		boxCheck = GetComponent<PlayerBoxCheck>();
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
		if (Input.GetKeyDown(KeyCode.Mouse0) && currentHeldBox == null) {
			attacking.Attack();
		}

		// Box holding
		if (Input.GetKeyDown(KeyCode.E)) {
			var cols = boxCheck.BoxCheck(boxMask);
			foreach (Collider c in cols) {
				currentHeldBox = c.transform.root.GetComponentInChildren<BoxBehaviour>();
				if (currentHeldBox != null) {
					currentHeldBox.Hold(transform);
					break;
				}
			}
		}

		// Box throwing
		if (Input.GetKeyDown(KeyCode.Mouse0) && currentHeldBox != null) {
			currentHeldBox.Throw(transform.forward);
			currentHeldBox = null;
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
