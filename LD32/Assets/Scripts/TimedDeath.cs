using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

	public float TimeToDie;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		TimeToDie -= Time.deltaTime;
		if (TimeToDie <= 0)
			Destroy (this.gameObject);

	}
}
