using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoxCheck : MonoBehaviour {

	public Vector3 boxCheckCenter;
	public Vector3 boxCheckHalfSize;

	public bool drawPunchCheckGizmo;

	void OnDrawGizmos () {
		if (drawPunchCheckGizmo) {
			Gizmos.color = new Color(1, 0, 0, 0.5f);
			var center = transform.position + transform.right * boxCheckCenter.x + transform.up * boxCheckCenter.y + transform.forward * boxCheckCenter.z;
			Gizmos.DrawCube(center, boxCheckHalfSize);
		}
	}

	public Collider[] BoxCheck (LayerMask mask) {
		var center = transform.position + transform.right * boxCheckCenter.x + transform.up * boxCheckCenter.y + transform.forward * boxCheckCenter.z;
		var cols = Physics.OverlapBox(center, boxCheckHalfSize, transform.rotation, mask);
		return cols;
	}
}
