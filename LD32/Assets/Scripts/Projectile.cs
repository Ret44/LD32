using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
}
