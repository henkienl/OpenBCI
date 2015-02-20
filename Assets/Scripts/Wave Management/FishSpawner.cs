using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour {

	public float spawnTime;
	public GameObject fish;

	private float spawnTimer;
	private List<GameObject> activeFish;
	private List<bool> visFish;
	private List<float> speed;
	private float[] xCoords;
	public float[] yCoords;
	private float[] zCoords;
	private bool deletingFish;

	int deleteFish;

	// Use this for initialization
	void Start () {

		activeFish = new List<GameObject> ();
		visFish = new List<bool> ();
		speed = new List<float> ();

		xCoords = new float[6];
		for (int i = 0; i < 6; ++i) 
		{
			xCoords[i] = (i % 2 == 0) ? (WaveCreator.Inst.edge[i / 2]) : 
				(WaveCreator.Inst.edge[i / 2] + WaveData.Inst.numNodes * WaveCreator.Inst.width[i / 2]);
		}

		zCoords = new float[3];
		for(int i = 0; i < 3; ++i)
			zCoords [i] = WaveCreator.Inst.space [i];

		spawnTimer = 0.0f;
		SpawnFish ();
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.deltaTime;
		if (spawnTimer > spawnTime && activeFish.Count < 6) 
		{
			spawnTimer -= spawnTime;
			SpawnFish();
		}
		deletingFish = false;
		for (int i = 0; i < activeFish.Count; ++i) 
		{
			if(activeFish[i].renderer.isVisible)
				visFish[i] = true;
			else if(visFish[i])
			{
				deleteFish = i;
				deletingFish = true;
			}
			activeFish[i].transform.position += new Vector3(speed[i] * Time.deltaTime, 0.0f, 0.0f);

		}
		if(deletingFish)
		{
			Destroy (activeFish[deleteFish]);
			activeFish.RemoveAt (deleteFish);
			speed.RemoveAt (deleteFish);
			visFish.RemoveAt (deleteFish);
		}
	
	}

	void SpawnFish()
	{
		int p = Random.Range (0, 1000) % 3;
		int o = Random.Range (0, 1000) % 2;
		float x = xCoords [p * 2 + o];
		float y = Random.Range (yCoords [p * 2], yCoords [p * 2 + 1]);
		float z = zCoords [p];

		GameObject newFish = (GameObject) Instantiate (fish, new Vector3 (x, y, z), Quaternion.identity);

		visFish.Add (false);

		int s = (o != 0) ? -1 : 1;
		Vector3 temp = newFish.transform.localScale;
		temp.x *= -1 * s;
		newFish.transform.localScale = temp;

		speed.Add (s * Random.Range (5, 10));

		activeFish.Add (newFish);
	}
}
