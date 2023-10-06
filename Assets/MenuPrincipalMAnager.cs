using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string nomeLvlJogo;
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuOpcoes;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeLvlJogo);
    }

    public void AbrirOpcoes()
    {
        menuPrincipal.SetActive(false);
        menuOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        menuPrincipal.SetActive(true);
        menuOpcoes.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }

}
