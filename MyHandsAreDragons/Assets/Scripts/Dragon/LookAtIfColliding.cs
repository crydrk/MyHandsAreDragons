using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class LookAtIfColliding : MonoBehaviour 
{
	public bool EnableLook = true;

    // Can identify a number of strings for objects to look at including players' heads and other dragons
    public List<string> StringsToLookFor = new List<string>();

	public Transform CurrentTarget;
	public Transform NeutralTarget; // An object placed straight ahead
	public Transform IntermediateTarget; // An object that lerps to the target position for smooth switching
	public float TargetChangeSpeed = 5f; // The speed at which to lerp IntermediateTarget
	public bool ResetLookOverTime = false; // Reset over time option as a safety net for if OnTriggerExit fails
    public Transform MyDragonToDragonCollider; // Each hand has a collider for the other dragons to look at, but we don't want our own

	private void OnTriggerEnter(Collider other)
	{
		// Check the name of the game object to see if it's something of interest (ie a player's face)
		// It might be cleaner to do this with a tag, but I prefer strings on a project where I have other devs
		// working with me - makes it more explicit and local within the GameObject.
		if (ContainsStringToLookFor(other.gameObject))
		{
			CurrentTarget = other.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		// If the object that exited is the current target, reset to look forward neutral
		// This is important because if a previous target exits, we don't want to reset
		if (CurrentTarget == other.transform)
		{
			ResetTarget ();
		}
	}
		
	private void Start () 
	{
		CurrentTarget = NeutralTarget;

		if (ResetLookOverTime)
		{
			// Reset target every 30 seconds as a safety net for OnTriggerExit not catching an exit
			// I'm told it's reliable, but I really feel like I sometimes have trouble with it
			InvokeRepeating ("ResetTarget", 0f, 30f);
		}
	}
	private void Update () 
	{
		// Reset the target is looking is disabled
		// Useful for close quarters shooting, as we don't want heads turning to look at players if trying to aim at something else
		if (!EnableLook && CurrentTarget != NeutralTarget)
		{
			ResetTarget ();
		}

		MoveIntermediateTarget ();
	}

    private bool ContainsStringToLookFor(GameObject source)
    {
        for (int i = 0; i < StringsToLookFor.Count; i++)
        {
            if (source.name.Contains(StringsToLookFor[i]) && source != MyDragonToDragonCollider)
            {
                return true;
            }
        }

        return false;
    }

	private void MoveIntermediateTarget()
	{
		// Safety net for if a player disconnects while a dragon is looking at them
		if (!CurrentTarget)
		{
			Debug.LogWarning ("CurrentTarget is null - target may have disappeared");
			ResetTarget ();
			return;
		}

		// Always lerp over time to the target
		// TODO: Unity forum member OneTen hates this method with a passion - could look into a better way
		IntermediateTarget.position = Vector3.Lerp (IntermediateTarget.position, CurrentTarget.position, TargetChangeSpeed * Time.deltaTime);
	}

	private void ResetTarget()
	{
		CurrentTarget = NeutralTarget;
	}
}
