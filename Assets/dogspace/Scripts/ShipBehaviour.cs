using UnityEngine;
using System.Collections;

public class ShipBehaviour : Bolt.EntityBehaviour<IShipState> {

	private float roll;
	private float pitch;
	private float throttle;
	private Rigidbody body;
	private ConstantForce force;
	private Vector3 forceVector;

	public float maxRoll = 1.5f;
	public float maxPitch = 1.5f;
	public float maxThrottle = 25f;

	public override void Detached() {
		Destroy (gameObject);
	}

	public override void ControlGained() {
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.GetComponent<TrackingCamera> ().Target = transform;
	}

	public override void Attached() {
		state.ShipTransform.SetTransforms (transform);
		body = GetComponent<Rigidbody> ();
		force = GetComponent<ConstantForce> ();
	}

	private void PollKeys() {
		if (Input.GetKey (KeyCode.Alpha1)) {
			throttle = 0.0f;
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			throttle = 0.11f;
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			throttle = 0.22f;
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			throttle = 0.33f;
		} else if (Input.GetKey (KeyCode.Alpha5)) {
			throttle = 0.45f;
		} else if (Input.GetKey (KeyCode.Alpha6)) {
			throttle = 0.56f;
		} else if (Input.GetKey (KeyCode.Alpha7)) {
			throttle = 0.67f;
		} else if (Input.GetKey (KeyCode.Alpha8)) {
			throttle = 0.78f;
		} else if (Input.GetKey (KeyCode.Alpha9)) {
			throttle = 0.89f;
		} else if (Input.GetKey (KeyCode.Alpha0)) {
			throttle = 1.0f;
		}

		roll = 0;;
		if (Input.GetKey(KeyCode.A)) {
			roll = -1.0f;
		} else if (Input.GetKey (KeyCode.D)) {
			roll = 1.0f;
		}

		pitch = 0;
		if (Input.GetKey (KeyCode.W)) {
			pitch = -1.0f;
		} else if (Input.GetKey(KeyCode.S)) {
			pitch = 1.0f;
		}
	}

	public void Update() {
		PollKeys ();
	}

	public override void SimulateController() {
		PollKeys();


		IShipCommandInput input = ShipCommand.Create();

		input.Pitch = pitch;
		input.Roll = roll;
		input.Throttle = throttle;

		entity.QueueInput(input);
	}

	public override void ExecuteCommand(Bolt.Command command, bool resetState) {
		ShipCommand cmd = (ShipCommand)command;
		
		if (resetState) {
			// we got a correction from the server, reset (this only runs on the client)
			transform.position = cmd.Result.Position;
			body.velocity = cmd.Result.Velocity;
			transform.rotation = cmd.Result.Rotation;
			body.angularVelocity = cmd.Result.AngularVelocity;
		}
		else {
			// apply movement (this runs on both server and client)
			forceVector.z = -cmd.Input.Throttle * maxThrottle;
			force.relativeForce = forceVector;
			transform.rotation = transform.rotation * Quaternion.Euler (cmd.Input.Pitch * maxPitch, 0, cmd.Input.Roll * maxRoll);

			// copy the motor state to the commands result (this gets sent back to the client)
			cmd.Result.Position = transform.position;
			cmd.Result.Velocity = body.velocity;
			cmd.Result.Rotation = transform.rotation;
			cmd.Result.AngularVelocity = body.angularVelocity;
		}
	}

}
