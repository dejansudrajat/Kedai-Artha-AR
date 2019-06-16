using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class HSController : MonoBehaviour {
	//private string secretKey = "mySecretKey";
	//public string addScoreURL = "http://localhost/ar_artha/addscore.php?";
	public string getDeskripsiURL ="http://kedaiartha.000webhostapp.com/display.php?";
	//public string getDeskripsiURL ="http://kedaiartha.000webhostapp.com/display?";

	public Text teks;
	public GameObject panel;
	public bool visibility = false;

	public void Cek(){
		GameObject []tg = GameObject.FindGameObjectsWithTag("kode");
		for(int i = 0; i < tg.Length; i++) {
			if (tg [i].GetComponent<Toggle> ().isOn == true) {
					StartCoroutine (GetDeskripsi (tg [i].name));
					Debug.Log (tg [i] + "aktif");
			} else {
				teks.text = "";
			}
		}
			
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Kiyaa");
		//teks.text = "";
		//StartCoroutine (GetScores (name));
	}
	void LateUpdate(){
		if (panel.activeInHierarchy == true) {
			panel.SetActive (true);
		} else {
			panel.SetActive (false);
			//GameObject []tg = GameObject.FindGameObjectsWithTag("kode");
			//for (int i = 0; i < tg.Length; i++) {
			//	tg [i].GetComponent<Toggle> ().isOn=false;
			//}
		}
		//Debug.Log ("Hih");
	}

	//remember to use StartCoroutine when calling this function !
	/*
	IEnumerator PostScores (string name, int desc){
		//this connect to a server side php script that will add the name and score to a MysSQL DB.
		//supply it witha string representing the players name and the player score.

		string hash = Md5Sum (name + desc + secretKey);


		string post_url = addScoreURL + "name=" + WWW.EscapeURL (name) + "&score=" + desc + "&hash=" + hash;

		//post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW (post_url);
		yield return hs_post;

		if (hs_post.error != null) {
			print ("There was an error posting the high score : " + hs_post.error);
		}
	}
	*/

	IEnumerator GetDeskripsi(string name){
		//statusText.text = "Loading Scores";

		/* edited
		string getURLMenu = getDeskripsiURL+ "kode=" + WWW.EscapeURL (name);
		WWW hs_get = new WWW (getURLMenu);
		yield return hs_get;
		*/
		string getURLMenu = getDeskripsiURL + "kode=" + WWW.EscapeURL (name);
		WWW hs_get = new WWW (getURLMenu);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("There was an error getting the high score : " + hs_get.error);
			Debug.Log (getURLMenu);
		} else {
			teks.text= hs_get.text;
			panel.SetActive (true);
			Debug.Log (getURLMenu);
		}

	}
	
	// Update is called once per frame
	/*
	public string Md5Sum(string strToEncrypt){
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToEncrypt);

		//encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash (bytes);

		//convert the ecrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString (hashBytes [i], 16).PadLeft (2, '0');
		}
		return hashString.PadLeft (32, '0');
	}
	*/
}
