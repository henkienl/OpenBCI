using UnityEngine;
using System.Collections;
using System.Threading;

public class OpenBCI_FileReader : MonoBehaviour {

	Thread fileReadThread; // Declare thread for file reading
	public OpenBCI_Sample sample; // This is where the resulting sample will be written to every frame
	public string fileLocation = "c:\\test\\test.txt"; // The file with samples to be read from
	public int skipFirstXSeconds = 0; // Skip the first amount of lines from the file, in seconds
	private float sampleRate = 250.0f;
	System.IO.StreamReader file; // Declare the filereader
	private string line; // Every frame a new line from the file

	public float currentData;
	public static OpenBCI_FileReader Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start () {
		file = new System.IO.StreamReader(fileLocation); // Initialize the filereadeer
		while ((line = file.ReadLine ())[0]=='%') { // Skip the first lines and read the samplerate
			if(line.Contains ("%Sample Rate = ")){
				string[] tempSubstrings = line.Split (' ');
				float.TryParse(tempSubstrings[3],out sampleRate);
				print (sampleRate);
			}
		}
		for (int i=0; i<skipFirstXSeconds*sampleRate; i++) { // Skip the first x amount of seconds from the file
			file.ReadLine();
		}
		fileReadThread = new Thread(new ThreadStart(FileEEGStream)); // Initialize thread for reading from file
		fileReadThread.Start (); // Start thread
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentData = sample.channelSample [1] - sample.channelSample [0];
		//print ("electrode: " + sample.channelSample [1]);
		//print ("referecne: " + sample.channelSample [0]);
		//print ("difference: " + currentData);
	}
	void FileEEGStream()
	{
		while (true) { // The new thread should be running all the time
			line = file.ReadLine (); // Read new line
			string[] tempSubstrings = line.Split (','); // Split into parts
			short sampleId; // The id of the sample
			short.TryParse (tempSubstrings [0], out sampleId);
			float[] channelSample = new float[8]; // convert to channeldata
			for (int i = 0; i<8; i++) {
					float.TryParse (tempSubstrings [i+1], out channelSample [i]);
			}
			float[] accelData = new float[3]; // convert to accelerometer data
			for (int i = 0; i<3; i++) {
					float.TryParse (tempSubstrings [i+9], out accelData [i]);
			}
			sample = new OpenBCI_Sample (sampleId, channelSample, accelData); // Put result in new sample object
			if(sampleRate>1f)Thread.Sleep ((int)(1000/sampleRate)); // Wait to stream according to samplerate
		}
	}

	OpenBCI_Sample GetSample()
	{
		return sample;
	}
}
