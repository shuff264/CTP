using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	//UI Controller class to handle all buttons and other elements as part of the UI

	public static UIController instance;

	public int placeType = 3;

	//Getting all the button objects
	public GameObject noneButton;
	public GameObject grassButton;
	public GameObject roadButton;
	public GameObject lightsButton;
	public GameObject dijkstraButton;
	public GameObject astarButton;

	public Text dijkstraText;
	public Text aStarText;

	int aStarCount = 0;
	int aStarSum = 0;
	int aStarAvg = 0;

	int dijkstraCount = 0;
	int dijkstraSum = 0;
	int dijkstraAvg = 0;

	//Getting the button component from the button objects
	Button buttonN;
	Button buttonG;
	Button buttonR;
	Button buttonL;
	Button buttonD;
	Button buttonA;

	//Getting the sliiders
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

		//Setting initial values
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
		//Update using the gameSPeedSlider to control the tick rate of the game
		Time.timeScale = gameSpeedSlider.value;
	}

	//OnClick functions for all of the buttons
	//Interactable changes each time to make it clear to the user what button is selected
	//Also makes that button un-useable to prevent any problems
	//Place type feeds into the tile map for placing objects
	public void OnClickNone(){
		placeType = 3;
		buttonN.interactable = false;
		buttonG.interactable = true;
		buttonR.interactable = true;
		buttonL.interactable = true;;
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

	public void UpdateNodeText(int nodes, SearchTypes type){

		switch (type){
		case SearchTypes.AStar:
			aStarCount++;
			aStarSum += nodes;
			aStarAvg = aStarSum/aStarCount;

			aStarText.text = "A* Average Nodes Tested: " + aStarAvg;
			break;
			
		case SearchTypes.Dijkstra:
			dijkstraCount++;
			dijkstraSum += nodes;
			dijkstraAvg = dijkstraSum/dijkstraCount;

			dijkstraText.text = "Dijkstra Average Nodes Tested: " + dijkstraAvg;
			break;
			
		default:
			Debug.Log("PROBLEM");
			break;
			
		}

	}

}
