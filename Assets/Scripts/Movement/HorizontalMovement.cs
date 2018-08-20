using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : IMove
{

    float movement;
    float maxSpeedTimer;
    float slowSpeedCharge;
    float currentSpeedTimer;
    public HorizontalMovement(PlayerController pl)
    {
        player = pl;
        currentSpeedTimer = 1;
        maxSpeedTimer = player.maxSpeedChargeTimer;
        slowSpeedCharge = player.slowSpeedCharge;
    }

    public override void Update()
    {
        base.Update();

        if (player.controller.isGrounded)
        {
            player.verticalVelocity = -player.gravity * Time.deltaTime;
            player.myAnim.SetBool("Jumping", false);
            player.myAnim.SetBool("Grounded", true);
        }
        else
        {
            player.verticalVelocity -= player.gravity * Time.deltaTime;
            player.myAnim.SetBool("Grounded", false);
        }

        movement = player.GetComponent<PlayerInput>().MainHorizontal();
        if (movement != 0)
        {
            if(!player.myAnim.GetBool("Jumping"))
                player.myAnim.SetBool("Running", true);
            currentSpeedTimer += Time.deltaTime / slowSpeedCharge;

            if (currentSpeedTimer >= maxSpeedTimer)
                currentSpeedTimer = maxSpeedTimer;
        }
        else
        {
            currentSpeedTimer = 1;
            player.myAnim.SetBool("Running", false);
        }
    }

    public override void Move()
    {
        player.transform.eulerAngles = movement == 0 ? player.transform.eulerAngles : new Vector3(0, Mathf.Sign(player.moveVector.x) * 90, 0);
        if (!player.controller.isGrounded && movement == 0 && player.moveVector.x != 0)
            player.moveVector.x += -Mathf.Sign(player.moveVector.x) * 1.5f;
        else
            player.moveVector.x = movement * currentSpeedTimer * player.moveSpeed + player.impactVelocity.x;
        player.moveVector.y = player.verticalVelocity;
        player.moveVector.z = 0;
    }
}
