using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public bool isEnemy;
	public int DMG;
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.tag == "Enemy" && !isEnemy) {
			col.collider.GetComponent<Enemy>().HP-=DMG;
			if(col.collider.GetComponent<Enemy>().HP<=0)
			{
				col.collider.GetComponent<Enemy>().mode = EnemyMode.Dying;
			}
		}
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward * speed * Time.deltaTime, Space.Self);
	}
}
