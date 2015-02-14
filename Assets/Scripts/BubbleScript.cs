using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour {

	public int rewardIndex;
	public int scoreAmt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Gen () {
		float r = Random.Range (0, 255) / 255.0f;
		float g = Random.Range (0, 255) / 255.0f;
		float b = Random.Range (0, 255) / 255.0f;
		renderer.material.color = new Color (r, g, b);
		scoreAmt = Random.Range (0, 10);
	}
}
