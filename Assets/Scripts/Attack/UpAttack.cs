using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAttack : IAttack
{
    public UpAttack(PlayerController pl, float _timerCoolDown = 0)
    {
        player = pl;
        timerCoolDownAttack = _timerCoolDown;
        coolDownAttack = _timerCoolDown;
        weaponExtends = player.weaponExtends;
        impactVelocity = player.impactVelocityUp;
        defaultAttack = player.defaultAttackUp;
        influenceOfMovement = player.influenceOfMovementUp;
        currentPressed = 1;
        maxPressed = 2.5f;
        minImpact = 30;
    }

    public override void Update()
    {
        base.Update();
        if (isPressing)
            if (currentPressed <= maxPressed)
                currentPressed += Time.deltaTime * 2;
    }

    public override void Attack(Collider col)
    {
        if (timerCoolDownAttack < 0)
        {
            player.myAnim.SetBool("ReleaseAUp", true);
            if (player.myAnim.GetBool("Grounded"))
                player.myAnim.Play("AttackUp");
            else
                player.myAnim.Play("HitUpAir");
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if (CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                player.hitParticles.Play();
                if (target != null)
                {
                    target.ReceiveDamage(new Vector3(0, CalculateImpact(currentPressed), 0));
                    player.whoIHited = target;
                    player.myAnim.SetBool("ReleaseAUp", true);
                }
            }
            isPressing = false;
            currentPressed = 1;
            timerCoolDownAttack = coolDownAttack;
        }
    }

    public override void Pressed()
    {
        player.myAnim.SetBool("ReleaseAUp", false);
        player.myAnim.Play("ChargingAUp");
        isPressing = true;
    }
}
