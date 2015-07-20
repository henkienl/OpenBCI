using UnityEngine;
using System.Collections;

public class Highscore : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

		if (PlayerPrefs.GetInt ("newscore") == 0)
			gameObject.GetComponent<GUIText>().text = "highscore : " + PlayerPrefs.GetInt ("highscore");
				
		else 
		{
			gameObject.GetComponent<GUIText>().text = "new highscore : " + PlayerPrefs.GetInt ("highscore") + "\n"
				+ "old highscore : " + PlayerPrefs.GetInt ("displayscore");
		}

		PlayerPrefs.SetInt ("displayscore", PlayerPrefs.GetInt ("highscore"));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
