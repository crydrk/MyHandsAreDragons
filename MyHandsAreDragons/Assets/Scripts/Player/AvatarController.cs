using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Valve.VR;

public class AvatarController : NetworkBehaviour
{

    // Transforms for positions of objects to be moved on the network
    public Transform Head;
    public Transform HandLeft;
    public Transform HandRight;

    // Camera and VR stuff to disable if not the local player
    public Camera VRCamera;
    public SteamVR_Behaviour_Pose VRHandLeft;
    public SteamVR_Behaviour_Pose VRHandRight;

    private void Awake()
    {
        // If we are not the local player, disable all the VR stuff
        if (!isLocalPlayer)
        {
            VRCamera.enabled = false;
            VRHandLeft.enabled = false;
            VRHandRight.enabled = false;
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            Debug.Log("it's the local player");
        }
    }

}
