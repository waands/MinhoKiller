using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

public class Testing : MonoBehaviour
{
    public Grid grid;
    private float cellSize = 1f; // Defina o tamanho da célula conforme desejado.

    private void Awake()
    {
        
        // Calcular o tamanho da câmera
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Calcular o número de células
        int cellsInX = Mathf.CeilToInt(cameraWidth / cellSize);
        int cellsInY = Mathf.CeilToInt(cameraHeight / cellSize);

        // Definir a posição de origem para centralizar o grid
        Vector3 originPosition = new Vector3(-cameraWidth / 2, -cameraHeight / 2);

        // Criar o grid
        grid = new Grid(cellsInX, cellsInY, cellSize, originPosition);
        Debug.Log(grid);

    }

    private void Update()
    {

    }
}


