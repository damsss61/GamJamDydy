using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void loadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneFade(sceneIndex));

    }

    IEnumerator LoadSceneFade(int sceneIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }
}
