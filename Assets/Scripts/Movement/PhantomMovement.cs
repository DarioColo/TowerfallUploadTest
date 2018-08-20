using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMovement : IMove
{

    PhantomController phantom;
    public PhantomMovement(PhantomController pl)
    {
        phantom = pl;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        float moveHorizontal = phantom.GetComponent<PlayerInput>().MainHorizontal();

        phantom.transform.position += phantom.transform.forward * 5 * Time.deltaTime;
        Vector3 dir = new Vector3(moveHorizontal * 5, 0, 0);
        var v = phantom.transform.rotation * Quaternion.Euler(dir);
        phantom.transform.rotation = Quaternion.Lerp(phantom.transform.rotation, v, Time.deltaTime * 50);
    }


}
