using UnityEngine;
using System.Collections;

public enum EnemyMode{
	Walking,
	Shooting,
	Dying
}

public class Enemy : MonoBehaviour {


	public int HP;

	public EnemyMode mode;
	public Vector3 direction;
	public float speed;
	public GameObject forwardSprite;
	public GameObject backSprite;
	public GameObject dyingSprite;
	public GameObject shootingSprite;

	public Animator animator;

	public bool isRekt;


	public float shotTimer;
	float shootTimer;
	bool shot; 

	public float randomReaction;
	// Use this for initialization
	void Start () {
		direction = Vector3.forward;
		animator = transform.FindChild ("kidsheet_0").GetComponent<Animator>();
	}

	public void SwitchAnimation(GameObject obj)
	{
		forwardSprite.SetActive (false);
		backSprite.SetActive (false);
		dyingSprite.SetActive (false);
		shootingSprite.SetActive (false);

		obj.SetActive (true);
	}
	// Update is called once per frame
	void Update () {
		if (mode == EnemyMode.Walking) {
			Vector3 oldPos = this.transform.position;
			this.transform.Translate (direction * speed * Time.deltaTime, Space.World);
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,-2.52f,this.transform.localPosition.z);
			if (Vector3.Distance (oldPos, Player.instance.transform.position) > Vector3.Distance (this.transform.position, Player.instance.transform.position)) {
				//SwitchAnimation(forwardSprite);
				animator.SetTrigger("forward");
			} else {
			//	SwitchAnimation (backSprite);
				animator.SetTrigger("backward");
			}
			RaycastHit hit;
			if (Physics.Raycast (this.transform.position, direction * 2, 2f)) {
				int dir = Random.Range (1, 4);
				switch (dir) {
				case 1:
					direction = Vector3.back;
					break;
				case 2:
					direction = Vector3.forward;
					break;
				case 3:
					direction = Vector3.left;
					break;
				case 4:
					direction = Vector3.right;
					break;
				}
			}
			randomReaction -= Time.deltaTime;
			if (randomReaction <= 0) {
				randomReaction = Random.Range (3, 8);
				int dir = Random.Range (0, 5);
				switch (dir) {
				case 1 : mode = EnemyMode.Walking; break;
				default : mode = EnemyMode.Shooting; break;
				}
			}
		}
		if (mode == EnemyMode.Shooting) {
			if(!shot)
			{
				shot = true;
				//SwitchAnimation(shootingSprite);
				animator.SetTrigger("shoot");
				GameObject bullet = Instantiate (Spawner.instance.bullet, this.transform.position + this.transform.forward * 2, this.transform.rotation) as GameObject;
				bullet.GetComponent<Projectile>().isEnemy = true;				                               
			}
			shootTimer-=Time.deltaTime;
			if(shootTimer<=0)
			{
				shootTimer = shotTimer;
				shot = false;
				mode = EnemyMode.Walking;
			}
		}
		if (mode == EnemyMode.Dying) {
			if(!isRekt){
				///SwitchAnimation(dyingSprite);
				animator.SetTrigger("die");
				GetComponent<LookAtPlayer>().enabled = false;
				GetComponent<Rigidbody>().useGravity = true;
				GetComponent<Rigidbody>().AddForce(-transform.forward * 2);
				isRekt = true;
			}
		}
	}
}
