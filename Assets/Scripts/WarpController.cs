using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour {

    public WarpController parentWarp;
    public Transform zoneToRespawn;

    public void WarpWithParent(Transform pl)
    {
        pl.transform.position = parentWarp.zoneToRespawn.position;
    }

   
}
