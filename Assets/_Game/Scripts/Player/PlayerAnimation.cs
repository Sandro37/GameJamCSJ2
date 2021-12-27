using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRunning", player.IsRunning);
        anim.SetBool("isWall", player.IsWall);
        anim.SetBool("IsGround", player.IsGround);
    }
}
