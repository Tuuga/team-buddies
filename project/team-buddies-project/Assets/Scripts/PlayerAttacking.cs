using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour {

	public LayerMask enemyMask;
	public GameObject bulletPrefab;
	public float bulletSpeed;
	public bool hasWeapon; // public for debug

	PlayerBoxCheck boxCheck;

	void Start () {
		boxCheck = GetComponent<PlayerBoxCheck>();
	}

	public void Attack () {
		if (hasWeapon) {
			Shoot();
		} else {
			Punch();
		}
	}

	void Shoot () {
		var bulletIns = (GameObject)Instantiate(bulletPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
		var rb = bulletIns.GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
		// Shoot animation
	}

	void Punch () {
		var cols = boxCheck.BoxCheck(enemyMask);
		foreach (Collider c in cols) {
			var ec = c.transform.root.GetComponent<EnemyController>();
			if (ec != null) {
				ec.GetHit();
			}
		}
		// Punch animation
	}

	public bool GetHasWeapon () {
		return hasWeapon;
	}
}
