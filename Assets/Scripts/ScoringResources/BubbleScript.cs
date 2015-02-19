using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour {

	public int scoreAmt;
	public float velAcc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity += new Vector3(0.0f, velAcc, 0.0f) * Time.deltaTime;
		if (!this.renderer.isVisible)
			Destroy (this.gameObject);
	}

	public void Gen () {
		float r = Random.Range (200, 255) / 255.0f;
		float g = Random.Range (200, 255) / 255.0f;
		float b = Random.Range (220, 255) / 255.0f;
		renderer.material.color = new Color (r, g, b);
		scoreAmt = Random.Range (0, 10);
		velAcc = Random.Range (1, 5) / 10.0f;
	}
}
