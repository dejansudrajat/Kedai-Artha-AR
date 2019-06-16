using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	public bool displayObjek = false;
	public Toggle displayObjekToggle;
	public GameObject Tampil;
	RaycastHit hit;
	//, Tampil1;

	public void Start(){
		displayObjekToggle.isOn = displayObjek;
		Tampil.SetActive (displayObjek);
		//Tampil1.SetActive (displayObjek);
	}

	public void Update(){
		
	}

	public void OnDisplayObjekToggleValueChanged(){
		if (displayObjekToggle.isOn) {
			displayObjek = true;
		} else {
			displayObjek = false;
		}
		Tampil.SetActive (displayObjek);
		//Tampil1.SetActive (displayObjek);

	}

}

	// Use this for initialization

	/*
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
*/