using UnityEngine;
using System.Collections;

public class ShipBehaviour : Bolt.EntityBehaviour<IShipState> {

	public override void Attached() {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.GetComponent<TrackingCamera> ().Target = transform;
		state.ShipTransform.SetTransforms(transform);
	}

}
