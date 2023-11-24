using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuManager : MonoBehaviour
{
    private List<GameObject> cobras = new List<GameObject>();
    public GameObject victoryMenu;
    public void Reiniciar()
    {
        Debug.Log("oi");
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }


    public void alive(GameObject cobra)
    {
        cobras.Add(cobra);
    }
    public void dead(GameObject cobra)
    {
        cobras.Remove(cobra);
        if (cobras.Count == 0)
        {
            victoryMenu.SetActive(true);
        }
    }
}
