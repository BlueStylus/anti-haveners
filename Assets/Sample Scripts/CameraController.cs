using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Serialized Fields
    public Transform player;
    public float playerAlignment = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,
            player.position.y + playerAlignment, -10); 
        // Camera follows the player but a little down
    }
}
