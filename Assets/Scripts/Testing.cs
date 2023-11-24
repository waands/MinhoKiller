using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

public class Testing : MonoBehaviour
{
    public Grid grid;
    private float cellSize = 0.1f; // Defina o tamanho da célula conforme desejado.

    private void Awake()
    {
        // Obter o Renderer do objeto
        Renderer renderer = GetComponent<Renderer>();

        // Usar o tamanho do bounds do objeto
        float objectWidth = renderer.bounds.size.x;
        float objectHeight = renderer.bounds.size.y;

        // Calcular o número de células
        int cellsInX = Mathf.CeilToInt(objectWidth / cellSize);
        int cellsInY = Mathf.CeilToInt(objectHeight / cellSize);

        // Definir a posição de origem para centralizar o grid no objeto
        Vector3 originPosition = transform.position - new Vector3(objectWidth / 2, objectHeight / 2, 0);

        // Criar o grid
        grid = new Grid(cellsInX, cellsInY, cellSize, originPosition);
    }


    private void Update()
    {

    }
}


