using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{
    [SerializeField] private GameObject firstTimeCutscene;
    [SerializeField] private GameObject normalCutscene;

    private static bool hasSceneBeenLoadedBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!hasSceneBeenLoadedBefore)
        {
            hasSceneBeenLoadedBefore = true;
            PlayFirstTimeCutscene();
        }
        else
        {
            PlayNormalCutscene();
        }
    }

    private void PlayFirstTimeCutscene()
    {
        if (firstTimeCutscene != null)
        {
            firstTimeCutscene.SetActive(true);
            Debug.Log("Playing first-time cutscene.");
        }
    }

    private void PlayNormalCutscene()
    {
        if (normalCutscene != null)
        {
            normalCutscene.SetActive(true);
            Debug.Log("Playing normal cutscene.");
        }
    }

}
