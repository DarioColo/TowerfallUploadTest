using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePowerUp : MonoBehaviour, IPowerUp
{
    public int effect;
    public int PowerUp()
    {
        return effect;
    }

}
