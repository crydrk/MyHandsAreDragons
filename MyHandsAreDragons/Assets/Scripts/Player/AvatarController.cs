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

    // SyncVars for transforms
    [SyncVar]
    private Vector3 headPos;
    [SyncVar]
    private Quaternion headRot;
    [SyncVar]
    private Vector3 leftHandPos;
    [SyncVar]
    private Quaternion leftHandRot;
    [SyncVar]
    private Vector3 rightHandPos;
    [SyncVar]
    private Quaternion rightHandRot;

    private void Start()
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
            CmdSyncPlayerTransforms(Head.position, Head.rotation, HandLeft.position, HandLeft.rotation, HandRight.position, HandRight.rotation);
        }
        else
        {
            GetPlayerTransforms();
        }
    }

    [Command]
    private void CmdSyncPlayerTransforms(Vector3 aHeadPos, Quaternion aHeadRot, Vector3 aLeftHandPos, Quaternion aLeftHandRot, Vector3 aRightHandPos, Quaternion aRightHandRot)
    {
        // Setting the SyncVar values of the transforms to send across the network
        headPos = aHeadPos;
        headRot = aHeadRot;
        leftHandPos = aLeftHandPos;
        leftHandRot = aLeftHandRot;
        rightHandPos = aRightHandPos;
        rightHandRot = aRightHandRot;
    }

    private void GetPlayerTransforms()
    {
        // Set the transforms from their respective syncVars
        Head.position = headPos;
        Head.rotation = headRot;
        HandLeft.position = leftHandPos;
        HandLeft.rotation = leftHandRot;
        HandRight.position = rightHandPos;
        HandRight.rotation = rightHandRot;
    }

}
