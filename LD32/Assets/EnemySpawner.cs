using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	public float maxTimeToSpawn;
	public float spawnTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnTime -= Time.deltaTime;
		if (spawnTime <= 0) {
			spawnTime = Random.Range (5,maxTimeToSpawn);
			maxTimeToSpawn -= Random.Range (0.1f,0.3f);
			GameObject enemy = Instantiate(Spawner.instance.enemy) as GameObject;
			enemy.transform.parent = this.transform;
			enemy.transform.localPosition = new Vector3(1f,1f,0f);
			enemy.transform.parent = GameObject.Find ("MapGeometry").transform;
		}
	}
}
