using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ScoreSystem : MonoBehaviour
{

    static int Score;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RedWins;

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();

        if (Score >= 5)
        {
            YouWin();
        }
    }
    
    void YouWin()
    {
        RedWins.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(GameObject.FindWithTag("Ball"));
            AddScore();
            StartCoroutine(RedGoal());
        }
    }

    void AddScore()
    {
        Score++;    
    }

    IEnumerator RedGoal()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SocCar");
    }
}
