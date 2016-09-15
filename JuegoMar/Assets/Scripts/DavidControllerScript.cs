using UnityEngine;
using System.Collections;

public class DavidControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool viendoDer = true;
	public bool rotate = false;

	Animator anim;

	bool enSuelo = false;
	public Transform groundCheck;
	float radioSuelo = 0.1f; //Esfera que indica si se esta tocando el suelo
	public LayerMask queEsSuelo; //Indica que se puede identificar como suelo
	public float jumpForce = 300f;

	public bool atacando = false;
	public float timerAtaque = 0.05f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		enSuelo = Physics2D.OverlapCircle (groundCheck.position, radioSuelo,queEsSuelo);
		anim.SetBool ("Ground", enSuelo);
		//anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

		if (!anim.GetBool ("Muerto")) {
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));

			GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

			if (move > 0 && !viendoDer)
					Flip ();
			else if (move < 0 && viendoDer)
					Flip ();
		}

//		if (rotate) {
//			Rigidbody2D aux = GetComponent<Rigidbody2D>();
//			aux.rotation = (aux.rotation + 800*Time.deltaTime);
//			GetComponent<Rigidbody2D>().AddForce (new Vector2 (-200f,0));
//			Debug.Log (aux.rotation);
//			if(aux.rotation >= 360)
//			{
//				rotate = false;
//			}
//		}
	}

	void Update(){
		if (!anim.GetBool ("Muerto")) {
			if (enSuelo && Input.GetButtonDown ("Jump")) {
					anim.SetBool ("Ground", false);
					GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
			}

			//ATAQUE

			timerAtaque -= Time.deltaTime;

			if (timerAtaque <= 0) {
					atacando = false;
					timerAtaque = 0.05f;
			}

			if (!atacando && Input.GetButtonUp ("Fire1")) {
					anim.SetTrigger ("Attack");
					atacando = true;
			}
		}


	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log ("ENTRO");
		//REDUCIR LA VIDA
	}

	void OnCollisionEnter2D(Collision2D other){
		//Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "SwordCheck") {
			//Debug.Log ("YEI");
		}
	}

	void Flip(){
		viendoDer = !viendoDer;
		Vector3 Escala = transform.localScale;
		Escala.x *= -1;
		transform.localScale = Escala;
	}

	void RotateAfterHit()
	{
		rotate = true;
	}
}
