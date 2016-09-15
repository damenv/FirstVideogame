using UnityEngine;
using System.Collections;

public class OtherHealth : MonoBehaviour {

	public Texture2D frameTexture;
	public Texture2D HBTexture;
	
	public float healthWidth = (float)-30;
	public float healthHeight = 5;
	
	public float healthMarginLeft = 97;
	public float healthMarginTop = 17;
	
	public float frameWidth = (float)-30;
	public float frameHeight = (float)0.15;
	
	public float frameMarginLeft = 10;
	public float frameMarginTop = 10;
	
	public GameObject oParent;
	public Animator anim;

	public Vector3 posPadre;
	public Vector3 sizePadre;
	public float ratio;
	public Vector3 scalePadre;
	// Use this for initialization
	void Start () {
		oParent = this.transform.parent.gameObject;
		anim = oParent.GetComponent<Animator>();
		scalePadre = oParent.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

		healthMarginLeft = posPadre.x; 
		healthMarginTop = Screen.height/2 + posPadre.y/3;
		frameMarginLeft = posPadre.x - 15;
		frameMarginTop = Screen.height/2 + posPadre.y/3;
	}

	void OnGUI(){
		sizePadre = Camera.current.WorldToScreenPoint (oParent.GetComponent<Renderer>().bounds.size);
		posPadre = Camera.current.WorldToScreenPoint (oParent.transform.position);
		posPadre.y = Screen.height - (posPadre.y);

		frameMarginLeft = posPadre.x + scalePadre.x * oParent.GetComponent<Renderer>().bounds.size.x;
		frameMarginTop = posPadre.y - scalePadre.y * oParent.GetComponent<Renderer>().bounds.size.y * 2 - 20;

		//Rect rect = new Rect (posPadre.x + (float)10, posPadre.y - (float)35, (float)-30, (float)0.15);
		Rect rect = new Rect (frameMarginLeft, frameMarginTop,  frameWidth, frameHeight);
		GUI.DrawTexture (rect, frameTexture, ScaleMode.ScaleToFit, true, 0);

		Rect Hrect = new Rect(frameMarginLeft, frameMarginTop, healthWidth, frameHeight);
		GUI.DrawTexture (Hrect, HBTexture, ScaleMode.ScaleAndCrop, true, 0);
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
