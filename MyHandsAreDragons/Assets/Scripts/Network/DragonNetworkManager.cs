using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragonNetworkManager : NetworkManager
{
    public static DragonNetworkManager instance = null;
    public bool EnableDebug = false;

    [HideInInspector]
    public bool isServer;

    private void Start()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
            Destroy(gameObject);

        Connect();
    }

    private void Connect()
    {
        // Get information from PlayerPrefs about settings from previous scene "NewStartScene"
        // This info is generated either from the ui on that screen or by the config txt files

        if (EnableDebug)
            Debug.Log("ipAddress: " + PlayerPrefs.GetString("ipAddress"));

        networkAddress = PlayerPrefs.GetString("ipAddress");

        int tempInt = 0;
        int.TryParse(PlayerPrefs.GetString("port"), out tempInt);
        networkPort = tempInt;

        if (EnableDebug)
            Debug.Log("port: " + networkPort);

        if (PlayerPrefs.GetInt("playMode") == 0)
        {
            if (EnableDebug)
                Debug.Log("Starting HOST");

            isServer = true;

            StartHost();
        }
        else if (PlayerPrefs.GetInt("playMode") == 1)
        {
            if (EnableDebug)
                Debug.Log("Starting SERVER");

            isServer = true;

            StartServer();
        }
        else if (PlayerPrefs.GetInt("playMode") == 2)
        {
            if (EnableDebug)
                Debug.Log("Starting CLIENT");

            isServer = false;

            StartClient();
        }

    }
}
