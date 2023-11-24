using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenuManager : MonoBehaviour
{
    public void Reiniciar()
    {
        Debug.Log("oi");
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }
}
