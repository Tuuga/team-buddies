using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour {

	public GameObject bulletPrefab;
	public float bulletSpeed;

	public Vector3 punchCheckCenter;
	public Vector3 punchCheckHalfSize;
	public bool drawPunchCheckGizmo;

	public LayerMask enemyMask;
	public bool hasWeapon; // public for debug

	void OnDrawGizmos () {
		if (drawPunchCheckGizmo) {
			Gizmos.color = new Color(1, 0, 0, 0.5f);
			var center = transform.position + transform.right * punchCheckCenter.x + transform.up * punchCheckCenter.y + transform.forward * punchCheckCenter.z;
			Gizmos.DrawCube(center, punchCheckHalfSize);
		}
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
		var center = transform.position + transform.right * punchCheckCenter.x + transform.up * punchCheckCenter.y + transform.forward * punchCheckCenter.z;
		var cols = Physics.OverlapBox(center , punchCheckHalfSize, transform.rotation, enemyMask);
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
