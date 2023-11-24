using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ElementDisappear : MonoBehaviour
{
    public List<GameObject> elements; // Lista para armazenar os elementos que podem desaparecer
    private float timer = 1.0f; // Intervalo de tempo para desaparecimento
    private List<GameObject> sortedElements; // Lista ordenada de elementos

    void Start()
    {
        // Ordena os elementos convertendo os nomes para números inteiros
        sortedElements = elements.OrderBy(go => int.Parse(go.name)).ToList();

        // Inicia o processo de desaparecimento
        InvokeRepeating("DisappearElement", timer, timer);
    }

    void DisappearElement()
    {
        if (sortedElements.Count > 0)
        {
            GameObject elementToDisappear = sortedElements[0];
            elementToDisappear.SetActive(false); // Desativa o elemento
            sortedElements.RemoveAt(0); // Remove o elemento da lista ordenada
        }
        else
        {
            CancelInvoke("DisappearElement"); // Cancela o InvokeRepeating se não houver mais elementos
        }
    }
}
