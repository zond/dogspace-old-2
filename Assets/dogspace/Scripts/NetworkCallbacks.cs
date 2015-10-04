using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener {

	public override void Connected(BoltConnection connection) {
		if (BoltNetwork.isServer) {
			var pos = new Vector3 (Random.Range (-16, 16), 0, Random.Range (-16, 16));
			var player = BoltNetwork.Instantiate (BoltPrefabs.Ship, pos, Quaternion.Euler (new Vector3(Random.Range (0, 360), Random.Range (0,360), Random.Range (0,360))));
			player.AssignControl (connection);
		}
	}
	
	public override void BoltStartDone() {
		if (BoltNetwork.isClient) {
			BoltNetwork.Connect (UdpKit.UdpEndPoint.Parse ("127.0.0.1:27000"));
		}
	}
}