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

		cbG = buttonG.colors;
		cbR = buttonR.colors;
		cbL = buttonL.colors;
	}
//	Button b = rButton.GetComponent<Button>(); 
//	ColorBlock cb = b.colors;
//	cb.normalColor = Color.white;
//	b.colors = cb;

	// Update is called once per frame
	void Update () {
		if(placeType == 0){
			cbG.disabledColor = Color.yellow;
			cbR.normalColor = Color.white;
			cbL.normalColor = Color.white;

			buttonG.interactable = false;
			buttonR.interactable = true;
			buttonL.interactable = true;
		}else if(placeType == 1){
			cbG.normalColor = Color.white;
			cbR.disabledColor = Color.yellow;
			cbL.normalColor = Color.white;

			buttonG.interactable = true;
			buttonR.interactable = false;
			buttonL.interactable = true;
		}else if(placeType == 2){
			cbG.normalColor = Color.white;
			cbR.normalColor = Color.white;
			cbL.disabledColor = Color.yellow;

			buttonG.interactable = true;
			buttonR.interactable = true;
			buttonL.interactable = false;
		}
		grassButton.GetComponent<Button>().colors = cbG;
		roadButton.GetComponent<Button>().colors = cbR;
		lightsButton.GetComponent<Button>().colors = cbL;

	}

	public void OnClickGrass(){
		placeType = 0;
	}

	public void OnClickRoad(){
		placeType = 1;
	}

	public void OnClickLights(){
		placeType = 2;
	}
}
