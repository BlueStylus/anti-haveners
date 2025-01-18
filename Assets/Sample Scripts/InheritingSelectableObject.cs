using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExampleSelectableObject : MonoBehaviour
{
    public abstract void OnSelection();
}

public class InheritingSelectableObject : ExampleSelectableObject
{
    public override void OnSelection()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Hi");
    }
}
