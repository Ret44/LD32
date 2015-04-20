using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player instance;
	public float playerSpeed;

	public float shootingDelay;
	private float shootDelay;

	public GameObject playerBullet;

	public int HP;
	public GameObject uiNormal;
	public GameObject uiCurious;
	public GameObject uiHit;
	public Text uiHP;
	public Text uiAmmo;
	public bool isMoving;
	public bool isDead;

	public Animator handAnimator;

	private float hitTimer;

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

	public static void Hit(int dmg)
	{
		if (!instance.isDead) {
			instance.SetFace (instance.uiHit);
			instance.hitTimer = 1.5f;
			instance.HP -= dmg;
			instance.uiHP.text = instance.HP.ToString ();
			if (instance.HP <= 0) {
				instance.isDead = true;
				instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				instance.GetComponent<Rigidbody>().AddForce(-instance.transform.forward*2);
				instance.handAnimator.gameObject.SetActive(false);
			}
		}
	}

	void SetFace(GameObject normal)
	{
		uiNormal.SetActive (false);
		uiCurious.SetActive (false);
		uiHit.SetActive (false);

		normal.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			hitTimer -= Time.deltaTime;
			if (hitTimer <= 0 && shootDelay <= 0) {
				SetFace (uiNormal);
			} else if (hitTimer > 0) {
				SetFace (uiHit);
			} else if (hitTimer <= 0 && shootDelay > 0) {
				SetFace (uiCurious);
			}
			isMoving = false;
			instance.shootDelay -= Time.deltaTime;
			if (Input.GetKey (KeyCode.W)) {
				this.transform.Translate (Vector3.forward * playerSpeed * Time.deltaTime);
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.S)) {
				this.transform.Translate (Vector3.back * playerSpeed * Time.deltaTime);
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.A)) {
				this.transform.Translate (Vector3.left * playerSpeed * Time.deltaTime);
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.D)) {
				this.transform.Translate (Vector3.right * playerSpeed * Time.deltaTime);
				isMoving = true;
			}

			handAnimator.SetBool ("isMoving", isMoving);

			if (Input.GetMouseButton (0))
				Player.Shoot ();
		}
	}
}
