using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveCreator : MonoBehaviour {

	public MainWaveScript wave;
	public Material waveMat;

	public float[] width;
	public int[] numPoints;
	public float[] scale;
	public float[] edge;
	public float[] height;
	public float[] space;
	public float[] amps;
	public float[] speeds;
	public Color[] colours;

	private List<float> edgeBounds;
	private List<float> edgeDests;
	private List<bool> colourSettings;
	private List<MainWaveScript>[] waves;
	private bool addingEnd;
	private bool addingFront;
	
	public static WaveCreator Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start () {

		edgeBounds = new List<float> ();
		edgeDests = new List<float> ();
		colourSettings = new List<bool> ();

		waves = new List<MainWaveScript>[3];

		colourSettings.Add (false);
		colourSettings.Add (true);
		colourSettings.Add (false);

		//Initialize the waves.
		for (int i = 0; i < 3; ++i) {
			waves[i] = new List<MainWaveScript>();
			if(speeds[i] < 0){
				edgeBounds.Add (edge[i] - width[i]);
				edgeDests.Add (edge[i] + (numPoints[i] - 1) * width[i]);
			}
			else{
				edgeBounds.Add (edge[i] + (numPoints[i] - 1) * width[i]);
				edgeDests.Add (edge[i] - width[i]);
			}

			for (int j = 0; j < numPoints[i]; ++j) {

				MainWaveScript wavePart = (MainWaveScript) Instantiate (wave, new Vector3(edge[i] + j * width[i], height[i], space[i]), Quaternion.identity);

				wavePart.transform.localScale = new Vector3(width[i] + 0.001f, scale[i], 1.0f);
				wavePart.xSpeed = speeds[i];

				wavePart.renderer.material = waveMat;
				if(j % 2 == 0){
					wavePart.renderer.material.color = colours[i * 2];
				}
				else{
					wavePart.renderer.material.color = colours[i * 2 + 1];
				}

				waves[i].Add (wavePart);

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 3; ++i){

			if(speeds[i] < 0 && waves[i][0].transform.position.x < edgeBounds[i])
				addingEnd = true;

			else if (speeds[i] > 0 && waves[i][numPoints[i] - 1].transform.position.x > edgeBounds[i])
				addingFront = true;

			if(addingEnd || addingFront)
			{
				int addIndex = (addingEnd) ? numPoints[i] - 1 : 0;
				int destroyIndex = (addingEnd) ? 0 : numPoints[i] - 1;
				addingEnd = false;
				addingFront = false;

				Destroy (waves[i][destroyIndex].gameObject);
				waves[i].RemoveAt(destroyIndex);

				MainWaveScript wavePart = (MainWaveScript) Instantiate (wave, new Vector3(edgeDests[i], height[i], space[i]), Quaternion.identity);
				
				wavePart.transform.localScale = new Vector3(width[i] + 0.001f, scale[i], 1.0f);
				wavePart.xSpeed = speeds[i];
				
				wavePart.renderer.material = waveMat;
				if(colourSettings[i]){
					wavePart.renderer.material.color = colours[i * 2];
				}
				else{
					wavePart.renderer.material.color = colours[i * 2 + 1];
				}
				colourSettings[i] = !colourSettings[i];

				waves[i].Insert (addIndex, wavePart);
				waves[i][addIndex].SetDest(WaveData.nodePos[numPoints[i] - 1] / amps[i] + height[i]);
			}

			if(WaveData.updateVel){
				for(int j = 0; j < numPoints[i]; ++j){
					waves[i][j].SetDest(WaveData.nodePos[j] / amps[i] + height[i]);
				}
			}
		}
	}
}
