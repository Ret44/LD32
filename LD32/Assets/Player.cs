using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player instance;
	public float playerSpeed;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			this.transform.Translate(Vector3.forward * playerSpeed);
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.Translate(Vector3.back * playerSpeed);
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.Translate(Vector3.left * playerSpeed);
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.Translate(Vector3.right * playerSpeed);
		}

	}
}
