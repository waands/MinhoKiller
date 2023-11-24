using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referência para o componente de texto UI
    private float scoreTime; // Contador de tempo

    void Start()
    {
        scoreTime = 0f;
    }

    void Update()
    {
        scoreTime += Time.deltaTime; // Incrementa o tempo
        scoreText.text = "Tempo: " + scoreTime.ToString("F2"); // Atualiza o texto
    }
}