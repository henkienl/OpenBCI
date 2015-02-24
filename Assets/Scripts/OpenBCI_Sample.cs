﻿using UnityEngine;
using System.Collections;

public class OpenBCI_Sample{

	public short sampleId; // The id of the sample
	public int[] channelSample; // The data of all channels, either 8 or 16
	public short[] accelData; // The data of the accelerometer, 3 directions

	public OpenBCI_Sample(){ // Constructor with default values
		sampleId = -1;
		channelSample = new int[8];
		accelData = new short[3];
	}

	public OpenBCI_Sample(short sampleId, int[] channelSample, short[]accelData){ // Constructor with given values
		this.sampleId = sampleId;
		this.channelSample = channelSample;
		this.accelData = accelData;
	}
	public override string ToString(){ // Put sample into a readable string
		string returnString = "";
		returnString += sampleId.ToString ();
		for(int i=0; i<channelSample.Length; i++) {
			returnString += ", "+channelSample[i];		
		}
		for (int i=0; i<accelData.Length; i++) {
			returnString += ", "+accelData[i];
		}
		return returnString;
	}

}
