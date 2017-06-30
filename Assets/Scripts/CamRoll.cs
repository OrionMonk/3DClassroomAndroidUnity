/*

=================== HEADER INFORMATION  ========================

Modification History:
1. 25/6/2017

Authors:
1. NayanJyoti Kakati

Objective:
To allow free camera view for a full 3D experience

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRoll : MonoBehaviour {

	Vector3 end_pos, start_pos; // 3d position of the starting and ending of the one finger swipe
	float end_t, start_t, maxTime; // the ending time and the starting time of the one finger swipe
	bool one_finger; // check if only one finger touched
	float rotate_rate; // rate of rotation

	// Use this for initialization
	void Start () {
		one_finger = false; // at the start no finger is touched
		maxTime = 1.0f; //  max time
		rotate_rate = 0.4f; // rate of rotation set
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1){// if only one finger touched
			Touch finger = Input.GetTouch (0); // get first finger touch

			if (finger.phase == TouchPhase.Began) {// if touched just now
				start_t = Time.time;// start time of swipe recorded
				start_pos = finger.position;// 3d start position recorded
				one_finger = true; // only one finger touched bool set to true
			}
			if(one_finger == true){ // if double touch is found then swipe condition is neglected
				end_t = Time.time; // record end time of swipe
				end_pos = finger.position; // record end position 3d of swipe

				if (end_t - start_t < maxTime) { // if time gap of swipe is valid
					transform.Rotate (new Vector3((-end_pos.y+start_pos.y)*rotate_rate,0, 0));// rotation done with proper calculation in 3d perspective
				}
				start_t = Time.time; // start time set again when rotated
				start_pos = finger.position; // start position set again  when rotated
			}
		}
	}
}
