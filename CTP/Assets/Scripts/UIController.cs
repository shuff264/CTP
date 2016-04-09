using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public static UIController instance;

	public int placeType = 3;

	public GameObject noneButton;
	public GameObject grassButton;
	public GameObject roadButton;
	public GameObject lightsButton;
	public GameObject dijkstraButton;
	public GameObject astarButton;
	
	Button buttonN;
	Button buttonG;
	Button buttonR;
	Button buttonL;
	Button buttonD;
	Button buttonA;
	
	public Slider spawnRateSlider;
	public Slider gameSpeedSlider;


	// Use this for initialization
	void Start () {
		instance = this;

		buttonG = grassButton.GetComponent<Button>();
		buttonR = roadButton.GetComponent<Button>();
		buttonL = lightsButton.GetComponent<Button>();
		buttonD = dijkstraButton.GetComponent<Button>();
		buttonA = astarButton.GetComponent<Button>();
		buttonN = noneButton.GetComponent<Button>();
		
		buttonN.interactable = false;
		buttonG.interactable = true;
		buttonR.interactable = true;
		buttonL.interactable = true;

		buttonD.interactable = false;
		buttonA.interactable = true;

		spawnRateSlider.value = 100.0f;
		gameSpeedSlider.value = 1.0f;

	}

	// Update is called once per frame
	void Update () {
		Time.timeScale = gameSpeedSlider.value;

	}

	public void OnClickNone(){
		placeType = 3;
		buttonN.interactable = false;
		buttonG.interactable = true;
		buttonR.interactable = true;
		buttonL.interactable = true;
	}

	public void OnClickGrass(){
		placeType = 0;
		buttonN.interactable = true;
		buttonG.interactable = false;
		buttonR.interactable = true;
		buttonL.interactable = true;
	}

	public void OnClickRoad(){
		placeType = 1;
		buttonN.interactable = true;
		buttonG.interactable = true;
		buttonR.interactable = false;
		buttonL.interactable = true;
	}

	public void OnClickLights(){
		placeType = 2;
		buttonN.interactable = true;
		buttonG.interactable = true;
		buttonR.interactable = true;
		buttonL.interactable = false;
	}

	public void OnClickDijkstra(){
		TileMap.instance.searchType = SearchTypes.Dijkstra;
		buttonD.interactable = false;
		buttonA.interactable = true;
	}

	public void OnClickAStar(){
		TileMap.instance.searchType = SearchTypes.AStar;		
		buttonD.interactable = true;
		buttonA.interactable = false;
	}

}
