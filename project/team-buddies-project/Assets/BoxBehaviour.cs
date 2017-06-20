using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour {

	public float throwForceForward;
	public float throwForceUp;
	public float throwTorque;

	public float fraction;

	Transform holder;
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void Hold (Transform t) {
		holder = t;
	}

	public void Throw (Vector3 dir) {
		holder = null;
		rb.AddForce(dir * throwForceForward + Vector3.up * throwForceUp, ForceMode.Impulse);
		rb.AddTorque(Random.onUnitSphere * throwTorque, ForceMode.Impulse);
	}

	void Update () {
		if (holder != null) {
			transform.position = holder.position + holder.forward;
			transform.rotation = holder.rotation;
		}
	}

	// When box hits ground, round position and set rotation to zero
	void OnCollisionEnter (Collision c) {
		var pos = transform.position;
		pos.x = RoundToFraction(pos.x, fraction);
		pos.y = RoundToFraction(pos.y, fraction);
		pos.z = RoundToFraction(pos.z, fraction);
		transform.position = pos;
		transform.rotation = Quaternion.identity;
		rb.velocity = Vector3.zero;
	}

	// Function to keep the box on a grid
	float RoundToFraction (float n, float fraction) {
		var div = Mathf.Round(1f / fraction);
		return Mathf.Round(n * div) / div;
	}
}
