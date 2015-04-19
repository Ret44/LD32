using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Vector3 direction;
	public float speed;
	public GameObject forwardSprite;
	public GameObject backSprite;

	public float randomReaction;
	// Use this for initialization
	void Start () {
		direction = Vector3.forward;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPos = this.transform.position;
		this.transform.Translate(direction * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance (oldPos, Player.instance.transform.position) > Vector3.Distance (this.transform.position, Player.instance.transform.position)) {
				forwardSprite.SetActive(true);
				backSprite.SetActive(false);
			}
			else {
				forwardSprite.SetActive(false);
				backSprite.SetActive(true);
			}
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, direction * 2, 2f)) {
			int dir = Random.Range (1,4);
			switch(dir)
			{
			case 1 : direction = Vector3.back; break;
			case 2: direction = Vector3.forward; break;
			case 3 : direction = Vector3.left; break;
			case 4: direction = Vector3.right; break;
			}
		}
		randomReaction -= Time.deltaTime;
		if (randomReaction <= 0) {
			randomReaction = Random.Range (3,8);
			int dir = Random.Range (1,4);
			switch(dir)
			{
			case 1 : direction = Vector3.back; break;
			case 2: direction = Vector3.forward; break;
			case 3 : direction = Vector3.left; break;
			case 4: direction = Vector3.right; break;
			}
		}
	}
}
