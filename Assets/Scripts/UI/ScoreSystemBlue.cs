using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreSystemBlue : MonoBehaviour
{
    static int Score;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BlueWins;

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
        BlueWins.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(GameObject.FindWithTag("Ball"));
            AddScore();
            StartCoroutine(BlueGoal());
        }
    }

    void AddScore()
    {
        Score++;
    }

    IEnumerator BlueGoal()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SocCar");
    }

}
