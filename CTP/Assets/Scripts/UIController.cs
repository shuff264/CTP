using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public static UIController instance;

	public int placeType = 0;

	public GameObject grassButton;
	public GameObject roadButton;
	public GameObject lightsButton;
	public GameObject dijkstraButton;
	public GameObject astarButton;
	

	Button buttonG;
	Button buttonR;
	Button buttonL;
	Button buttonD;
	Button buttonA;
	


	// Use this for initialization
	void Start () {
		instance = this;

		buttonG = grassButton.GetComponent<Button>();
		buttonR = roadButton.GetComponent<Button>();
		buttonL = lightsButton.GetComponent<Button>();
		buttonD = dijkstraButton.GetComponent<Button>();
		buttonA = astarButton.GetComponent<Button>();
		

		buttonG.interactable = false;
		buttonR.interactable = true;
		buttonL.interactable = true;

		buttonD.interactable = false;
		buttonA.interactable = true;

	}

	// Update is called once per frame
	void Update () {

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
