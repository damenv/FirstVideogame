using UnityEngine;
using System.Collections;

public class GoblinWarriorController : MonoBehaviour {
	public Transform target;
	public Transform targetHealth;
	public PlayerHealth targetHealthScript;
	public DavidControllerScript dcs;

	public Transform sword;
	public BoxCollider2D swordCollider;

	public int moveSpeed = 4;

	bool viendoDer = true;

	Animator anim;

	private Transform myTransform;

	public bool atacando = false;
	public float timerAtaque = 2f;

	void Awake(){
		myTransform = transform;
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		GameObject go = GameObject.FindGameObjectWithTag ("Player");

		dcs = (DavidControllerScript)go.GetComponent ("DavidControllerScript");

		target = go.transform;
		targetHealth = target.FindChild ("GUIHealth");
		//targetHealth = go.GetComponent("GUIHealth");
		targetHealthScript = (PlayerHealth)targetHealth.GetComponent ("PlayerHealth");

		sword = myTransform.FindChild ("SwordCheck");
		swordCollider = (BoxCollider2D)sword.GetComponent ("BoxCollider2D");
	}

	void FixedUpdate(){
		//Look at target
		if (target.position.x < myTransform.position.x && viendoDer)
			Flip ();
		else if (target.position.x > myTransform.position.x && !viendoDer)
			Flip ();
		
		//Move towards target
		if(!viendoDer)
			myTransform.position += myTransform.right * -1 * moveSpeed * Time.deltaTime;
		else
			myTransform.position += myTransform.right * moveSpeed * Time.deltaTime;
		//anim.SetFloat ("Speed", Mathf.Abs(myTransform.rigidbody2D.velocity.x));

		anim.SetFloat ("Speed", moveSpeed);
	}

	// Update is called once per frame
	void Update () {
		//Attack ();
		float distance = Vector2.Distance (myTransform.position, target.transform.position);
		
		if (distance < 1.4) {
			moveSpeed = 0;
			
			timerAtaque -= Time.deltaTime;
			
			if (timerAtaque <= 0) {
				atacando = false;
				timerAtaque = 0.5f;
			}
			
			if(!atacando){
				Attack ();
			}
		}
		else
			moveSpeed = 4;
	}

	void Attack(){
		anim.SetTrigger ("Attack");
		atacando = true;
		targetHealthScript.alterHealth (-12);
		dcs.rotate = true;
//
//		float distance = Vector2.Distance (myTransform.position, target.transform.position);
//		
//		if (distance < 1.4) {
//			moveSpeed = 0;
//			
//			timerAtaque -= Time.deltaTime;
//			
//			if (timerAtaque <= 0) {
//				atacando = false;
//				timerAtaque = 0.5f;
//			}
//			
//			if(!atacando){
//				anim.SetTrigger ("Attack");
//				atacando = true;
//				targetHealthScript.alterHealth (-12);
//			}
//		}
//		else
//			moveSpeed = 4;
	}

	void Flip(){
		viendoDer = !viendoDer;
		Vector3 Escala = myTransform.localScale;
		Escala.x *= -1;
		myTransform.localScale = Escala;
	}
}
