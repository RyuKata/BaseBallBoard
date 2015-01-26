using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
		GameController gameController;
		// Use this for initialization
		void Start () {
				gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		}

		void OnCollisionEnter(Collision collision){
				switch (collision.gameObject.tag) {
				case "Out":
						gameController.battingResult = GameController.BattingResult.OUT;
						if (gameController.outCount < 2) {
								gameController.outCount++;
								ResetCount ();
						} else {
								gameController.StopCoroutine ("Throw");
								gameController.StartCoroutine ("GameResult");
						}
						break;
				case "Hit1":
						gameController.battingResult = GameController.BattingResult.HIT;
						for (var i = 2; i >= 0; i--) {
								if (gameController.runner [i]) {
										if (i + 1 > 2) {
												gameController.HomeIn ();
												gameController.runner [i] = false;
										} else {
												gameController.runner [i + 1] = true;
												gameController.runner [i] = false;
										}
								}
						}
						gameController.runner [0] = true;
						ResetCount ();
						break;
				case "Hit2":
						gameController.battingResult = GameController.BattingResult.HIT2;
						for (var i = 2; i >= 0; i--) {
								if (gameController.runner [i]) {
										if (i + 2 > 2) {
												gameController.HomeIn ();
												gameController.runner [i] = false;
										} else {
												gameController.runner [i + 2] = true;
												gameController.runner [i] = false;
										}
								}
						}
						gameController.runner [1] = true;
						ResetCount ();
						break;
				case "Hit3":
						gameController.battingResult = GameController.BattingResult.HIT3;
						for (var i = 2; i >= 0; i--) {
								if (gameController.runner [i]) {
										gameController.HomeIn ();
										gameController.runner [i] = false;
								}
						}
						gameController.runner [2] = true;
						ResetCount ();
						break;
				case "Homerun":
						gameController.battingResult = GameController.BattingResult.HOMERUN;
						for (var i = 0; i < 3; i++) {
								if (gameController.runner [i]) {
										gameController.HomeIn ();
										gameController.runner [i] = false;
								}
						}
						gameController.HomeIn ();
						ResetCount ();
						break;
				case "Foul":
						gameController.battingResult = GameController.BattingResult.FOUL;
						if (gameController.strikeCount < 2) {
								gameController.strikeCount++;
						}
						break;
				case "Strike":
						gameController.battingResult = GameController.BattingResult.STRIKE;
						if (gameController.strikeCount < 2) {
								gameController.strikeCount++;
						} else {
								ResetCount ();
								if (gameController.outCount < 2) {
										gameController.battingResult = GameController.BattingResult.OUT;
										gameController.outCount++;
								} else {
										gameController.battingResult = GameController.BattingResult.OUT;
										gameController.StopCoroutine ("Throw");
										gameController.StartCoroutine ("GameResult");
								}
						}
						break;
				case "Ball":
						gameController.battingResult = GameController.BattingResult.BALL;
						if (gameController.ballCount < 3) {
								gameController.ballCount++;
						} else {
								ResetCount ();
								for (var i = 2; i >= 0; i--) {
										if (gameController.runner [i]) {
												int j;
												for (j = i; j >= 0; j--) {
														if (!gameController.runner [j])
																break;
												}
												if (j < 0) {
														if (i + 1 > 2) {
																gameController.runner [i] = false;
																gameController.HomeIn ();
														} else {
																gameController.runner [i + 1] = true;
																gameController.runner [i] = false;
														}
												}
										}
								}
								gameController.runner [0] = true;
						}
						break;
				default:
						return;
				}
				gameController.Onbase ();
				gameController.BallCount ();
				Destroy (gameObject);
		}

		void ResetCount(){
				gameController.strikeCount = 0;
				gameController.ballCount = 0;
		}
}
