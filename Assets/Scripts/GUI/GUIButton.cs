using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	public enum MenuAction{Play = 0, Menu, Quit, Tutorial, ResetScore};
	public MenuAction action;

	public bool hoverFlag;
	public bool selectFlag;
	public bool pressing;

	// Use this for initialization
	void Start () {
		gameObject.guiText.material.color = GUIManager.Inst.idleButton;
		if (action == MenuAction.Tutorial) 
		{
			gameObject.guiText.text = (PlayerPrefs.GetInt ("tutorial") == 0) ?
				("tutorial disabled") : ("tutorial enabled");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		hoverFlag = true;
	}

	void OnMouseExit()
	{
		hoverFlag = false;
	}

	void OnMouseDown()
	{
		selectFlag = true;
	}

	void OnMouseUp()
	{
		pressing = true;
	}

	public void Active()
	{
		gameObject.guiText.material.color = GUIManager.Inst.hoverButton;
	}

	public void Inactive()
	{
		gameObject.guiText.material.color = GUIManager.Inst.idleButton;
	}

	public void Press()
	{
		gameObject.guiText.material.color = GUIManager.Inst.pressButton;
	}

	public void Select()
	{
		gameObject.guiText.material.color = GUIManager.Inst.hoverButton;
		switch (action) 
		{
		case MenuAction.Play:
			Application.LoadLevel ("Waves");
			break;
		case MenuAction.Menu:
			Application.LoadLevel ("MainMenu");

			break;
		case MenuAction.Quit:
			Application.Quit ();
			break;
		case MenuAction.Tutorial:
			PlayerPrefs.SetInt ("tutorial", (PlayerPrefs.GetInt ("tutorial") == 0) ? 1 : 0);
			PlayerPrefs.Save ();
			gameObject.guiText.text = (PlayerPrefs.GetInt ("tutorial") == 0) ? 
				("tutorial disabled") : ("tutorial enabled");
			break;
		case MenuAction.ResetScore:
			PlayerPrefs.SetInt ("highscore", 0);
			PlayerPrefs.Save ();
			break;
		}
	}
}
