using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float destroyTime;

	void Start () {
		Destroy(gameObject, destroyTime);
	}

	void OnTriggerEnter (Collider c) {
	var enemy = c.transform.root.GetComponent<EnemyController>();
		if (enemy != null) {
			enemy.GetHit();
		}
		Destroy(gameObject);
	}
}
