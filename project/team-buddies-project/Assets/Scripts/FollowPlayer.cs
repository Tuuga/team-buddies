using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	Vector3 startPos;
	Transform player;

	void Start () {
		startPos = transform.position;
		player = FindObjectOfType<PlayerMovement>().transform;
	}

	void Update () {
		var pos = player.position + startPos;
		transform.position = pos;
	}
}
