using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveData : MonoBehaviour {

	public static float moveTime;
	public float cap;
	public float floor;
	public float baseAdjust;
	public int numNodes;
	public static float maxDev;
	public static List<float> nodePos;
	public static List<float> reverseNodePos;
	public static List<float> FFTdata;
	public static float motionTimer;
	public static bool updateVel;

	public static WaveData Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	void Start () {

		maxDev = cap / baseAdjust;
		moveTime = 1.0f;
		motionTimer = 0.0f;
		nodePos = new List<float>();
		reverseNodePos = new List<float> ();
		FFTdata = new List<float>();
		for (int i = 0; i < numNodes; ++i) {
			nodePos.Add (0.0f);
			reverseNodePos.Add (0.0f);
				}
		for (int i = 0; i < 5; ++i) {
			FFTdata.Add (0.0f);
				}
	}

	void Update () {

		motionTimer += Time.deltaTime;
		//Placeholder FFT data generator.
		for (int i = 0; i < 5; ++i) {
			FFTdata[i] = Random.Range (0, 10);
				}
		//Convert raw data to vertical wave displacement
		if (motionTimer > moveTime) {

			updateVel = true;
			motionTimer -= moveTime;

			//Bind the new sample in the max range and scale for displacement.
			float newPos = OpenBCI_FileReader.Inst.currentData;
			if(newPos > cap || newPos < floor)
			{
				newPos = (newPos > cap) ? cap : floor;
			}
			newPos /= baseAdjust;

			nodePos.RemoveAt (numNodes - 1);
			nodePos.Insert (0, newPos);
			reverseNodePos.RemoveAt (0);
			reverseNodePos.Insert (numNodes - 1, newPos);

				} 

		else {

			updateVel = false;

				}
	}
}
