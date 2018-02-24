using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    public Doog doog;
    public Image image;
    public Text text;

    private void Start()
    {
        Acclimate();
    }

    public void Acclimate()
    {
        Acclimate(doog);
    }

    public void Acclimate(Doog doog)
    {
        this.doog = doog;

        int score = 0;
        FindObjectOfType<ScoreManager>().scores.TryGetValue(doog.name, out score);
        text.text = score.ToString();

        image.sprite = doog.GetComponent<SpriteRenderer>().sprite;
    }

}
