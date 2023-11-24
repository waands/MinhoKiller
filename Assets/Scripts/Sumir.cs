using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ElementDisappear : MonoBehaviour
{
    public List<GameObject> elements; // Lista para armazenar os elementos que podem desaparecer
    private float timer = 5.0f; // Intervalo de tempo para desaparecimento
    private List<GameObject> sortedElements; // Lista ordenada de elementos

    private float shakeDuration = 3.0f; // Duração do tremor
    private float shakeMagnitude = 0.01f; // Intensidade do tremor

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

            // Começa o tremor apenas no elemento que vai desaparecer
            StartCoroutine(ShakeAndDisappear(elementToDisappear));

            sortedElements.RemoveAt(0); // Remove o elemento da lista ordenada
        }
        else
        {
            CancelInvoke("DisappearElement"); // Cancela o InvokeRepeating se não houver mais elementos
        }
    }

    IEnumerator ShakeAndDisappear(GameObject element)
    {
        Vector3 originalPos = element.transform.position;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * shakeMagnitude;

            element.transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        element.SetActive(false); // Desativa o elemento após o tremor
    }
}
