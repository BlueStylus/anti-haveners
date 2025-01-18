using System.Collections;
using TMPro;
using UnityEngine;

/* This script is only for Controller of Caption object
 * Model and View are in CaptionTrigger.cs
 * 
 */

public class CaptionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _captionObject;
    private TextMeshProUGUI _captionTextBox;

    [SerializeField] private TextMeshProUGUI _hintObject;
    private TextMeshProUGUI _hintTextBox;

    public static bool captionToggle;

    [SerializeField] private bool _defaultState = false;

    private void Start()
    {
        _captionTextBox = _captionObject.GetComponent<TextMeshProUGUI>();
        _captionTextBox.enabled = _defaultState;

        _hintTextBox = _hintObject.GetComponent<TextMeshProUGUI>();
    }

    public void RecieveCaption(string caption, float displayDuration)
    {
        // Put any logic for queuing or waiting or only showing one caption at a time.
        StartCoroutine(DisplayCaption(caption, displayDuration));
    }

    // this is a co-routine by the way
    private IEnumerator DisplayCaption(string caption, float duration)
    {
        // IEnumerator type is something that happens over multiple frames
        // (array of possible things and then enumerating through them)

        _captionTextBox.text = caption;
        _captionTextBox.enabled = true;

        if (captionToggle)
        {
            _captionTextBox.text = caption;
            _captionTextBox.enabled = true;

            yield return new WaitForSeconds(duration);

            _captionTextBox.enabled = false;
        }
    }

    public void ReceiveHint(string hint)
    {
        _captionTextBox.enabled = true;
        _hintTextBox.text = hint;
    }
}
