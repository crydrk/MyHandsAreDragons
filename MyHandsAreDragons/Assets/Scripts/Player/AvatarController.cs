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
            SetPlayerTransforms();
        }
        else
        {
            GetPlayerTransforms();
        }
    }
    
    private void SetPlayerTransforms()
    {
        // Setting the SyncVar values of the transforms to send across the network
        // TODO: Confirm if this is necessary: I'm checking the value to make sure it's different before assigning - the
        // documentation for SyncVar says setting the value for it will make it dirty, so that it will be marked to be sent
        // over the network. I'm thinking it's more efficient to make sure that the value has changed here rather than
        // constantly sending everything over the network. This also begs the question of if I assign the same value, does
        // it still get marked as dirty?

        // Head sync
        if (headPos != Head.position)
        {
            headPos = Head.position;
        }

        if (headRot != Head.rotation)
        {
            headRot = Head.rotation;
        }

        // Left hand sync
        if (leftHandPos != HandLeft.position)
        {
            leftHandPos = HandLeft.position;
        }

        if (leftHandRot != HandLeft.rotation)
        {
            leftHandRot = HandLeft.rotation;
        }

        // Right hand sync
        if (rightHandPos != HandRight.position)
        {
            rightHandPos = HandRight.position;
        }

        if (rightHandRot != HandRight.rotation)
        {
            rightHandRot = HandRight.rotation;
        }
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
