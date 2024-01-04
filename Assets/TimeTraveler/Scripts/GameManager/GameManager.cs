using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneAsset _nextScene;
    public GameObject _winText;
    public GameObject _loseText;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _winText.SetActive(false);
        _loseText.SetActive(false);
    }

    public void ShowWinText()
    {
        _winText.SetActive(true);
    }

    public void ShowLoseText()
    {
        _loseText.SetActive(true);
    }

    void RestartLevel()
    {
        _winText.SetActive(false);
        _loseText.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextScene.GetInstanceID());
    }

    private IEnumerator HandleLose()
    {
        ShowLoseText();
        yield return new WaitForSeconds(1f);
        RestartLevel();
    }

    private IEnumerator HandleWin()
    {
        ShowWinText();
        yield return new WaitForSeconds(1f);
        LoadNextLevel();
    }

    public void Lose() {
        StartCoroutine(HandleLose());
    }

    public void Win()
    {
        StartCoroutine(HandleWin());
    }
}