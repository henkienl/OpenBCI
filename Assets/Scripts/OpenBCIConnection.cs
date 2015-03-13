using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class OpenBCIConnection : MonoBehaviour
{
	//Setup parameters to connect
	public OpenBCI_Sample sample = new OpenBCI_Sample(); // This is where the resulting sample will be written to every frame
	public int COM_Port = 3; // Define the COMport the OpenBCI is connected to
	private SerialPort sp;
	Thread myThread; // The seperate thread for handling the connection
	private byte[] sampleByteArray= new byte[34]; // The incoming data

	public static OpenBCIConnection Inst{get; private set;}
	public float currentData;

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start ()
	{
		sp = new SerialPort("COM"+COM_Port, 115200, Parity.None, 8, StopBits.One); // Initialize the connection
		OpenConnection(); // Make the connection
		myThread = new Thread(new ThreadStart(ReadIncomingData)); // Init the connection thread
		myThread.IsBackground = true; // Makes the thread close when the foreground application stops
		myThread.Start(); // Start thread
	}
	


	//Function connecting
	public void OpenConnection()
	{
		if (sp != null)
		{
			if (sp.IsOpen)
			{
				sp.Close();
				print("Closing port, because it was already open!");
			}
			else
			{
				sp.Open(); // opens the connection
				sp.ReadTimeout = int.MaxValue; // sets the timeout value before reporting error
				print("Port Opened!");
			}
		}
		else
		{
			if (sp.IsOpen)
			{
				print("Port is already open");
			}
			else
			{
				print("Port == null");
			}
		}
		
	}
	
	void OnApplicationQuit()
	{
		sp.Write("s"); // Stop the datastream
		sp.Close(); // Close the port
	}

	void Update()
	{
		currentData = sample.channelSample [1] - sample.channelSample [0];
		//print ("electrode: " + sample.channelSample [1]);
		//print ("reference: " + sample.channelSample [0]);
		//print ("difference: " + currentData);
	}

	void ReadIncomingData()
	{
		int byteCounter = 0; // A counter for the incoming bytes
		sp.Write("b"); // Start the binary datastream
		while(true)
		{
			//Read incoming data
			sampleByteArray[byteCounter] = (byte)sp.ReadByte();
			if(sampleByteArray[byteCounter]==0xA0 && byteCounter == 0){ // If the header byte is found, start counting
				byteCounter = 1;
			}
			else if(byteCounter == 32 && sampleByteArray[byteCounter] == 0xC0){ // If the footer byte has been found and the count is correct
				byteCounter=0; // Reset the counter
				byte[] tempByteArrayToShort = new byte[2]; // Convert the 2nd byte to sampleID (8bit unsigned)
				tempByteArrayToShort[0]=sampleByteArray [1];
				tempByteArrayToShort[1]=(byte)0x00;
				short sampleId = System.BitConverter.ToInt16(tempByteArrayToShort, 0); // The id of the sample
				float[] channelSample = new float[8]; // convert the 3rd to 26th byte to channeldata (24bit signed MSB first)
				for (int i = 0; i<8; i++) {
					byte[] tempByteArrayToInt = new byte[4];
					tempByteArrayToInt[0] = sampleByteArray[4+(i*3)];
					tempByteArrayToInt[1] = sampleByteArray[3+(i*3)];
					tempByteArrayToInt[2] = sampleByteArray[2+(i*3)];
					tempByteArrayToInt[3] = (byte)0x00;
					if ((tempByteArrayToInt[2]&0x80)>0)
					{
						tempByteArrayToInt[3]=(byte)0xFF;
					}
					channelSample[i]=(System.BitConverter.ToInt32(tempByteArrayToInt, 0))*0.02235f; //scale factor of 0.02235 microVolts per count
				}
				float[] accelData = new float[3]; // convert 27th to 32nd byte to accelerometer data (16bit signed MSB first)
				for (int i = 0; i<3; i++) {
					accelData[i]=(System.BitConverter.ToInt16(sampleByteArray, 26+(i*2)))*0.02235f; //scale factor of 0.02235 microVolts per count
				}
				sample = new OpenBCI_Sample (sampleId, channelSample, accelData); // Put result in new sample object
				//print (sample.ToString ());

			}
			else if(byteCounter > 32) byteCounter=0; // Footer not found, reset counter
			else if(byteCounter > 0) byteCounter++; // Count until footer is found
			
		}
	}
}