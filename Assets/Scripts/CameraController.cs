using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject emptyOriginObject = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (emptyOriginObject != null)
        {
            transform.LookAt(emptyOriginObject.transform);
            transform.RotateAround(
                emptyOriginObject.transform.position,
                Vector3.up, Time.deltaTime * 2);
        }
	}
}
