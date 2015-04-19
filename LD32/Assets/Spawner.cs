using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public static Spawner instance;
	public GameObject enemy;
	public GameObject bullet;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
