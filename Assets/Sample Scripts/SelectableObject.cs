using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    // private MeshRenderer _meshRenderer;
    // private Material _defaultMaterial;
    // [SerializeField] private Material _hoveredMaterial;

    private bool isTiming;
    public float timer;
    public float maxTime = 3f;

    public bool destroy;

    [SerializeField] private GameObject audioManager;
    [SerializeField] private int audioIndex;
    private AudioSource[] audios;

    [SerializeField] private AudioSource itemFound;

    [SerializeField] private bool firstItem;
    public SelectableObject prevItem;
    public bool locked;

    // this triggers every time an object is enabled (toggle on/off)
    private void Awake()
    {
        // _meshRenderer = GetComponent<MeshRenderer>();
        // _defaultMaterial = _meshRenderer.material;

        audios = audioManager.GetComponent<AudioManager>().audios;
    }

    public bool IsSelected { get; private set; }

    public void HoverHighlight(bool toggle)
    {
        if (toggle)
        {
            // Debug.Log("I am being hovered!");
            // _meshRenderer.material = _hoveredMaterial != null ? _hoveredMaterial : _defaultMaterial;
            if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
                isTiming = true;
        } 
        else
        {
            // _meshRenderer.material = _defaultMaterial;
            isTiming = false;
        }
    }

    public void Update()
    {
        if (firstItem)
        {
            if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
            {
                destroy = true;

                for (int i = 0; i < audios.Length; i++)
                {
                    audios[i].Stop();
                }
                audios[audioIndex].Play();
                itemFound.Play();

                gameObject.SetActive(false);
            }
        }
        else if (prevItem.destroy == true && locked == false)
        {
            if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
            {
                //_meshRenderer.material = _hoveredMaterial != null ? _hoveredMaterial : _defaultMaterial;
                destroy = true;

                for (int i = 0; i < audios.Length; i++)
                {
                    audios[i].Stop();
                }
                audios[audioIndex].Play();
                itemFound.Play();

                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }

    public void Unlock()
    {
        locked = false;
    }
}
