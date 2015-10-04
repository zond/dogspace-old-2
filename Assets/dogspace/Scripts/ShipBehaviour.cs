using UnityEngine;
using System.Collections;

public class ShipBehaviour : Bolt.EntityBehaviour<IShipState> {

	// Use this for initialization
	void Start () {
		if (BoltNetwork.isClient) {
			GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
			camera.GetComponent<TrackingCamera> ().Target = transform;
		}
	}
}
