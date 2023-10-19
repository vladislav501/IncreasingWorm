using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Worm : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> wormtail;
    [SerializeField] Transform wormtail_prefab;

    [SerializeField] Text scoreText;
    private int score = 0;

    [SerializeField] Text bestScoreText;
    private int bestScore = 0;

    [SerializeField] Button btn_UP;
    [SerializeField] Button btn_DOWN;
    [SerializeField] Button btn_LEFT;
    [SerializeField] Button btn_RIGHT;

    [SerializeField] Text timerText;
    float elapsedTime;
 

    private void Start()
    {
        
        wormtail = new List<Transform>();
        wormtail.Add(this.transform);
    }

    private void Update()
    {
        Timer();
        

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }

        
    }

    public void ButtonUP()
    {
        direction = Vector2.up;
    }
    public void ButtonDOWN()
    {
        direction = Vector2.down;
    }
    public void ButtonLEFT()
    {
        direction = Vector2.left;
    }
    public void ButtonRIGHT()
    {
        direction = Vector2.right;
    }

    private void CheckScore()
    {
        if (score >= bestScore)
            bestScore = score;

        scoreText.text = "Очки: " + score.ToString();
        bestScoreText.text = "Рекорд: " + bestScore.ToString();
    }

    private void Timer()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("Время: " + "{0:00}:{1:00}", minutes, seconds);
    }

   

    private void FixedUpdate()
    {
        for (int i = wormtail.Count - 1; i > 0; i--)
        {
            wormtail[i].position = wormtail[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 10f);
    }


    private void AddTail()
    {
        Transform tail = Instantiate(this.wormtail_prefab);
        tail.position = wormtail[wormtail.Count - 1].position;

        wormtail.Add(tail);
    }

    private void GameOver()
    {
        for (int i = 1; i < wormtail.Count; i++)
        {
            Destroy(wormtail[i].gameObject);
        }

        wormtail.Clear();
        wormtail.Add(this.transform);

        this.transform.position = Vector3.zero;

        if (score >= bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("wormScore", bestScore);
        }

        score = 0;
        CheckScore();
        elapsedTime = 0;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "fruit")
        {
            AddTail();
            score++;
            CheckScore();
        }
        else if (other.tag == "gameover")
        {
            GameOver();
        }
    }
}