using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
		public enum BattingResult{
				OUT,
				HIT,
				HIT2,
				HIT3,
				HOMERUN,
				FOUL,
				STRIKE,
				BALL,
				WAIT,
				CHANGE,
		}
		public GameObject[] baseColor;
		public GameObject[] strikes;
		public GameObject[] balls;
		public GameObject[] outs;
		public BattingResult battingResult;
		public int outCount;
		public int strikeCount;
		public int ballCount;
		public int point;
		int highScore;
		public bool[] runner = new bool[3]; //0:first,1:second,2:third
		Pitcher pitcher;
		TextMesh score;
		GUIStyle battingResultStyle;
		GUIStyle scoreStyle;
		string[] batting = {"OUT","HIT","2BHIT","3BHIT","HOMERUN","FOUL","STRIKE","BALL"};

		void Awake(){
				CameraFade.StartAlphaFade (Color.black, true, 0.3f, 0);
		}
	// Use this for initialization
		void Start () {
				battingResultStyle = new GUIStyle ();
				battingResultStyle.fontSize = 60;
				battingResultStyle.normal.textColor = Color.cyan;

				scoreStyle = new GUIStyle ();
				scoreStyle.fontSize = 60;
				scoreStyle.normal.textColor = Color.yellow;

				score = GameObject.Find ("Score").GetComponent <TextMesh>();
				score.text = "Score: " + point.ToString (); 

				pitcher = GameObject.Find ("Pitcher").GetComponent<Pitcher> ();
				StartCoroutine ("Throw");

				outCount = 0;
				strikeCount = 0;
				ballCount = 0;
				point = 0;
				for(var i=0;i<3;i++){
					runner[i] = false;
				}

				highScore = PlayerPrefs.GetInt ("HighScore");
		}

		void Update(){
				if(battingResult == BattingResult.CHANGE && Input.GetMouseButtonDown(0)){
						CameraFade.StartAlphaFade (Color.black, false, 0.5f, 0, () => { Application.LoadLevel ("Title"); });
				}
		}

		public void BallCount(){
				for (var i = 0; i < 2; i++) {
						if (i < strikeCount) {
								strikes [i].renderer.material.color = Color.yellow;
						} else {
								strikes [i].renderer.material.color = Color.white;
						}
				}

				for (var i = 0; i < 3; i++) {
						if (i < ballCount) {
								balls [i].renderer.material.color = Color.green;
						} else {
								balls [i].renderer.material.color = Color.white;
						}
				}

				for (var i = 0; i < 2; i++) {
						if (i < outCount) {
								outs [i].renderer.material.color = Color.red;
						} else {
								outs [i].renderer.material.color = Color.white;
						}
				}
		}
				
		public void Onbase(){
				for (var i = 0; i < 3; i++) {
						if (runner [i]) {
								baseColor [i].renderer.material.color = Color.red;
						} else {
								baseColor [i].renderer.material.color = Color.white;
						}
				}
		} 

		public void HomeIn(){
				point++;
				score.text = "Score: " + point.ToString (); 
		}

		IEnumerator GameResult(){
				if (highScore < point) {
						highScore = point;
						PlayerPrefs.SetInt ("HighScore", highScore);
				}
				yield return new WaitForSeconds(1);
				battingResult = GameController.BattingResult.CHANGE;
		}

		IEnumerator Throw(){
				while (true) {
						battingResult = BattingResult.WAIT;
						yield return new WaitForSeconds (3f);
						pitcher.SendMessage ("Throw");
						yield return new WaitForSeconds (3f);
				}
		}
				
		void OnGUI(){
				int battingResultInt = (int)battingResult;
				switch (battingResult) {
				case BattingResult.WAIT:
						break;
				case BattingResult.CHANGE:
						GUI.Label (new Rect (Screen.width / 2 - ("Score: ".Length + point.ToString().Length)* battingResultStyle.fontSize / 4, Screen.height / 2 + battingResultStyle.fontSize / 2, 400, 120),"Score: " + point.ToString(), scoreStyle);
						GUI.Label (new Rect (Screen.width / 2 - ("HighScore: ".Length + highScore.ToString().Length)* battingResultStyle.fontSize / 4, Screen.height / 2 - battingResultStyle.fontSize * 3 / 2, 400, 120),"HighScore: " + highScore.ToString(), scoreStyle);
						break;
				default:
						GUI.Label (new Rect (Screen.width / 2 - batting [battingResultInt].Length * battingResultStyle.fontSize / 3, Screen.height / 2 - battingResultStyle.fontSize / 2, 400, 120), batting [battingResultInt], battingResultStyle);
						break;
				}
		}
}
