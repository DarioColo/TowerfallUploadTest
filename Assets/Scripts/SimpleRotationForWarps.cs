using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationForWarps : MonoBehaviour {

    public bool imadoor;
    float normalscale;
    float streechedscale;
    public bool streechdir;
    public float strechtimer;
    public float testimer;
    public float testtimemulti;
	// Use this for initialization
	void Start () {
        if (imadoor)
        {
            streechedscale = transform.localScale.y;
            normalscale = transform.localScale.z;
        }
        streechdir = true;
	}
	
	// Update is called once per frame
	void Update () {

        //streechdir = transform.localScale.y == streechedscale ? true : false;
        strechtimer += Time.deltaTime;
        if (!imadoor) transform.Rotate(0, 0.5f, 0);
        else
        {
            transform.Rotate(1, 0, 0);
            //if (streechdir)
            //{
            //    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, normalscale, streechedscale), strechtimer * testtimemulti);
            //    if (strechtimer >= testimer)
            //    {
            //        strechtimer = 0;
            //        streechdir = false;
            //    }
            //}
            //else
            //{
            //    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, streechedscale, normalscale), strechtimer * testtimemulti);
            //    if (strechtimer >= testimer)
            //    {
            //        strechtimer = 0;
            //        streechdir = true;
            //    }
            //}
        }
    }
}
