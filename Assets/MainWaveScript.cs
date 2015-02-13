using UnityEngine;
using System.Collections;

public class MainWaveScript : MonoBehaviour {

	public float xSpeed;
	public float edgeBound;
	public float edgeDest;
	public float ampScale;
	private float yVel;

	// Use this for initialization
	void Start () {
		yVel = 0;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (xSpeed * Time.deltaTime, yVel * Time.deltaTime, 0);
	}

	public void SetDest(float dest)
	{
		yVel = (dest - transform.position.y) / WaveData.moveTime;
	}

}
