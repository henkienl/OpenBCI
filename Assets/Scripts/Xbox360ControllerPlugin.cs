using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Xbox360ControllerPlugin : MonoBehaviour
{
	// individual controller functionalities
	
	// enable and disable controller
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int Enable(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int Disable(int id);
	
	// update controller connection and buttons
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int Update(int id);
	
	// check connection
	// was previously not connected, now working
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool Reconnected(int id);
	// was previously connected, now not working
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool Disconnected(int id);
	// is connected right now
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool IsConnected(int id);
	// is not connected right now
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool IsDisconnected(int id);
	
	// check button
	// was previously up, now down
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool ButtonPressed(int id, int btn);
	// was previously down, now up
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool ButtonReleased(int id, int btn);
	// is down right now
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool IsButtonDown(int id, int btn);
	// is up right now
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern bool IsButtonUp(int id, int btn);
	
	// check joysticks as normalized values [-1, 1]
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float LeftJoystickX(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float LeftJoystickY(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float RightJoystickX(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float RightJoystickY(int id);
	
	// check triggers as normalized values [0, 1]
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float LeftTrigger(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float RightTrigger(int id);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern float TriggerDifference(int id);
	
	// activate and deactivate rumble
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void SetRumble(int id, float left, float right);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void SetRumblePan(int id, float val);
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void StopRumble(int id);
	
	
	// global functionalities affecting all controllers
	
	// enable and disable clamping of joystick and triggers
	// this determines whether or not the returned values from 
	//	the joystick and trigger checks take the dead zones 
	//	into account
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void EnableLeftJoystickClamp();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void EnableRightJoystickClamp();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void EnableTriggerClamp();
	
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void DisableLeftJoystickClamp();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void DisableRightJoystickClamp();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern void DisableTriggerClamp();
	
	// get button identifiers
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnA();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnB();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnX();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnY();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnLeftBumper();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnRightBumper();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnStart();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnBack();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnLeftJoystick();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnRightJoystick();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnDUp();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnDDown();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnDLeft();
	[DllImport ("Xbox360ControllerPlugin")]
	public static extern int BtnDRight();
}
