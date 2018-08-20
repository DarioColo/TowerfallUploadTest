using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDamage : MonoBehaviour
{
    public int impactVelocity;

    public float timer = 0.25f;

    public enum Direction
    {
        Up,
        Down,
        NegativeHorizontal,
        PositiveHorizontal,
    }

    public Direction myDir;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            foreach (var item in Physics.OverlapSphere(transform.position, 1f))
            {
                if (item.GetComponent<PlayerController>())
                {
                    if (myDir == Direction.Up)
                    {
                        item.GetComponent<PlayerController>().ReceiveDamage(new Vector3(0, impactVelocity, 0));
                    }
                    if (myDir == Direction.Down)
                    {
                        item.GetComponent<PlayerController>().ReceiveDamage(new Vector3(0, -impactVelocity, 0));
                    }
                    if (myDir == Direction.NegativeHorizontal)
                    {
                        item.GetComponent<PlayerController>().ReceiveDamage(new Vector3(-impactVelocity, 0, 0));
                    }
                    if (myDir == Direction.PositiveHorizontal)
                    {
                        item.GetComponent<PlayerController>().ReceiveDamage(new Vector3(impactVelocity, 0, 0));
                    }
                    timer = 0;  
                }
            }
        }
    }
}
