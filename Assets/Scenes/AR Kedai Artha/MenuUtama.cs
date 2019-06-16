using UnityEngine;
using System.Collections;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace MenuUtama
{
    public class MenuUtama : MonoBehaviour
    {
		public GameObject tampilkan, sembunyikan;
        // Use this for initialization
        void Start ()
        {
            
        }
        
        // Update is called once per frame
        void Update ()
        {


			if (Application.isPlaying) {
				
				if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Backspace)) {
					OnButtonKeluarClick ();
				
					//tampilkan.SetActive (false);
					//sembunyikan.SetActive (true);
					Debug.Log("tekan back muncul dialog");
				//ControlDialog ();
				}
			} 

        }
	
        public void OnDaftarMenuButtonClick ()
        {
            #if UNITY_5_3 || UNITY_5_3_OR_NEWER
            //SceneManager.LoadScene ("DaftarMenu");
			//SceneManager.LoadScene("MenuAR");
			SceneManager.LoadScene("MenuAR_backup");
			#else
            Application.LoadLevel ("DaftarMenu");
            #endif
        }
        
        public void OnShowPetunjukButtonClick ()
        {
            #if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene ("Petunjuk");
            #else
            Application.LoadLevel ("Petunjuk");
            #endif
        }
        
        public void OnShowTentangButtonClick ()
        {
            #if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene ("Tentang");
			//SceneManager.LoadScene("ShowLicense");
            #else
            Application.LoadLevel ("Tentang");
            #endif
        }

        public void OnButtonKeluarClick()
        {
			tampilkan.SetActive (true);
			sembunyikan.SetActive (false);

			//Application.Quit();
            //Debug.Log("Keluar Apllikasi");
        }
		public void Y(){
			Application.Quit();
			Debug.Log ("Keluar ye");
		}

		public void N(){
			tampilkan.SetActive (false);
			sembunyikan.SetActive (true);
			Debug.Log ("Batal");
		}


    }
}