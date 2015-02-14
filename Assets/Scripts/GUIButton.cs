using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {

	public enum MenuAction{Play = 0, Menu, Quit};
	public MenuAction action;

	// Use this for initialization
	void Start () {
		gameObject.guiText.material.color = GUIManager.Inst.idleButton;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter()
	{
		gameObject.guiText.material.color = GUIManager.Inst.hoverButton;
	}

	void OnMouseExit()
	{
		gameObject.guiText.material.color = GUIManager.Inst.idleButton;
	}

	void OnMouseDown()
	{
		gameObject.guiText.material.color = GUIManager.Inst.pressButton;
	}

	void OnMouseUp()
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
		}
	}
}
