using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {
		void StartButtonPressed(){
				Invoke ("GameStart",0.3f);
		}

		void Awake(){
				CameraFade.StartAlphaFade (Color.black, true, 2f, 0);
		}

		void GameStart(){
				CameraFade.StartAlphaFade(Color.black, false, 0.5f, 0, () => { Application.LoadLevel("Main"); });
		}
}
