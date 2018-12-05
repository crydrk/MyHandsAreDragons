using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class GameNetworkManager : NetworkManager 
{
    public static GameNetworkManager instance = null;
    public bool EnableDebug = false;

	// Voice chat prefab
	public GameObject VoiceChat;

	private bool isDedicatedServer;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
            Destroy(gameObject);
    }

    private void Start()
	{
		// TODO: Probably remove - we used to connect after a delay as a workaround for a different
		// problem. Seems we no longer need to.
		StartCoroutine (ConnectAfterDelay (0.0f));
	}

	public override NetworkClient StartHost (MatchInfo info)
	{
		this.OnStartHost ();
		this.matchInfo = info;

		return null;
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		if (isDedicatedServer)
			return;
		
		if (!this.clientLoadedScene) {
			ClientScene.Ready (conn);
			if (this.autoCreatePlayer) {
				ClientScene.AddPlayer (0);
			}
		}
	}
    
	private IEnumerator ConnectAfterDelay(float delay)
	{
		// Get information from PlayerPrefs about settings from previous scene "Intro"
		// This info is generated either from the ui on that screen or by the config txt files

		yield return new WaitForSeconds (delay);

		int isServer = PlayerPrefs.GetInt ("isServer");

        if (EnableDebug)
		    Debug.Log ("ipAddress: " + PlayerPrefs.GetString ("ipAddress"));

		networkAddress = PlayerPrefs.GetString ("ipAddress");

        if (EnableDebug)
            Debug.Log ("modified ipAddress: " + networkAddress);

		int tempInt = 0;
		int.TryParse(PlayerPrefs.GetString ("port"), out tempInt);
		networkPort = tempInt;

        if (EnableDebug)
            Debug.Log ("final port value: " + networkPort);

		if (isServer == 0)
		{
			StartClient ();
		}
		else if (isServer == 1)
		{
			// Start server or host depending on if running tests or having a dedicated server
			if (PlayerPrefs.GetInt ("playMode") == 2)
			{
				StartHost ();

				isDedicatedServer = true;

                if (EnableDebug)
                    Debug.Log("Starting SERVER");

            }
			else
			{
				StartHost ();

                if (EnableDebug)
                    Debug.Log("Starting HOST");
            }
		}

		// Instantiate the voice chat prefab after networking has begun
		GameObject.Instantiate(VoiceChat);
	}
}
