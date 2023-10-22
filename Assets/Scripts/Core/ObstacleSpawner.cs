using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	int obstacleCount = 0;
	[SerializeField] GameObject[] _obstaclePrefab;

	private void Start()
	{
		obstacleCount = PlayerPrefs.GetInt("Highscore");
		StartCoroutine(SpawnObstacles());
	}
	float NextRoundInSeconds;
	IEnumerator SpawnObstacles()
	{
		yield return new WaitForSeconds(NextRoundInSeconds);
		List<int> topPosIndexes = new List<int>();
		topPosIndexes.Add(0);
		topPosIndexes.Add(1);
		topPosIndexes.Add(2);
		if (obstacleCount > 50)
		{
			int spawnTwoObstacles = Random.Range(0, 2);
			if (spawnTwoObstacles == 0)
			{
				NextRoundInSeconds = 1;
			}
			else
			{
				NextRoundInSeconds = 2;
			}
		}
		else
		{
			NextRoundInSeconds = 2;
		}
		int topPosIndex = topPosIndexes[Random.Range(0, topPosIndexes.Count)];
		topPosIndexes.Remove(topPosIndex);
		GameObject obstacle = Instantiate(_obstaclePrefab[Random.Range(0, _obstaclePrefab.Length)]);
		obstacle.GetComponent<PlaceObstaclesAtTop>()._placeIndex = topPosIndex;
		int rotate = Random.Range(0, 2);
		if (rotate == 1)
		{
			if (topPosIndex == 0)
			{
				obstacle.transform.rotation = Quaternion.Euler(0, 0, -45);
				obstacle.transform.localScale = new Vector3(4, 0.5f);
				obstacle.GetComponent<PlaceObstaclesAtTop>().xoffset = -1;
			}
			else if (topPosIndex == 2)
			{
				obstacle.transform.rotation = Quaternion.Euler(0, 0, 45);
				obstacle.transform.localScale = new Vector3(4, 0.5f);
				obstacle.GetComponent<PlaceObstaclesAtTop>().xoffset = 1;
			}
		}
		if (topPosIndex == 1)
		{
			obstacle.transform.localScale = new Vector3(1.5f,0.5f);
			int random = Random.Range(0, 2);
			if(random == 0)
			{
				obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else
			{
				obstacle.transform.rotation = Quaternion.Euler(0, 0, 90);
			}
		}
		obstacleCount++;
		Duet.instance._scoreText.text = obstacleCount.ToString();
		if (PlayerPrefs.GetInt("Highscore", 0) < obstacleCount)
		{
			PlayerPrefs.SetInt("Highscore", obstacleCount);
		}
		if (!Duet.instance.isGameOver)
		{
			StartCoroutine(SpawnObstacles());
		}
		else
		{
			GameObject[] allObs = GameObject.FindGameObjectsWithTag("Obstacle");
			foreach (GameObject obs in allObs)
			{
				obs.GetComponent<Rigidbody2D>().gravityScale = 0;
				obs.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
			//Send Highscore
		}
	}
}
