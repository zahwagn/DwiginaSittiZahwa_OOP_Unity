using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator.gameObject.SetActive(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("StartTransition");

        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        Player.Instance.transform.position = new Vector3(0, -4.5f, 0);

        animator.SetTrigger("EndTransition");
        animator.gameObject.SetActive(false);
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}