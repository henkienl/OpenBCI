using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float accRate;
	public float jumpForce;
	public float maxSpeed;

	public float airLength;
	public float airHeight;
	public Texture2D bgAir;
	public Texture2D fgAir;
	public float airX;
	public float airY;

	public float airDrain;
	public float airDrainScale;
	public float maxAir;

	public Vector3 scale;

	private bool grounded;
	private bool jumping;
	private float jumpTimer;
	private float transitTime;
	private bool transitioning;
	private bool up;
	private Vector3 transitVel;
	private Vector3 destCoords;
	private int layer;
	private float air;
	private int score;

	// Use this for initialization
	void Start () 
	{
		jumpTimer = 0.0f;
		transitTime = 1.0f;
		score = 0;
		air = maxAir;
		layer = 1;
		transitioning = false;
		grounded = false;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!transitioning && Input.GetAxis ("Horizontal") != 0) 
		{
			transform.parent = null;
			float dir = Input.GetAxis ("Horizontal");
			int mod = (dir > 0) ? -1 : 1;

			transform.localScale = new Vector3(mod * scale.x, scale.y, scale.z);

			if(dir > 0 && rigidbody.velocity.x < maxSpeed)
				rigidbody.velocity += new Vector3(100.0f * Time.deltaTime, 0.0f, 0.0f);

			else if(dir < 0 && rigidbody.velocity.x > -maxSpeed)
				rigidbody.velocity -= new Vector3(100.0f * Time.deltaTime, 0.0f, 0.0f);
		}

		if (!transitioning && Input.GetButton ("Jump")) 
		{
			if (grounded) 
				jumping = true;

			if (jumping) 
			{
				jumpTimer += Time.deltaTime;

				if (jumpTimer > 0.15f) 
				{
					jumpTimer = 0.0f;
					jumping = false;
				}

				if (jumping) 
					rigidbody.AddForce (0.0f, jumpForce, 0.0f);
			}
		}

		else if (jumping)
			jumping = false;

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
		GUI.color = new Color (0f, 0f, 0f, 1f);
		GUI.Label (new Rect (airX + 8.0f, airY + airHeight, 
		                     100, 20), score.ToString () + " pearls");
	}

	void OnCollisionEnter (Collision hit) 
	{
		if (Input.GetAxis ("Horizontal") == 0) 
		{
			transform.parent = hit.transform;
		} 
		else 
		{
			transform.parent = null;
		}
		grounded = true;
	}

	void OnCollisionExit (Collision hit) 
	{
		transform.parent = null;
		grounded = false;
	}

	void OnTriggerEnter(Collider hit)
	{

		if (hit.gameObject.tag == "Air") 
		{
			RewardManager.rewards.Remove (hit.gameObject.GetComponent<BubbleScript>());
			Destroy (hit.gameObject);
			AdjustAir (hit.gameObject.GetComponent<BubbleScript> ().scoreAmt);
			print (air);
				
		} 

		else if (hit.gameObject.tag == "Reward")
		{
			RewardManager.pearls.Remove (hit.gameObject);
			Destroy (hit.gameObject);
			++score;
			airDrain += airDrainScale;
			RewardManager.Inst.spawnTime *= (1.0f - airDrainScale/5.0f);
			RewardManager.Inst.pearlSpawnTime *= (1.0f - airDrainScale/5.0f);
		}

		else if (hit.gameObject.tag == "Death")
			Destroy (this.gameObject);
	}

	void OnDestroy()
	{
		Application.LoadLevel ("GameOver");
	}


}
