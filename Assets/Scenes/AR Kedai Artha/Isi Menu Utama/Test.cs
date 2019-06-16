using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ARKedaiArtha
{
public class Test : MonoBehaviour {
		public GameObject des1, des2, des3, des4, des5, tidak,terdeteksi;
		public int timer1,timer2,timer3, timer4, timer5;
		public bool timer1s,timer2s,timer3s, timer4s, timer5s = false;
		public bool visibility = false;
	
	// Use this for initialization
	void Start () {
			
	}
	
		void LateUpdate(){
			if (visibility == true) {
				tidak.SetActive (false);
				terdeteksi.SetActive (true);
			} else {
				tidak.SetActive (true);
				terdeteksi.SetActive (false);			
			}

		}

	// Update is called once per frame
	void Update () {
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Backspace)) {
				SceneManager.LoadScene ("MenuUtama");
			}

			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				RaycastHit hit;

				if (Physics.Raycast (ray, out hit)) {
					Debug.Log (hit.transform.name);
					//if (hit.collider.tag == "Pertama") {
					if (hit.collider.tag == "Obj1") {
						des1.SetActive (true);
						des2.SetActive (false);
						des3.SetActive (false);
						des4.SetActive (false);
						des5.SetActive (false);
						timer1s = true;
						timer2s = false;
						timer3s = false;
						timer4s = false;
						timer5s = false;
					}
					if (hit.collider.tag == "Obj2") {
						des1.SetActive (false);
						des2.SetActive (true);
						des3.SetActive (false);
						des4.SetActive (false);
						des5.SetActive (false);
						timer1s = false;
						timer2s = true;
						timer3s = false;
						timer4s = false;
						timer5s = false;
					}
					if (hit.collider.tag == "Obj3") {
						des1.SetActive (false);
						des2.SetActive (false);
						des3.SetActive (true);
						des4.SetActive (false);
						des5.SetActive (false);
						timer1s = false;
						timer2s = false;
						timer3s = true;
						timer4s = false;
						timer5s = false;
					}
					if (hit.collider.tag == "Obj4") {
						des1.SetActive (false);
						des2.SetActive (false);
						des3.SetActive (false);
						des4.SetActive (true);
						des5.SetActive (false);
						timer1s = false;
						timer2s = false;
						timer3s = false;
						timer4s = true;
						timer5s = false;
					}
					if (hit.collider.tag == "Obj5") {
						des1.SetActive (false);
						des2.SetActive (false);
						des3.SetActive (false);
						des4.SetActive (false);
						des5.SetActive (true);
						timer1s = false;
						timer2s = false;
						timer3s = false;
						timer4s = false;
						timer5s = true;
					}
				} // akhir raycast hit
			} // akhir button down

			if (timer1s = true) {
				timer1 = timer1 + 1;
			}
			if (timer2s = true) {
				timer2 = timer2 + 1;
			}
			if (timer3s = true) {
				timer3 = timer3 + 1;
			}
			if (timer4s = true) {
				timer4 = timer4 + 1;
			}
			if (timer5s = true) {
				timer5 = timer5 + 1;
			}


			if (timer1 == 2000 || timer2==2000 || timer3==2000 || timer4==2000 || timer5 ==2000) {
				des1.SetActive (false);
				des2.SetActive (false);
				des3.SetActive (false);
				des4.SetActive (false);
				des5.SetActive (false);
				//Debug.Log ("satu");
				timer1 = 0;
				timer2 = 0;
				timer3 = 0;
				timer4 = 0;
				timer5 = 0;
				timer1s = false;
				timer2s = false;
				timer3s = false;
				timer4s = false;
				timer5s = false;

			}
			/*
			if (timer2 == 2000) {
				des1.SetActive (false);
				des2.SetActive (false);
				des3.SetActive (false);
				Debug.Log ("dua");
				timer1 = 0;
				timer2 = 0;
				timer3 = 0;
				timer1s = false;
				timer2s = false;
				timer3s = false;
			}
			if (timer3 == 3000) {
				des1.SetActive (false);
				des2.SetActive (false);
				des3.SetActive (false);
				Debug.Log ("satu");
				timer1 = 0;
				timer2 = 0;
				timer3 = 0;
				timer1s = false;
				timer2s = false;
				timer3s = false;
			}
			*/
		}
				

	public void onButtonBackClick ()
	{
		#if UNITY_5_3 || UNITY_5_3_OR_NEWER
		SceneManager.LoadScene ("MenuUtama");
		#else
		Application.LoadLevel ("Test");
		#endif
	}
}
}
