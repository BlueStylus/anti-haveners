using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    private Animator playerAnim;
    private int direction = 0;

    // public int npcState;
    // 0 = idle down, -1 = walk down, -2 = idle up, -3 = walk up
    // 1 = idle left, 2 = walk left, 3 = idle right, 4 = walk right

    // Use this for initialization
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.Play("DownIdle", 0);
    }

    public void ChangeAnimation(int npcState)
    {
        switch (npcState)
        {
            case 0:
                playerAnim.Play("DownIdle", 0);
                break;
            case -1:
                playerAnim.Play("DownWalk", 0);
                break;
            case -2:
                playerAnim.Play("UpIdle", 0);
                break;
            case -3:
                playerAnim.Play("UpWalk", 0);
                break;
            case 1:
                playerAnim.Play("LeftIdle", 0);
                break;
            case 2:
                playerAnim.Play("LeftWalk", 0);
                break;
            case 3:
                playerAnim.Play("RightIdle", 0);
                break;
            case 4:
                playerAnim.Play("RightWalk", 0);
                break;
            default:
                playerAnim.Play("DownIdle", 0);
                break;
        }
    }
}
