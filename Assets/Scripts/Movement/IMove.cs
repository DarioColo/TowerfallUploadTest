using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IMove {

    public PlayerController player;
    //Quiza un TimerPressing para saltar más preciso.
    public abstract void Move();

    public virtual void Update()
    {

    }
}
