using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

	public LayerMask mapMask;

	public void RotateToDirection (Vector3 dir) {
		if (dir.magnitude > 0) {
			transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
		}
	}

	public void RotateToMousePosition () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, mapMask)) {
			transform.rotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
		}
	}
}
