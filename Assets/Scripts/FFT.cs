using UnityEngine;
using System.Collections;
using System.Threading;

public class OpenBCI_FFT : MonoBehaviour {

	Thread FftThread; // The seperate thread for FFT

	// Use this for initialization
	void Start () {
		FftThread = new Thread(new ThreadStart(FFT)); // Init the FFT thread
		FftThread.IsBackground = true; // Makes the thread close when the foreground application stops
		FftThread.Start(); // Start thread
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//This is done within the FFT thread
	void FFT(){
		//Put some thread init stuff here
		while (true) { //The infinite loop for the new thread

		}

	}
}
