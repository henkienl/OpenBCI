using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour {

	public int scoreAmt;
	public float velAcc;
	public int layer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity += new Vector3(0.0f, velAcc, 0.0f) * Time.deltaTime;
		if (transform.position.y > WaveCreator.maxHeights [layer] + 4.0f) 
			velAcc += 0.02f;
		if (!this.renderer.isVisible) 
		{
			RewardManager.rewards.Remove (this);
			Destroy (this.gameObject);
		}
	}

	public void Gen () {
		float r = Random.Range (200, 255) / 255.0f;
		float g = Random.Range (200, 255) / 255.0f;
		float b = Random.Range (220, 255) / 255.0f;
		renderer.material.color = new Color (r, g, b);
		scoreAmt = Random.Range (5, 10);
		velAcc = Random.Range (5, 30) / 100.0f;
	}
}
