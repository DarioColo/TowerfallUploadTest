using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

    public List<Sprite> lifesprites = new List<Sprite>();
	public Image currentLifeBar;

    public float maxLife;
    private float currentLife;

	void Start () {
		currentLifeBar = GetComponent<Image>();
        currentLife = maxLife;
	}
    private void UpdateMyLifeHUD(float damage)
    {
        var ratio = damage / maxLife;
        currentLifeBar.fillAmount -= ratio;
    }
    public void TakeDamage(float damage)
    {
        currentLife -= Mathf.RoundToInt(damage);
        UpdateMyLifeHUD(damage);
    }
}
