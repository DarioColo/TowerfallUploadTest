using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IHability {

    public float dashingTime;
    public float dashTimer;

    public Dash(PlayerController p, float _timerCoolDown = 0)
    {
        player = p;
        timerCoolDown = _timerCoolDown;
        coolDown = _timerCoolDown;
        dashTimer = player.dashDistance / player.dashSpeed;
    }

    public override void Update()
    {
        base.Update();
        if (player.isDashing && dashingTime <= dashTimer)
            dashingTime += Time.deltaTime;
        else
        {
            dashingTime = 0;
            player.isDashing = false;
            player.myAnim.SetBool("Dashing", false);
        }

        if (player.controller.isGrounded && !player.isDashing)
            player.canDash = true;

    }

    public override void Hability()
    {
        if(timerCoolDown < 0)
        {
            player.isDashing = true;
            player.canDash = false;
            player.moveVector.x = Mathf.Sign(player.transform.rotation.y) * player.dashSpeed;
            player.verticalVelocity = 0;
            player.moveVector.y = player.verticalVelocity;
            timerCoolDown = coolDown;
            player.myAnim.Play("Dash");
            player.myAnim.SetBool("Dashing", true);
        }
    }
}
