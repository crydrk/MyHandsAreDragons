using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class NetworkUI : MonoBehaviour 
{
	// UI Buttons in the Intro scene
	public Dropdown ModeDropdown;
	public Dropdown LocationDropdown;
	public Text IPAddress;
	public Text Port;
	public Button StartButton;
	public Toggle ServerToggle;

	public bool ClientBuild = false;
	public bool ServerBuild = false;
	public string ClientIPOverride = "10.0.0.9";

	// List of IP addresses that correspond with the dropdown options
	public List<string> IPList = new List<string>();

	private void Start()
	{
		// If either of these tags are present in the Player settings, automatically starts with these
		// settings and skips the UI options

		#if CLIENT_BUILD
			PlayerPrefs.SetInt ("playMode", 0);
			ReadIpAddress ("IPConfig.txt");
			PlayerPrefs.SetString("ipAddress", ClientIPOverride);
			PlayerPrefs.SetString ("port", "7777");
			PlayerPrefs.SetInt ("isServer", 0);
			SceneManager.LoadScene ("MainScene");
			return;
		#endif

		#if SERVER_BUILD
			PlayerPrefs.SetInt ("playMode", 2);
			ReadIpAddress ("IPConfig.txt");
			PlayerPrefs.SetString("ipAddress", ClientIPOverride);
			PlayerPrefs.SetString ("port", "7777");
			PlayerPrefs.SetInt ("isServer", 1);
			SceneManager.LoadScene ("MainScene");
			return;
		#endif
	}

	public void StartGame()
	{
		// StartGame triggered by UI

		// Get mode and location
		// Location is based on the IP address list
		int mode = ModeDropdown.value;
		int location = LocationDropdown.value;

		int isServer = 0;

		PlayerPrefs.SetInt ("playMode", mode);

		// Logic for setting IP per location
		if (location < LocationDropdown.options.Count - 1)
		{
			PlayerPrefs.SetString("ipAddress", IPList[location]);
		}
		else if (location == LocationDropdown.options.Count - 1) // "Other" should always be the last value in the dropdown
		{
			if (IPAddress.text.Length < 1)
			{
				PlayerPrefs.SetString ("ipAddress", "127.0.0.1");
			}
			else
			{
				PlayerPrefs.SetString ("ipAddress", IPAddress.text);
			}
		}


		PlayerPrefs.SetString ("port", "7777");

        PlayerPrefs.SetInt ("isServer", isServer);

		SceneManager.LoadScene ("NewMainScene");
	}

	private void ReadIpAddress(string filename)
	{
		// A txt file called IPConfig.txt should sit next to the executable

		string line;
		try
		{
			System.IO.StreamReader file = new System.IO.StreamReader (filename);

			if ((line = file.ReadLine()) != null)
			{
				ClientIPOverride = line;
			}

			file.Close ();
		}
		catch (Exception e)
		{
			Debug.Log ("Error reading file");
			Debug.Log (e.Message);
		}
	}
}
