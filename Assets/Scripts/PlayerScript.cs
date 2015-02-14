using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float accRate;
	public float jumpForce;
	public float maxSpeed;
	private bool grounded;
	private bool jumping;
	private float jumpTimer;
	private float transitTime;
	private bool transitioning;
	private bool up;
	private Vector3 transitVel;
	private Vector3 destCoords;
	private int layer;
	private int score;

	// Use this for initialization
	void Start () {
		jumpTimer = 0.0f;
		transitTime = 1.0f;
		layer = 1;
		transitioning = false;
		grounded = false;
		jumping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0) {
			float dir = Input.GetAxis ("Horizontal");
			if((dir > 0 && rigidbody.velocity.x < maxSpeed)
			   || (dir < 0 && rigidbody.velocity.x > -maxSpeed)){
				rigidbody.AddForce (dir * accRate, 0.0f, 0.0f);
			}
		}
		if (Input.GetButton ("Jump")) {
			if (grounded) {
				jumping = true;
			}
			if (jumping) {
				jumpTimer += Time.deltaTime;
				if (jumpTimer > 0.3f) {
					jumpTimer = 0.0f;
					jumping = false;
				}
				if (jumping) {
					rigidbody.AddForce (0.0f, jumpForce, 0.0f);
				}
			}
		}
		else if (jumping) {
			jumping = false;
		}
		if (!transitioning && Input.GetButtonDown ("Shift Up")) {
			if(layer > 0){
				transitioning = true;
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
				up = true;
				--layer;
				destCoords = new Vector3(0.0f, WaveCreator.Inst.height[layer] + WaveCreator.Inst.scale[layer]/2 + transform.localScale.y/2 + 125.0f / WaveCreator.Inst.amps[layer], WaveCreator.Inst.space[layer]);
				transitVel = (destCoords - transform.position)/transitTime;
			}
		}
		else if (!transitioning && Input.GetButtonDown ("Shift Down")) {
			if(layer <  2){
				transitioning = true;
				rigidbody.useGravity = false;
				rigidbody.isKinematic = true;
				up = false;
				++layer;
				destCoords = new Vector3(0.0f, WaveCreator.Inst.height[layer] + WaveCreator.Inst.scale[layer]/2 + transform.localScale.y/2 + 125.0f/WaveCreator.Inst.amps[layer], WaveCreator.Inst.space[layer]);
				transitVel = (destCoords - transform.position)/transitTime;
			}
		}
		if (transitioning) {
			if((up && transform.position.z > destCoords.z)
			   || (!up && transform.position.z < destCoords.z)){
				transitioning = false;
				rigidbody.useGravity = true;
				rigidbody.isKinematic = false;
				transform.Translate (0.0f, destCoords.y - transform.position.y, destCoords.z - transform.position.z);
			}
			else {
				transform.Translate (transitVel * Time.deltaTime);
			}
		}
	}
	
	void OnCollisionEnter (Collision hit) {
		if (Input.GetAxis ("Horizontal") == 0) {
			transform.parent = hit.transform;
		} 
		else {
			transform.parent = null;
		}
			grounded = true;
		}
	void OnCollisionExit (Collision hit) {
			transform.parent = null;
			grounded = false;
		}

	void OnTriggerEnter(Collider hit){

			if (hit.gameObject.tag == "Reward") {
			RewardManager.rewards.RemoveAt (hit.gameObject.GetComponent<BubbleScript>().rewardIndex);
			Destroy(hit.gameObject);
			score += hit.gameObject.GetComponent<BubbleScript>().scoreAmt;
			print (score);
				}
		}



}
