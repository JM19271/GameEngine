using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScareManager : MonoBehaviour
{
    public Animator jumpScareAnimator;
    public string gameOverSceneName = "GameOverScene";

    public float jumpScareDuration = 5f;

    private void Start()
    {
        StartCoroutine(PlayJumpScareAndGoToGameOver());
    }

    private IEnumerator PlayJumpScareAndGoToGameOver()
    {
        if (jumpScareAnimator != null)
        {
            jumpScareAnimator.SetTrigger("PlayJumpScare");
        }

        yield return new WaitForSeconds(jumpScareDuration);

        SceneManager.LoadScene(gameOverSceneName);
    }
}
