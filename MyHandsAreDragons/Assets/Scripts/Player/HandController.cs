using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public List<JawManager> DragonJaws = new List<JawManager>();

	public void Fire()
    {
        for (int i = 0; i < DragonJaws.Count; i++)
        {
            DragonJaws[i].SetTargetJawRotationNormalized(1f);
        }

        StartCoroutine(ResetDragonJawsAfterTime());
    }

    public IEnumerator ResetDragonJawsAfterTime()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < DragonJaws.Count; i++)
        {
            DragonJaws[i].SetTargetJawRotationNormalized(0f);
        }
    }

    public void SetJawRotationRaw(float value)
    {
        for (int i = 0; i < DragonJaws.Count; i++)
        {
            DragonJaws[i].SetTargetJawRotationNormalized(value);
        }
    }
}
