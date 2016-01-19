﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileType {

	public string name;
	public GameObject tilePrefab;

	public float movementCost = 1;
	public bool movementAllowed = true;
}
