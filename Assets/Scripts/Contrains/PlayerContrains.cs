using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrains : MonoBehaviour {

    Transform player;
    float myContrains_Z;

    public Collider leftWarp;
    public Collider rightWarp;

    float buffer = 2f;

    void Start ()
    {
        player = gameObject.transform;
        myContrains_Z = 0;
	}

    private void Update()
    {
        if (player.position.z != 0)
            player.position = new Vector3(player.position.x, player.position.y, myContrains_Z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("DoorWarp"))
        {
            other.gameObject.GetComponent<WarpController>().WarpWithParent(transform);
            //var warpPos = other.transform.position.x + (Mathf.Sign(other.transform.position.x) * buffer);
            //if (player.position.x < warpPos)
            //    OnMyLeft();
            //else if (player.position.x > warpPos)
            //    OnMyRight();
        }
    }

    void OnMyLeft()
    {
        var warpPos = leftWarp.transform.position.y - leftWarp.bounds.extents.y;
        player.position = new Vector3(leftWarp.transform.position.x + buffer, warpPos, 0);
    }

    void OnMyRight()
    {
        var warpPos = rightWarp.transform.position.y - rightWarp.bounds.extents.y;
        player.position = new Vector3(rightWarp.transform.position.x - buffer, warpPos, 0);
    }
}
