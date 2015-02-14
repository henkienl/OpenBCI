using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Color idleButton;
	public Color hoverButton;
	public Color pressButton;

	public static GUIManager Inst{ get; private set; }

	void Awake()
	{
		Inst = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
