using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFlip : MonoBehaviour {
	// Update is called once per frame
	void LateUpdate () {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
	}
}
