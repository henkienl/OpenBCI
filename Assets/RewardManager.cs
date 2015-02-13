using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour {

	float spawnTimer;
	public BubbleScript bubble;

	public static List<BubbleScript> rewards;
	private List<int> zCoords;
	private List<int> yCoords;

	// Use this for initialization
	void Start () {
		rewards = new List<BubbleScript> ();
		zCoords = new List<int> ();
		yCoords = new List<int> ();
		zCoords.Add (-10);
		zCoords.Add (0);
		zCoords.Add (20);
		yCoords.Add (10);
		yCoords.Add (20);
		yCoords.Add (30);
		yCoords.Add (50);
		yCoords.Add (50);
		yCoords.Add (60);
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if (spawnTimer > 0.5f) {
			spawnTimer -= 2.0f;
			if (rewards.Count > 10) {
				if(rewards[0] != null){
					Destroy (rewards[0].gameObject);
				}
				rewards.RemoveAt (0);
				}
			int p = Random.Range (0, 1000) % 3;
			float x = Random.Range (-40, 20);
			float y = Random.Range (yCoords[p*2], yCoords[p*2 + 1]);
			float z = zCoords[p];

			BubbleScript reward = (BubbleScript) Instantiate (bubble, new Vector3 (x, y, z), Quaternion.identity);
			reward.Gen ();
			rewards.Add (reward);
		}
		for(int i = 0; i < rewards.Count; ++i)
		{
			rewards[i].rewardIndex = i;
		}
	}
}
