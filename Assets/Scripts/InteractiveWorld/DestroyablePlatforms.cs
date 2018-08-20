using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatforms : MonoBehaviour
{

    public float lifeOfPlatform;
    private float acumulator;
    private float maxLife;

    public bool onlyFromAbove;

    private void Start()
    {
        maxLife = lifeOfPlatform;
    }

    void Update()
    {
        if (lifeOfPlatform <= 0)
            Destroy(gameObject);
    }

    public void DestroyablePlatform(PlayerController pl)
    {
        if (pl.stunned)
        {
            if (!onlyFromAbove)
            {
                lifeOfPlatform -= pl.impactSpeed;
                acumulator += pl.impactSpeed / 4;
                pl.impactVelocity = Vector3.zero;
                GetComponent<Renderer>().material.color = Color.HSVToRGB(0, ((acumulator) / maxLife), 0.8f);
            }
            else
            {
                lifeOfPlatform -= Mathf.Abs(pl.impactVelocity.y);
                acumulator += (Mathf.Abs(pl.impactVelocity.y) / 4);
                pl.impactVelocity = Vector3.zero;
                GetComponent<Renderer>().material.color = Color.HSVToRGB(0, ((acumulator) / maxLife) * 2, 0.8f);
            }
        }
    }
}
