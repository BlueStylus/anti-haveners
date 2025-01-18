using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera _selectionCamera;
    // [SerializeField] private CaptionHandler _captionHandler;
    // [SerializeField] private CaptionHandler _hintHandler;

    private SelectableObject _currentlyHovered;

    // Update is called once per frame
    void Update()
    {
        Ray ray = _selectionCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            SelectableObject potentialSelectable = hit.collider.gameObject.GetComponent<SelectableObject>();

            if (potentialSelectable != null)
            {
                potentialSelectable.HoverHighlight(true);
                _currentlyHovered = potentialSelectable;

                // this is to select using E, change to select by staring at the item
                if (_currentlyHovered.destroy)
                {
                    // _captionHandler.RecieveCaption(_currentlyHovered.caption, _currentlyHovered.displayDuration);
                    //_hintHandler.RecieveCaption("What you're looking for:", 5f);
                }
            }
            else
            {
                _currentlyHovered?.HoverHighlight(false);
                _currentlyHovered = null;
            }
        }
        else
        {
            // the ? also checks if the value (currentlyHovered) is null
            _currentlyHovered?.HoverHighlight(false);
            _currentlyHovered = null;
        }
    }
}
