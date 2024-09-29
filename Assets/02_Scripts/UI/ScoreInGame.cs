using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreInGame : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score;

    void Start()
    {
        EventManager.Instance.CollectedObjectsUpdated.AddListener(UpdateFromState);
    }

    void UpdateFromState () {
        Hydrate(GameState.NumberOfCollectedObjects());
    }

    public void Hydrate(int scoreResult)
    {
        score.text = scoreResult.ToString();
    }

}
