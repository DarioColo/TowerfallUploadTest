using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public GameObject prefabPowerUp;
    public List<Transform> powerupsPos;
    public float spawnTimer;

    private void Start()
    {
        spawnTimer = 12f;
    }

    void Update ()
    {
        spawnTimer += Time.deltaTime;
		if(spawnTimer > 20f)
        {
            CreatePowerUp();
            spawnTimer = 0;
        }
	}

    void CreatePowerUp()
    {
        var powerUp = Instantiate(prefabPowerUp);
        powerUp.transform.position = powerupsPos[Random.Range(0, powerupsPos.Count - 1)].position;
    }
}
