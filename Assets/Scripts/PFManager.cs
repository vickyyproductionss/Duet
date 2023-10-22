using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFManager : MonoBehaviour
{
	private void Start()
	{
		Login();
	}
	private void Login()
	{
		string PlayerID = SystemInfo.deviceUniqueIdentifier;
		var request = new LoginWithCustomIDRequest
		{
			CustomId = PlayerID,
			CreateAccount = true // Creates an account if it doesn't exist
		};

		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
	}
	private void OnLoginSuccess(LoginResult result)
	{
		Debug.Log("Login success");
		PlayerPrefs.SetString("PF_ID", result.PlayFabId);
	}

	private void OnLoginFailure(PlayFabError error)
	{
		Debug.Log("Login failed");
	}
}
