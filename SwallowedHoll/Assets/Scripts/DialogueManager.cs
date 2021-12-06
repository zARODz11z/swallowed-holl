using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Created by Andrew Rodriguez with the help of https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
//This class manages most of the logic for our dialogue system. It connects the NpcDialoge and Dialogue script together.
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Grab grab;
    [SerializeField]
    HandAnim hand;
    [SerializeField]
    Movement movement;
    [SerializeField]
    SimpleCameraMovement cameraMovement;
    public Text nameText; //npc name text object
    public Text dialogueText; //dialogue text object
    private Queue<string> sentences; 
    public GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) //creates our queue of sentences, locks the player, enables the cursor, and activates dialogue UI
    {
        Debug.Log("Starting conversation with "+dialogue.name);
        movement.blockMovement();
        hand.forceIdle();
        grab.enabled = false;
        hand.enabled = false;
        cameraMovement.enabled = false;
        Cursor.visible = true; //makes cursor visible
        Cursor.lockState = CursorLockMode.None;//makes cursor moveable
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogueBox.SetActive(true);//enable the dialogue box UI
        DisplayNextSentence();
    }
    public void DisplayNextSentence() //displays next sentence in queue
    {
        if (sentences.Count==0)
        {
            EndDialogue();
            return;
        }
        string curSentence = sentences.Dequeue();
        StopAllCoroutines(); //This makes sure the animating stops if the player clicks the continue button
        StartCoroutine(TypeSentence(curSentence)); //We call our coroutine to display each word at a delay
    }

    IEnumerator TypeSentence (string sentence)//displays words in sentence with a delay
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        grab.enabled = true;
        movement.unblockMovement();
        hand.enabled = true;
        hand.setisCrouching(false);
        cameraMovement.enabled = true;

        dialogueBox.SetActive(false); //makes dialogue box disapear
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
