using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player instance;
	public float playerSpeed;

	public float shootingDelay;
	private float shootDelay;

	public GameObject playerBullet;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}

	public static void Shoot()
	{
		if (instance.shootDelay < 0) {
			Instantiate (instance.playerBullet, instance.transform.position + instance.transform.forward * 2, instance.transform.rotation);
			instance.shootDelay = instance.shootingDelay;
		}
	}

	// Update is called once per frame
	void Update () {
		instance.shootDelay -= Time.deltaTime;
		if (Input.GetKey (KeyCode.W)) {
			this.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			this.transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			this.transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			this.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
		}

		if (Input.GetMouseButton (0))
			Player.Shoot ();

	}
}
