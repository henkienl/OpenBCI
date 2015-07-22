using UnityEngine;
using System.Collections;

public class OpenBCI_FFT : MonoBehaviour {

	Thread myThread; // The seperate thread for FFT

	// Use this for initialization
	void Start () {
		myThread = new Thread(new ThreadStart(ReadIncomingData)); // Init the FFT thread
		myThread.IsBackground = true; // Makes the thread close when the foreground application stops
		myThread.Start(); // Start thread
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
