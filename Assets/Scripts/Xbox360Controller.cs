using UnityEngine;
using System.Collections;

public class Xbox360Controller : MonoBehaviour
{
	// editor variable for setting the controller ID
	// default controller index is zero
	// valid range is 0 to 3, anything else will result in 
	//	the controller not being activated
	public int controllerID = 0;
	
	// internal ID, set in stone when the application begins
	private int ctrlID;
	
	
	// button identifiers
	// should be constant but for some bizarre reason 
	//	Unity doesn't allow it...
	public static int btnA	= Xbox360ControllerPlugin.BtnA();
	public static int btnB	= Xbox360ControllerPlugin.BtnB();
	public static int btnX	= Xbox360ControllerPlugin.BtnX();
	public static int btnY	= Xbox360ControllerPlugin.BtnY();
	public static int btnLeftBump	= Xbox360ControllerPlugin.BtnLeftBumper();
	public static int btnRightBump	= Xbox360ControllerPlugin.BtnRightBumper();
	public static int btnStart		= Xbox360ControllerPlugin.BtnStart();
	public static int btnBack		= Xbox360ControllerPlugin.BtnBack();
	public static int btnLeftJoy	= Xbox360ControllerPlugin.BtnLeftJoystick();
	public static int btnRightJoy	= Xbox360ControllerPlugin.BtnRightJoystick();
	public static int btnDUp	= Xbox360ControllerPlugin.BtnDUp();
	public static int btnDDown	= Xbox360ControllerPlugin.BtnDDown();
	public static int btnDLeft	= Xbox360ControllerPlugin.BtnDLeft();
	public static int btnDRight	= Xbox360ControllerPlugin.BtnDRight();
	
	
	// controller functions
//	public int GetControllerID();
//	public bool Reconnected();
//	public bool Disconnected();
//	public bool IsConnected();
//	public bool IsDisconnected();
//	public bool ButtonPressed(int btn);
//	public bool ButtonReleased(int btn);
//	public bool IsButtonDown(int btn);
//	public bool IsButtonUp(int btn);
//	public float LeftJoystickX();
//	public float LeftJoystickY();
//	public float RightJoystickX();
//	public float RightJoystickY();
//	public float LeftTrigger();
//	public float RightTrigger();
//	public float TriggerDifference();
//	public void SetRumble(float left, float right);
//	public void SetRumble(float val);
//	public void StopRumble();
	
	
	// built-in functions
	
	// application starts
	protected void Start()
	{
		// set controller ID permanently and activate controller
		ctrlID = controllerID;
		Xbox360ControllerPlugin.Enable(ctrlID);
		if (IsConnected())
			Debug.Log("X360 controller connected.");
		else
			Debug.Log("X360 controller not connected.");
	}
	
	// update
	protected void Update()
	{
		// update controller and output change in connection
		int result = Xbox360ControllerPlugin.Update(ctrlID);
		if (result < 0)
			Debug.Log("X360 controller disconnected.");
		else if (Reconnected())
			Debug.Log("X360 controller reconnected.");
	}
	
	// application ends
	protected void OnApplicationQuit()
	{
		// deactivate controller
		Xbox360ControllerPlugin.Disable(ctrlID);
	}
	
	
	// added functions
	
	// get the ID of this controller
	public int GetControllerID()
	{
		return ctrlID;
	}
	
	// get the connection state
	public bool Reconnected()
	{
		return Xbox360ControllerPlugin.Reconnected(ctrlID);
	}
	public bool Disconnected()
	{
		return Xbox360ControllerPlugin.Disconnected(ctrlID);
	}
	public bool IsConnected()
	{
		return Xbox360ControllerPlugin.IsConnected(ctrlID);
	}
	public bool IsDisconnected()
	{
		return Xbox360ControllerPlugin.IsDisconnected(ctrlID);
	}
	
	// get the states of the buttons, joysticks and triggers
	public bool ButtonPressed(int btn)
	{
		return Xbox360ControllerPlugin.ButtonPressed(ctrlID, btn);
	}
	public bool ButtonReleased(int btn)
	{
		return Xbox360ControllerPlugin.ButtonReleased(ctrlID, btn);
	}
	public bool IsButtonDown(int btn)
	{
		return Xbox360ControllerPlugin.IsButtonDown(ctrlID, btn);
	}
	public bool IsButtonUp(int btn)
	{
		return Xbox360ControllerPlugin.IsButtonUp(ctrlID, btn);
	}
	
	public float LeftJoystickX()
	{
		return Xbox360ControllerPlugin.LeftJoystickX(ctrlID);
	}
	public float LeftJoystickY()
	{
		return Xbox360ControllerPlugin.LeftJoystickY(ctrlID);
	}
	public float RightJoystickX()
	{
		return Xbox360ControllerPlugin.RightJoystickX(ctrlID);
	}
	public float RightJoystickY()
	{
		return Xbox360ControllerPlugin.RightJoystickY(ctrlID);
	}
	public float LeftTrigger()
	{
		return Xbox360ControllerPlugin.LeftTrigger(ctrlID);
	}
	public float RightTrigger()
	{
		return Xbox360ControllerPlugin.RightTrigger(ctrlID);
	}
	public float TriggerDifference()
	{
		return Xbox360ControllerPlugin.TriggerDifference(ctrlID);
	}
	
	// rumble
	public void SetRumble(float left, float right)
	{
		Xbox360ControllerPlugin.SetRumble(ctrlID, left, right);
	}
	public void SetRumblePan(float val)
	{
		Xbox360ControllerPlugin.SetRumblePan(ctrlID, val);
	}
	public void StopRumble()
	{
		Xbox360ControllerPlugin.StopRumble(ctrlID);
	}
}
