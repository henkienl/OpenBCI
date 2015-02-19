﻿using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	public enum Stage{Move, Fall, Jump, WaveSwitch, CollectPearls,
		CollectAir, AirGauge, Quit};

	public float fadeTime;
	public float fadeInTime;
	public float msgTime;

	private Stage stage;
	private Color temp;
	private float fadeTimer;
	private float fadeInTimer;
	private float msgTimer;
	private bool advancing;
	private bool fadeIn;
	private int scoreCheck;
	private int airCheck;

	// Use this for initialization
	void Start () 
	{
		if (PlayerPrefs.GetInt ("tutorial") == 0) 
		{
			Destroy (gameObject);
			PlayerScript.tutorial = false;
		}
		else
		{
			ChangeStage (Stage.Move);
			fadeIn = true;
			PlayerScript.tutorial = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fadeIn)
			DoFadeIn ();

		switch (stage) 
		{

		case Stage.Move:

			if(Input.GetAxis ("Horizontal") != 0)
				advancing = true;

			break;

		case Stage.Jump:

			if(Input.GetButton ("Jump"))
				advancing = true;

			break;

		case Stage.Fall:
		case Stage.AirGauge:

			msgTimer += Time.deltaTime;

			if(msgTimer > msgTime)
				advancing = true;

			break;

		case Stage.WaveSwitch:

			if(Input.GetButton ("Shift Up")
			   || Input.GetButton ("Shift Down"))
				advancing = true;

			break;

		case Stage.CollectPearls:

			if(PlayerScript.score > scoreCheck)
				advancing = true;

			break;


		case Stage.CollectAir:

			if(PlayerScript.bubbles > airCheck)
				advancing = true;

			break;

		case Stage.Quit:
		default:

			PlayerPrefs.SetInt ("tutorial", 0);
			PlayerPrefs.Save ();
			PlayerScript.tutorial = false;
			PlayerScript.score = 0;
			Destroy (this.gameObject);

			break;
		}

		if(advancing && !fadeIn)
			Advancer ();
	}

	void ChangeStage(Stage newStage)
	{
		stage = newStage;
		advancing = false;
		fadeTimer = 0.0f;
		fadeInTimer = 0.0f;
		fadeIn = true;

		gameObject.guiText.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		switch (stage) 
		{

		case Stage.Move:

			gameObject.guiText.text = "use the a and d keys to move";
			break;

		case Stage.Jump:

			gameObject.guiText.text = "use the spacebar to jump";
			break;

		case Stage.Fall:

			msgTimer = 0.0f;
			gameObject.guiText.text = "don't get swept off the screen";
			break;

		case Stage.WaveSwitch:

			gameObject.guiText.text = "use the w and s keys to move between waves";
			break;

		case Stage.CollectPearls:

			scoreCheck = PlayerScript.score;
			gameObject.guiText.text = "collect pink pearls to increase your score";
			break;

		case Stage.CollectAir:

			airCheck = PlayerScript.bubbles;
			gameObject.guiText.text = "collect bubbles to preserve your air";
			break;

		case Stage.AirGauge:

			msgTimer = 0.0f;
			gameObject.guiText.text = "run out of air and it's game over";
			break;

		default:

			gameObject.guiText.text = "";
			break;
		}
	}

	void Advancer()
	{
		fadeTimer += Time.deltaTime;

		if(fadeTimer >= fadeTime)
		{
			fadeTimer = fadeTime;
			ChangeStage (stage + 1);
			return;
		}

		temp = gameObject.guiText.color;
		temp.a = (fadeTime - fadeTimer) / fadeTime;
		gameObject.guiText.color = temp;
	}

	void DoFadeIn()
	{
		fadeInTimer += Time.deltaTime;

		if(fadeInTimer > fadeInTime)
		{
			fadeInTimer = fadeInTime;
			fadeIn = false;
			if(fadeInTime == 0)
				return;
		}
		temp = gameObject.guiText.color;
		temp.a = fadeInTimer / fadeInTime;
		gameObject.guiText.color = temp;
	}
}
