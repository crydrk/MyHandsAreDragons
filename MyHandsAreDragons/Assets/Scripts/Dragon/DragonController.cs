using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    public string Name;

    public void ResetDragon()
    {
        Debug.LogWarning("Resetting dragon: " + Name);
    }
}
