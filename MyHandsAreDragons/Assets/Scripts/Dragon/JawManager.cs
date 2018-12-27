using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawManager : MonoBehaviour
{
    public Vector3 JawClosedLimit = new Vector3();
    public Vector3 JawOpenLimit = new Vector3();
    public Transform JawJoint;
    public float JawMovementSpeed = 5f;

    private float normalizedJawExtension;
    private float currentNormalizedJawExtension;


    public void SetTargetJawRotationNormalized(float amount)
    {
        normalizedJawExtension = amount;
    }
	
	void Update ()
    {
        currentNormalizedJawExtension = Mathf.Lerp(currentNormalizedJawExtension, normalizedJawExtension, Time.deltaTime * JawMovementSpeed);
        JawJoint.localRotation = Quaternion.Lerp(Quaternion.Euler(JawClosedLimit), Quaternion.Euler(JawOpenLimit), currentNormalizedJawExtension);
	}
}
