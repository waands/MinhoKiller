using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private float scoreTime;
    public bool timerActive = true;

    void Start()
    {
        scoreTime = 0f;
    }

    void Update()
    {
        if (timerActive)
        {
            scoreTime += Time.deltaTime; 
            scoreText.text = "Tempo: " + scoreTime.ToString("F2");
        }
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    // Adicione um método para reiniciar o temporizador se necessário
    public void StartTimer()
    {
        timerActive = true;
    }
}