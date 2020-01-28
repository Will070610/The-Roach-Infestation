using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    public Animator animator;

    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool("OnOpen", true);
        StartCoroutine(LoadNextLevel());

    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        animator.SetBool("OnExit", true);
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
