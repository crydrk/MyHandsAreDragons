using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonManager : MonoBehaviour
{
    // List of DragonControllers - each dragon already lives within the prefab
    public List<DragonController> Dragons = new List<DragonController>();
    private Dictionary<string, DragonController> dragonDict = new Dictionary<string, DragonController>();
    private DragonController currentDragon;

    // A dummy component is added to DragonController's GameObject to automatically set the dragons' values
    // instead of each potentially having its own settings - this component should always just be disabled
    public LookAtIK BaseLookIK;

    private void Start()
    {
        BuildDragonDict();
    }

    private void BuildDragonDict()
    {
        for (int i = 0; i < Dragons.Count; i++)
        {
            dragonDict[Dragons[i].Name] = Dragons[i];
        }
    }

    private void SetCurrentDragon(string name)
    {
        // If the name is not in the dictionary, do nothing
        if (!dragonDict.ContainsKey(name))
        {
            Debug.LogWarning("Dragon name not found in dragonDict");
            return;
        }

        currentDragon.ResetDragon();
        currentDragon.gameObject.SetActive(false);

        currentDragon = dragonDict[name];
        
    }
}
