using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

	public void Rotate (Quaternion q) {
		transform.rotation = q;
	}
}
