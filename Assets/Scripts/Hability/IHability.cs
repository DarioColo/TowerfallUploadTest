using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IHability {

    public PlayerController player;
    public float timerCoolDown;
    public float coolDown;

    public abstract void Hability();

    public virtual void Update()
    {
        timerCoolDown -= Time.deltaTime;
    }
}
