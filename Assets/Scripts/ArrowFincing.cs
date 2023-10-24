using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowFincing : MonoBehaviour
{
    public GroundArrow groundArrowPrefab; // Prefab da flecha fincada no chão
    public Transform ground; // Objeto que representa o chão onde as flechas podem ser fincadas
    public Transform playerTransform; // Variável para a referência ao transform do jogador
    public int maxGroundArrows = 5; // Limite máximo de flechas fincadas no chão

    private GroundArrow currentArrow; // Referência para a flecha fincada atual

    //manter o controle das flechas no chão
    public List<GameObject> groundArrows = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FincArrow();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
        RemoveGroundArrow();
        }
    }

    private void FincArrow()
    {
        // Verifique se já existe uma flecha fincada
        //if (currentArrow == null)
        if (groundArrows.Count < maxGroundArrows)
        {
            // Crie uma nova flecha fincada
            Vector3 playerPosition = playerTransform.position; // Obtém a posição atual do jogador
            currentArrow = Instantiate(groundArrowPrefab, playerPosition, Quaternion.identity);

            currentArrow.transform.parent = ground; // Coloque a flecha no chão
            groundArrows.Add(currentArrow.gameObject); // Adicione a flecha à lista
        }
    }

    void RemoveGroundArrow()
    {
        if (groundArrows.Count > 0)
        {
            GameObject arrowToRemove = groundArrows[groundArrows.Count - 1];
            groundArrows.RemoveAt(groundArrows.Count - 1);
            Destroy(arrowToRemove); // Destrua o objeto da flecha fincada
        }
    }

}
