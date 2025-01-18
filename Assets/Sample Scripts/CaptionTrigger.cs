using UnityEngine;

/* This script is only for Model and View of Caption object
 * Controller is in CaptionHandler.cs
 * 
 */

public class CaptionTrigger : MonoBehaviour
{
    public string caption;
    public float displayDuration; // in seconds

    private void OnTriggerEnter(Collider other)
    {
        CaptionHandler potentialHandler = other.GetComponent<CaptionHandler>();

        if (potentialHandler != null )
        {
            // sends caption info to caption handler
            potentialHandler.RecieveCaption(caption, displayDuration);
            gameObject.SetActive(false);
        }
    }
}
