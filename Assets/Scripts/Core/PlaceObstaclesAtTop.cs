using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObstaclesAtTop : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	public int _placeIndex;
	public float xoffset = 0;
	void Start()
	{
		// Get the SpriteRenderer component of the GameObject
		spriteRenderer = GetComponent<SpriteRenderer>();

		if (spriteRenderer == null)
		{
			Debug.LogError("SpriteRenderer component not found.");
			return;
		}

		Vector2 spriteSize = spriteRenderer.bounds.size;

		float screenHeight = Camera.main.orthographicSize * 2;
		float screenWidth = screenHeight * Screen.width / Screen.height;

		Vector3 topRightPosition = new Vector3(screenWidth / 2 - spriteSize.x / 2 + xoffset, screenHeight / 2 + spriteSize.y, 0);

		Vector3 topLeftPosition = new Vector3(-screenWidth / 2 + spriteSize.x / 2 + xoffset, screenHeight / 2 + spriteSize.y, 0);

		Vector3 topCenterPosition = new Vector3(0, screenHeight / 2 + spriteSize.y, 0);

		if(_placeIndex == 0)
		{
			transform.position = topLeftPosition;
		}
		else if (_placeIndex == 1)
		{
			transform.position = topCenterPosition;
		}
		else
		{
			transform.position = topRightPosition;
		}
	}
}
