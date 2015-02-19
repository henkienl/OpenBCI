using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float accRate;
	public float jumpForce;
	public float maxSpeed;
	public float maxJump;

	public float airLength;
	public float airHeight;
	public Texture2D bgAir;
	public Texture2D fgAir;
	public float airX;
	public float airY;

	public float airDrain;
	public float airDrainScale;
	public float maxAir;

	private Vector3 scale;

	private bool grounded;
	private bool jumping;
	private bool airJump;
	private int mod;
	private float airTime;
	private float rotVel;
	private float jumpTimer;
	private float currentAngle;

	private float transitTime;
	private bool transitioning;
	private bool up;
	private Vector3 transitVel;
	private Vector3 destCoords;
	private int layer;
	private float air;

	public static int score;
	public bool testResetHighscore;

	// Use this for initialization
	void Start () 
	{
		jumpTimer = 0.0f;
		transitTime = 1.0f;
		mod = 1;
		score = 0;
		currentAngle = 0.0f;
		PlayerPrefs.SetInt ("score", 0);
		air = maxAir;
		layer = 1;
		transitioning = false;
		grounded = false;
		jumping = true;
		scale = transform.localScale;
		if (testResetHighscore)
			PlayerPrefs.SetInt ("highscore", 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!transitioning && Input.GetAxis ("Horizontal") != 0) 
		{
			float dir = Input.GetAxis ("Horizontal");

			mod = (dir > 0) ? -1 : 1;
			transform.localScale = new Vector3(mod * scale.x, scale.y, scale.z);

			if(dir > 0 && rigidbody.velocity.x < maxSpeed)
				rigidbody.velocity += new Vector3(100.0f * Time.deltaTime, 0.0f, 0.0f);

			else if(dir < 0 && rigidbody.velocity.x > -maxSpeed)
				rigidbody.velocity -= new Vector3(100.0f * Time.deltaTime, 0.0f, 0.0f);
		}

		else if(grounded && Input.GetAxis("Horizontal") == 0)
		{
			transform.Translate (WaveCreator.Inst.speeds[layer] * Time.deltaTime, 0.0f, 0.0f);
		}

		if (!transitioning && Input.GetButton ("Jump")) 
		{
			if (!jumping && grounded)
			{
				jumping = true;
				airJump = false;
				currentAngle = 0.0f;
				jumpTimer = 0.0f;
				rotVel = 0.0f;
			}

			if (jumping) 
			{
				jumpTimer += Time.deltaTime;

				if (jumpTimer > 0.15f || rigidbody.velocity.y > maxJump) 
				{
					if(rigidbody.velocity.y > maxJump)
						rigidbody.velocity.Set (rigidbody.velocity.x, maxJump, 0.0f);
					jumpTimer = 0.0f;
					jumping = false;
					airTime = -1 * rigidbody.velocity.y / Physics.gravity.y;
					rotVel = currentAngle / airTime;
					airJump = true;
				}

				if (jumping) 
				{
					rigidbody.AddForce (0.0f, jumpForce, 0.0f);
					currentAngle += 5.0f;
					UpdateAngles ();
				}
					
			}
		}

		else if (jumping)
		{
			if(rigidbody.velocity.y > maxJump)
				rigidbody.velocity.Set (rigidbody.velocity.x, maxJump, 0.0f);
			jumpTimer = 0.0f;
			jumping = false;
			airTime = -1 * rigidbody.velocity.y / Physics.gravity.y;
			rotVel = currentAngle / airTime;
			airJump = true;
		}

		if(airJump)
		{
			jumpTimer += Time.deltaTime;
			if(jumpTimer < airTime)
			{
				currentAngle -= rotVel * Time.deltaTime;
				if(currentAngle < 0)
					currentAngle = 0;
				UpdateAngles ();
			}
			else if (currentAngle > -60)
			{
				currentAngle -= 1.0f;
				UpdateAngles ();
			}
		}

		if (!airJump && !jumping && currentAngle != 0) 
		{
			float temp = currentAngle;
			float change = (currentAngle < 0) ? (5.0f) : (-5.0f);
			currentAngle += change;
			if((currentAngle < 0) != (temp < 0))
				currentAngle = 0;
			UpdateAngles ();
		}

		if (!transitioning && Input.GetButtonDown ("Shift Up") && layer > 0) 
		{
			transitioning = true;
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
			up = true;
			--layer;
			destCoords = new Vector3(0.0f, WaveCreator.maxHeights[layer] + transform.localScale.y / 2.0f, WaveCreator.Inst.space[layer]);
			transitVel = (destCoords - transform.position)/transitTime;
		}

		else if (!transitioning && Input.GetButtonDown ("Shift Down") && layer < 2) 
		{
			transitioning = true;
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
			up = false;
			++layer;
			destCoords = new Vector3(0.0f, WaveCreator.maxHeights[layer] + transform.localScale.y / 2.0f, WaveCreator.Inst.space[layer]);
			transitVel = (destCoords - transform.position)/transitTime;
		}

		if (transitioning) 
		{
			if((up && transform.position.z > destCoords.z)
			   || (!up && transform.position.z < destCoords.z))
			{
				transitioning = false;
				rigidbody.useGravity = true;
				rigidbody.isKinematic = false;
				transform.Translate (0.0f, destCoords.y - transform.position.y, destCoords.z - transform.position.z);
			}

			else 
				transform.Translate (transitVel * Time.deltaTime);
		}

		AdjustAir (-(airDrain * Time.deltaTime));
	}

	void AdjustAir(float adj)
	{
		air += adj;

		if (air > maxAir)
			air = maxAir;

		else if (air < 0.0f)
			Application.LoadLevel ("GameOver");
	}

	void OnGUI()
	{
		GUI.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		GUI.BeginGroup (new Rect (airX, airY, airLength, airHeight));
		GUI.Box (new Rect (0, 0, airLength, airHeight), bgAir);
		GUI.BeginGroup (new Rect (0, 0, air / maxAir * airLength, airHeight));
		GUI.Box (new Rect (0, 0, airLength, airHeight), fgAir);
		GUI.EndGroup ();
		GUI.EndGroup ();
	}

	void OnCollisionEnter (Collision hit) 
	{
		if (!jumping && hit.gameObject.tag == "Ground") 
		{
			grounded = true;
			airJump = false;
		}
	}

	void OnCollisionExit (Collision hit) 
	{
		if(hit.gameObject.tag == "Ground")
			grounded = false;
	}

	void OnTriggerEnter(Collider hit)
	{

		if (hit.gameObject.tag == "Air") 
		{
			RewardManager.rewards.Remove (hit.gameObject.GetComponent<BubbleScript>());
			Destroy (hit.gameObject);
			AdjustAir (hit.gameObject.GetComponent<BubbleScript> ().scoreAmt);				
		} 

		else if (hit.gameObject.tag == "Reward")
		{
			RewardManager.pearls.Remove (hit.gameObject);
			Destroy (hit.transform.parent.gameObject);
			Destroy (hit.gameObject);
			++score;
			PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") + 1);
			PlayerPrefs.Save ();
			airDrain += airDrainScale;
			RewardManager.Inst.spawnTime *= (1.0f - airDrainScale/5.0f);
			RewardManager.Inst.pearlSpawnTime *= (1.0f - airDrainScale/5.0f);
		}

		else if (hit.gameObject.tag == "Death")
		{
			Destroy (this.gameObject);
		}
	}

	void OnDestroy()
	{
		if (score > PlayerPrefs.GetInt ("highscore")) 
		{
			PlayerPrefs.SetInt ("highscore", score);
			PlayerPrefs.SetInt ("newscore", 1);
		} 

		else
			PlayerPrefs.SetInt ("newscore", 0);

		PlayerPrefs.Save ();

		Application.LoadLevel ("GameOver");
	}

	void UpdateAngles()
	{
		Vector3 temp = transform.eulerAngles;
		if (currentAngle >= 0)
			temp.z = (mod < 0) ? (currentAngle) : (360.0f - currentAngle);
		else
			temp.z = (mod < 0) ? (360.0f + currentAngle) : (-currentAngle);
		transform.eulerAngles = temp;
	}


}
