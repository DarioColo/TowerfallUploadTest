using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counterdown : MonoBehaviour
{

    float timer;
    bool activeTimer;
    float count;

    void Start()
    {
        count = (int)GameManager.Instance.timeToStart;
        GetComponent<TextMesh>().text = count.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime / 3;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timer);
        if (transform.localScale == Vector3.zero)
            Restart();
    }

    void Restart()
    {
        count--;
        transform.localScale = Vector3.one;
        transform.eulerAngles = Vector3.zero;
        if (count < 0)
            Destroy(gameObject);
        if (count == 0)
            GetComponent<TextMesh>().text = "FIGHT!";
        else
            GetComponent<TextMesh>().text = count.ToString();
        timer = 0;
    }
}
