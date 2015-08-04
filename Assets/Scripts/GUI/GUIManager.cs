﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	public Color idleButton;
	public Color hoverButton;
	public Color pressButton;

	public static int selected;
	int horControl;
	bool horRestrict;
	int verControl;
	bool verRestrict;
	bool waitFlag;
	public List<GUIButton> buttons;

	public static GUIManager Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start () 
	{
		selected = 0;
		horControl = 0;
		verControl = 0;
		horRestrict = false;
		verRestrict = false;
	}
	
	// Update is called once per frame
	void Update () {

		horControl = (int)Input.GetAxis ("Horizontal");
		verControl = (int)Input.GetAxis ("Vertical");

		if(horControl != 0 && !horRestrict)
		{
			selected += horControl;
			horRestrict = true;
		}
		else if (horControl == 0)
			horRestrict = false;

		if(verControl != 0 && !verRestrict)
		{
			selected -= verControl;
			verRestrict = true;
		}
		else if (verControl == 0)
			verRestrict = false;

		if (selected < 0)
			selected = buttons.Count - 1;
		else if (selected > buttons.Count - 1)
			selected = 0;

		for (int i = 0; i < buttons.Count; ++i) 
		{
			if(buttons[i].hoverFlag)
				selected = i;
		}

		for (int i = 0; i < buttons.Count; ++i) 
		{
			if(i == selected)
				buttons[i].Active ();
			else
				buttons[i].Inactive ();
		}
	}
}
