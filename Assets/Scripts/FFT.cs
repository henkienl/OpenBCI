using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class OpenBCI_FFT : MonoBehaviour {

	Thread FftThread; // The seperate thread for FFT
	/*public static List<float> samples;
	public static List<float> spectrum;
	public int numSamples; //Should be 512 by default
	public float sampleRate; //Should be 1024 by default*/

	// Use this for initialization
	void Start () {
		FftThread = new Thread(new ThreadStart(FFT)); // Init the FFT thread
		FftThread.IsBackground = true; // Makes the thread close when the foreground application stops
		FftThread.Start(); // Start thread

		//Initialize the sample arrays
		/*for (int i = 0; i < numSamples; ++i) {
			samples.Add (0.0f);
			spectrum.Add (0.0f);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This is done within the FFT thread
	void FFT(){
		//Put some thread init stuff here
		while (true) { //The infinite loop for the new thread
			/*
			float sum = 0.0f;
			for(int i = 0; i < numSamples; ++i)
			{
				sum += samples[i] * samples[i];
			}
			*/

		}

	}
}
