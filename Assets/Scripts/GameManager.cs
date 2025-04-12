using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerHealthManager PHMan;

    private void Start()
    {
        PHMan = FindObjectOfType<PlayerHealthManager>();
    }

    public void RestartGame(float delay)
    {
        StartCoroutine(RestartGameCoroutine(delay));
    }

    private IEnumerator RestartGameCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PHMan.PlayerCurrentHealth = PHMan.PlayerMaxHealth;
        PHMan.gameObject.SetActive(true);
    }

}
