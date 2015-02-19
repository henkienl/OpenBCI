using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour {

	public float spawnTime;
	private float spawnTimer;

	public float pearlSpawnTime;
	private float pearlSpawnTimer;

	public int airCap;
	public int pearlCap;

	public BubbleScript bubble;
	public GameObject pearl;

	public static List<BubbleScript> rewards;
	public static List<GameObject> pearls;
	private List<float> zCoords;
	private List<float> yCoords;

	public static RewardManager Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start () {
		rewards = new List<BubbleScript> ();
		pearls = new List<GameObject> ();
		zCoords = new List<float> ();
		yCoords = new List<float> ();
		for (int i = 0; i < 3; ++i) {
			zCoords.Add (WaveCreator.Inst.space[i]);
			yCoords.Add (WaveCreator.maxHeights[i]);
			yCoords.Add (WaveCreator.maxHeights[i] + 5.0f);
				}
	}
	
	// Update is called once per frame
	void Update () {

		spawnTimer += Time.deltaTime;
		pearlSpawnTimer += Time.deltaTime;

		if (spawnTimer > spawnTime) 
		{
			spawnTimer -= spawnTime;
			if (rewards.Count > airCap) 
			{
				if(rewards[0] != null)
					Destroy (rewards[0].gameObject);
				
				rewards.RemoveAt (0);
			}

			int p = Random.Range (0, 1000) % 3;
			float x = Random.Range (-40, 20);
			float y = WaveCreator.minHeights[p] - 2.0f;
			float z = zCoords[p];

			BubbleScript reward = (BubbleScript) Instantiate (bubble, new Vector3 (x, y, z), Quaternion.identity);
			reward.Gen ();
			rewards.Add (reward);
		}

		if (pearlSpawnTimer > pearlSpawnTime) 
		{
			pearlSpawnTimer -= pearlSpawnTime;
			if(pearls.Count > pearlCap)
			{
				if(pearls[0] != null)
					Destroy(pearls[0].gameObject);

				pearls.RemoveAt(0);
			}

			int p = Random.Range (0, 1000) % 3;
			float x = Random.Range (-40, 20);
			float y = Random.Range (yCoords[p*2], yCoords[p*2 + 1]);
			float z = zCoords[p];

			GameObject reward = (GameObject) Instantiate (pearl, new Vector3 (x, y, z), Quaternion.identity);
			pearls.Add (reward);
		}
	}
}
