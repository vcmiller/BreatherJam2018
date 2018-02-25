using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : DontStop {

    public Dictionary<string, int> scores;
    public GameObject returnBomb;

    UnityEngine.UI.Text winText
    {
        get
        {
            return GameObject.Find("Win Text").GetComponent<UnityEngine.UI.Text>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        scores = new Dictionary<string, int>();
    }

    private void OnLevelWasLoaded(int level)
    {
        foreach(var key in scores.Keys)
        {
            if(scores[key] > 6.9f)
            {
                winText.text = System.String.Format("Player {0} Wins!", Regex.Match(key, @"\d+").Value);
                winText.color = Color.white;
                Instantiate(returnBomb);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    public void Win (string target) {
        int score = 0;
        if (scores.TryGetValue(target, out score))
        {
            scores[target] = score + 1;
        }
        else
        {
            scores.Add(target, 1);
        }


        foreach (var tracker in FindObjectsOfType<ScoreTracker>())
        {
            tracker.Acclimate();
        }
	}
}
