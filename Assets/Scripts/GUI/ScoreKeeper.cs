﻿using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = PlayerScript.score.ToString ();
	}
}