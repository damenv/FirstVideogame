using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	//PRUEBA 1:
	//public int maxHealth = 100; 72/6 = 12 
	//public int curHealth = 100;

	//PRUEBA 2:
	public Texture2D frameTexture;
	public Texture2D HBTexture;

	public int healthWidth = -25;
	public int healthHeight = 22;

	public int healthMarginLeft = 97;
	public int healthMarginTop = 17;

	public int frameWidth = 200;
	public int frameHeight = 30;

	public int frameMarginLeft = 10;
	public int frameMarginTop = 10;

	public GameObject dude;
	//public Transform player = dude.transform;
	public Animator anim;
	// Use this for initialization
	void Start () {
		dude = GameObject.FindGameObjectWithTag ("Player");
		anim = dude.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		//PRUEBA 1:
		//GUI.Box (new Rect(10,10,Screen.width/2/(maxHealth/curHealth), 20), curHealth + "/" + maxHealth);

		//PRUEBA 2:

		GUI.DrawTexture(new Rect(frameMarginLeft,frameMarginTop, frameMarginLeft + frameWidth,frameMarginTop + frameHeight), frameTexture, ScaleMode.ScaleToFit, true, 0 );

		GUI.DrawTexture(new Rect(healthMarginLeft,healthMarginTop,healthWidth + healthMarginLeft, healthHeight), HBTexture, ScaleMode.ScaleAndCrop, true, 0 );

	}

	public void alterHealth(int factor){ //El factor es 12 para que sea luego de 6 golpes la muerte
		if (healthWidth <= -97) {
			healthWidth = -97; //MUERTE
			anim.SetBool("Muerto", true);
		}
		else if (healthWidth > -25)
			healthWidth = -25;
		else
			healthWidth += factor;

	}
}
