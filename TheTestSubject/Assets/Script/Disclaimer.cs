using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disclaimer : MonoBehaviour
{
    [SerializeField] private int _disclaimerDuration = 35;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayLongSound("Disclaimer");
        Invoke("RestartGame", _disclaimerDuration);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
