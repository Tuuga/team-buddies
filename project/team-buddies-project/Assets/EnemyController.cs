using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float hitForceBack, hitForceUp;

	Transform player;
	Rigidbody rb;
	public bool grounded; // public for debug

	void Start () {
		player = FindObjectOfType<PlayerController>().transform;
		rb = GetComponent<Rigidbody>();
	}

	public void GetHit () {
		if (grounded) {
			var hitDir = transform.position - player.position;
			hitDir.y = 0;
			hitDir = hitDir.normalized;
			rb.AddForce(hitDir * hitForceBack + Vector3.up * hitForceUp, ForceMode.Impulse);
			grounded = false;
			// Get hit animation
		}
	}

	void OnCollisionEnter (Collision c) {
		grounded = true;
	}
}
