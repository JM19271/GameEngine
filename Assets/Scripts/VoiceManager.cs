using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceManager : MonoBehaviour
{
    public Text dialogueText;
    public AudioSource audioSource;
    public AudioClip phoneCallClip;

    private string dialogue = "Damn it! The engine’s dead. Just my luck... Stuck in the middle of nowhere. I’ve gotta find something—tools, parts, anything—to get this piece of junk running again.";

    void Start()
    {
        Invoke("StartPhoneCall", 1f);
    }

    void StartPhoneCall()
    {
        dialogueText.text = dialogue;


        Invoke("HideDialogue", 13f);
    }

    void HideDialogue()
    {
        dialogueText.text = "";
    }

}
