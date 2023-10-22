using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Duet : MonoBehaviour
{
	public static Duet instance;

	[SerializeField] GameObject _gameEndScreen;
	[SerializeField] float speed;
	public bool isGameOver;
	public TMP_Text _scoreText;
	
	private void Awake()
	{
		instance = this;
	}
	void FixedUpdate()
    {
        DetectTouches();
    }

	public void PlayAgainBtn()
	{
		SceneManager.LoadScene(0);
	}

	public void GameOver()
	{
		GameObject[] allObs = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (GameObject obs in allObs)
		{
			obs.GetComponent<Rigidbody2D>().gravityScale = 0;
			obs.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		isGameOver = true;
		_gameEndScreen.SetActive(true);
	}

    void DetectTouches()
    {
#if UNITY_ANDROID
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			
			// Check if the touch is on the left side of the screen
			if (touch.position.x < Screen.width / 2)
			{
				// Add your left-side touch handling code here
				RotateDuet(false);
			}

			// Check if the touch is on the right side of the screen
			if (touch.position.x >= Screen.width / 2)
			{
				// Add your right-side touch handling code here
				RotateDuet(true);
			}
		}
#endif
		float horizontal = Input.GetAxisRaw("Horizontal");
		if(horizontal > 0)
		{
			RotateDuet(true);
		}
		else if(horizontal < 0)
		{
			RotateDuet(false);
		}
	}
	void RotateDuet(bool toRight)
	{
		if (toRight)
		{
			float currentAngle = transform.rotation.eulerAngles.z;
			transform.Rotate(0,0,-1*speed);
		}
		else
		{
			float currentAngle = transform.rotation.eulerAngles.z;
			transform.Rotate(0,0,1*speed);
		}
	}
}