using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IAttack
{
    public PlayerController player;
    public float timerCoolDownAttack;
    public float coolDownAttack;
    public float weaponExtends;
    public float impactVelocity;
    public float defaultAttack;
    public float influenceOfMovement;
    public float chargedEffect;

    public float currentPressed;
    public float maxPressed;
    public bool isPressing;
    public float minImpact;

    public abstract void Attack(Collider col);
    public abstract void Pressed();
    public virtual void Update()
    {
        timerCoolDownAttack -= Time.deltaTime;
    }

    public bool CheckParently(Transform t)
    {
        if (t.parent == null)
            return false;
        if (t.parent == player.transform)
            return true;
        return CheckParently(t.parent);
    }

    public PlayerController TargetScript(Transform t)
    {
        if (t.GetComponent<PlayerController>())
            return t.GetComponent<PlayerController>();
        if (t.parent == null)
            return null;
        return TargetScript(t.parent);
    }

    public float CalculateImpact(float x)
    {
        return minImpact * x;
    }
}
