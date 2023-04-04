using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;
    [SerializeField] 
    private TMP_Text score;
    void Update()
    {
        score.text = "Score: " + GameManager.Instance._score.ToString();

        if (GameManager.Instance.gameOver)
        {
            restartButton.SetActive(true);
        }
    }
}
