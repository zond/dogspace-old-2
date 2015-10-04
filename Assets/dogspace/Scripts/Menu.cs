using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private bool headless = false;

	void Start() {
		if (System.Environment.GetCommandLineArgs () != null) {
			foreach (string arg in System.Environment.GetCommandLineArgs()) {
				if (arg == "-headless") {
					headless = true;
				}
			}
		}
		if (headless) {
			BoltLauncher.StartServer (UdpKit.UdpEndPoint.Parse ("0.0.0.0:27000"));
		}
	}

	void OnGUI() {
		if (!headless) {
			GUILayout.BeginArea (new Rect (10, 10, Screen.width - 20, Screen.height - 20));
		
			if (GUILayout.Button ("Start Server", GUILayout.ExpandWidth (true))) {
				Destroy (gameObject);
				BoltLauncher.StartServer (UdpKit.UdpEndPoint.Parse ("0.0.0.0:27000"));
			}
		
			if (GUILayout.Button ("Start Client", GUILayout.ExpandWidth (true))) {
				Destroy (gameObject);
				BoltLauncher.StartClient ();
			}
		
			GUILayout.EndArea ();
		}
	}
}