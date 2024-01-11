using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> phrases;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private void Start()
    {
        phrases = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {

        nameText.text = dialogue.nom;

        phrases.Clear();

        foreach(string phrase in dialogue.phrases)
        {
            phrases.Enqueue(phrase);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
       if(phrases.Count == 0)
        {
            EndDialogue();
            return;
        } 
       string phrase = phrases.Dequeue();
        StopAllCoroutines();
       StartCoroutine(TypeSentence(phrase));
    }
    IEnumerator TypeSentence(string phrase)
    {
        dialogueText.text = "";
        foreach (char lettre in phrase.ToCharArray())
        {
            dialogueText.text += lettre;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsDialogueFinished",true);
    }
}
