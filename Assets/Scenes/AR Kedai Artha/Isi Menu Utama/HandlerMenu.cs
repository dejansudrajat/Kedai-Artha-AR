using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HandlerMenu : MonoBehaviour {

	public void Start(){


	}
	public void Update(){

		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Backspace)) {
			SceneManager.LoadScene ("MenuUtama");
		}
	}
	public void OnButtonKembaliClick()
	{
		SceneManager.LoadScene ("MenuUtama");

	}
}
