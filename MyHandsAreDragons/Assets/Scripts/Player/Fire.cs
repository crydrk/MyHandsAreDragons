using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Fire : MonoBehaviour
{
    // Input stuff
    public SteamVR_Action_Boolean TriggerAction;

    public SteamVR_Input_Sources HandType;

    public HandController PlayerHand;

    /*
    private void OnEnable()
    {

        if (TriggerAction == null)
        {
            Debug.LogError("No plant action assigned");
            return;
        }

        TriggerAction.AddOnChangeListener(OnTriggerActionChange, HandType);
    }

    private void OnDisable()
    {
        if (TriggerAction != null)
            TriggerAction.RemoveOnChangeListener(OnTriggerActionChange, HandType);
    }

    private void OnTriggerActionChange(SteamVR_Action_In actionIn)
    {
        if (TriggerAction.GetStateDown(HandType))
        {
            OpenMouth();
        }
    }
    */

    private void Update()
    {
        if (SteamVR_Input.__actions_default_in_GrabPinch.GetStateDown(HandType))
        {
            //OpenMouth();
        }

        PlayerHand.SetJawRotationRaw(SteamVR_Input.__actions_default_in_Squeeze.GetAxis(HandType));
    }

    private void OpenMouth()
    {
        PlayerHand.Fire();
    }
}
