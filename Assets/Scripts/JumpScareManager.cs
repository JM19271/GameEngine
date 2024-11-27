using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class JumpScareManager : MonoBehaviour
{
    public Animator jumpScareAnimator;
    public string gameOverSceneName = "GameOverScene";
    public GameObject Player;
    public GameObject EYE;
    public GameObject MonsterLight;
    public GameObject Playercamera;
    public AudioSource jumpScareAudioSource;
    public AudioSource growlAudioSource;
    public GameObject MonsterCamera;
    public NavMeshAgent navAgent;

    public float jumpScareDuration = 5f;
    public float delayBeforeJumpScare = 2f;


    private void Start()
    {
        if (jumpScareAnimator == null)
        {
            Debug.LogError("JumpScareManager: jumpScareAnimator is not assigned!");
        }

        if (Player == null)
        {
            Debug.LogError("JumpScareManager: Player is not assigned!");
        }

        if (MonsterLight == null)
        {
            Debug.LogError("JumpScareManager: monsterLight is not assigned!");
        }

        if (jumpScareAudioSource == null)
        {
            Debug.LogError("JumpScareManager: monsterAudio is not assigned!");
        }

        if (growlAudioSource == null)
        {
            Debug.LogError("JumpScareManager: growlAudioSource is not assigned!");
        }

        if (navAgent == null)
        {
            Debug.LogError("JumpScareManager: NavMeshAgent is not assigned!");
        }

        MonsterLight.SetActive(false);
        EYE.SetActive(false);
        MonsterCamera.SetActive(false);

        if (jumpScareAudioSource != null)
        {
            jumpScareAudioSource.enabled = false;  
        }
    }
    
    public void TriggerJumpScare()
    {
        if (growlAudioSource != null)
        {
            growlAudioSource.Stop(); 
        }

        StartCoroutine(PlayJumpScareAndGoToGameOver());
    }

    private IEnumerator PlayJumpScareAndGoToGameOver()
    {
        PlayerControl playerMovement = Player.GetComponent<PlayerControl>();

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (navAgent != null) navAgent.enabled = false;

        if (jumpScareAnimator != null)
        {
            jumpScareAnimator.SetBool("IsWalking", false);
            jumpScareAnimator.SetBool("IsRunning", false);
            jumpScareAnimator.SetTrigger("PlayJumpScare");
        }

        if (MonsterLight != null)
        {
            MonsterLight.SetActive(true);
        }

        if (EYE != null)
        {
            EYE.SetActive(true);
        }

        if (MonsterCamera != null)
        {
            MonsterCamera.SetActive(true);
        }

        if (Playercamera != null)
        {
            Playercamera.SetActive(false);
        }

        if (Player != null)
        {
            Player.SetActive(false);
        }

        yield return new WaitForSeconds(delayBeforeJumpScare);

        if (jumpScareAnimator != null)
        {
            jumpScareAnimator.SetTrigger("PlayJumpScare");
        }

        if (jumpScareAudioSource != null)
        {
            jumpScareAudioSource.enabled = true;
        }

        yield return new WaitForSeconds(jumpScareDuration);

        SceneManager.LoadScene(gameOverSceneName);
    }
}
