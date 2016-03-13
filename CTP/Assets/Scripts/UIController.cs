using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : Singleton<UIController> {

	public int placeType = 0;

	public GameObject grassButton;
	public GameObject roadButton;
	public GameObject lightsButton;

	Button buttonG;
	Button buttonR;
	Button buttonL;

	ColorBlock cbG;
	ColorBlock cbR;
	ColorBlock cbL;	

	// Use this for initialization
	void Start () {
		buttonG = grassButton.GetComponent<Button>();
		buttonR = roadButton.GetComponent<Button>();
		buttonL = lightsButton.GetComponent<Button>();

		buttonG.interactable = false;
		buttonR.interactable = true;
		buttonL.interactable = true;
	}
//	Button b = rButton.GetComponent<Button>(); 
//	ColorBlock cb = b.colors;
//	cb.normalColor = Color.white;
//	b.colors = cb;

	// Update is called once per frame
	void Update () {
		if(placeType == 0){

		}else if(placeType == 1){

		}else if(placeType == 2){

		}
	}

	public void OnClickGrass(){
		placeType = 0;
		buttonG.interactable = false;
		buttonR.interactable = true;
		buttonL.interactable = true;
	}

	public void OnClickRoad(){
		placeType = 1;
		buttonG.interactable = true;
		buttonR.interactable = false;
		buttonL.interactable = true;
	}

	public void OnClickLights(){
		placeType = 2;
		buttonG.interactable = true;
		buttonR.interactable = true;
		buttonL.interactable = false;
	}
}
