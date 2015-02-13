using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveData : MonoBehaviour {

	public static float moveTime;
	public static List<float> nodePos;
	public static List<float> FFTdata;
	public static float motionTimer;
	public static bool updateVel;

	void Start () {

		moveTime = 1.0f;
		motionTimer = 0.0f;
		nodePos = new List<float>();
		FFTdata = new List<float>();
		for (int i = 0; i < 100; ++i) {
			nodePos.Add (Random.Range (0, 250));
			nodePos[i] -= 125.0f;
				}
		for (int i = 0; i < 5; ++i) {
			FFTdata.Add (0.0f);
				}
	}

	void Update () {

		motionTimer += Time.deltaTime;
		for (int i = 0; i < 5; ++i) {
			FFTdata[i] = Random.Range (0, 10);
				}
		if (motionTimer > moveTime) {
			updateVel = true;
			motionTimer -= moveTime;
			float newPos = Random.Range (0, 250);
			newPos -= 125;
			nodePos.RemoveAt (99);
			nodePos.Insert (0, newPos);
				} 

		else {

			updateVel = false;

				}
	}
}
