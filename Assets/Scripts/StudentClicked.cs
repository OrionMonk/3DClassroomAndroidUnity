using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentClicked : MonoBehaviour {
	public GameObject NameText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseDown(){
		NameText.GetComponent<Text> ().text = transform.name;
	}
}
