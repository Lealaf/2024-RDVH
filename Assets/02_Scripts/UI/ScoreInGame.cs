using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreInGame : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score;

    public void Hydrate(int scoreResult)
    {
        score.text = scoreResult.ToString();
    }

}
