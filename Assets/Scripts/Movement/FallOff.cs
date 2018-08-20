using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOff : IMove
{

    public FallOff(PlayerController pl)
    {
        player = pl;
    }

    public override void Move()
    {
        player.verticalVelocity = -player.fallOffSpeed;
        player.moveVector.x = 0;
        player.moveVector.y = player.verticalVelocity;
        player.myAnim.Play("Forced Fall");
        player.isFallingOff = true;
    }
}