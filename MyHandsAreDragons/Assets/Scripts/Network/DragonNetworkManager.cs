using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragonNetworkManager : NetworkManager
{
    public static DragonNetworkManager instance = null;
    public bool EnableDebug = false;

    private bool isDedicatedServer;

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
        // Get information from PlayerPrefs about settings from previous scene "Intro"
        // This info is generated either from the ui on that screen or by the config txt files

        int isServer = PlayerPrefs.GetInt("isServer");

        if (EnableDebug)
            Debug.Log("ipAddress: " + PlayerPrefs.GetString("ipAddress"));

        networkAddress = PlayerPrefs.GetString("ipAddress");

        int tempInt = 0;
        int.TryParse(PlayerPrefs.GetString("port"), out tempInt);
        networkPort = tempInt;

        if (EnableDebug)
            Debug.Log("port: " + networkPort);

        if (isServer == 0)
        {
            StartClient();
        }
        else if (isServer == 1)
        {
            // Start server or host depending on if running tests or having a dedicated server
            if (PlayerPrefs.GetInt("playMode") == 2)
            {

                StartServer();

                isDedicatedServer = true;

                if (EnableDebug)
                    Debug.Log("Starting SERVER");

            }
            else
            {
                StartHost();

                if (EnableDebug)
                    Debug.Log("Starting HOST");
            }
        }
    }
}
