using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// the speed of the rotation
	public float speed = 10.0f;

	// setup the possible rotation states
	public enum whichWayToRotate {AroundX, AroundY, AroundZ}

	// set the direction of the rotation
	public whichWayToRotate way = whichWayToRotate.AroundX;

	// Update is called once per frame
	void Update () {

		// do the appropriate rotation based on the way state
		switch(way)
		{
		case whichWayToRotate.AroundX:
				transform.Translate(Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
				transform.Translate(Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
				break;
		case whichWayToRotate.AroundY:
			transform.Rotate(Vector3.up * Time.deltaTime * speed);
			break;
		case whichWayToRotate.AroundZ:
			transform.Rotate(Vector3.forward * Time.deltaTime * speed);
			break;
		}	
	}
}