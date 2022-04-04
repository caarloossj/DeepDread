using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RedBlueGames.Tools.TextTyper;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Dialogue;

public class DialogueParser : MonoBehaviour
{
    public xDialogueGraph graph;
    public GameObject DialogueBar;
    [SerializeField] private TextTyper typer;
    public TextMeshProUGUI tmpro_dialogue;
    public TextMeshProUGUI tmpro_name;
    private UIInputHandler inputHandler;
    [SerializeField] private float baseDefaultHeight = 128;
    [SerializeField] private float baseLineHeight = 20;
    [SerializeField] private float baseMinHeight = 124;
    [SerializeField] private RectTransform baseTransform;
    public TextMeshProUGUI tmpro_question;
    private string previousCharacter = "";

    private void Awake() {
        inputHandler = UIInputHandler.instance;
    }

    //Debug Only
    private void Start() {
        StartDialogue();
    }

    private void SkipText()
    {
        //Skip text or go to the next node
        if(typer.IsTyping)
        {
            typer.Skip();
        } else {
            NextNode();
        }
    }

    public void StartDialogue() {
        //Go to first node in graph
        foreach (xBaseNode b in graph.nodes)
        {
            if(b.GetString() == "Start")
            {
                graph.current = b;
                break;
            }
        }

        NextNode();
    }

    private void CreateNewBox()
    {
        var node = graph.current as xDialogueNode;

        //Set name
        var name = node.character.CharacterName;

        //If new character, popup animation
        if(name != previousCharacter)
        {
            var scale = DialogueBar.transform.localScale;
            var startScale = scale;
            startScale.y = 0;

            DialogueBar.transform.localScale = startScale;
            DialogueBar.transform.DOScale(scale, 0.3f).SetEase(Ease.OutBack);
        }

        tmpro_name.text = name;
        previousCharacter = name;

        var txtInfo = tmpro_dialogue.GetTextInfo(node.dialogueText);
        var height = baseDefaultHeight + baseLineHeight * (txtInfo.lineCount);
        if(height < baseMinHeight) { height = baseMinHeight;}

        baseTransform.sizeDelta = new Vector2(baseTransform.sizeDelta.x, height);

        typer.TypeText(node.dialogueText,.02f);
    }

    private void NextNode(string portFieldName = "")
    {
        foreach (NodePort p in graph.current.Ports)
        {
            if((portFieldName == "" && p.fieldName == "exit") || (p.fieldName == portFieldName))
            {
                graph.current = p.Connection.node as xBaseNode;
                break;
            }
        }

        var nodeType = graph.current.GetType();

        switch(graph.current)
        {
            case xDialogueNode s:
                DialogueBar.SetActive(true);
                CreateNewBox();
                break;
        }
    }


    private void OnEnable() {
        inputHandler.UIControls.UI.TextSkip.performed += context => SkipText();
    }

    private void OnDisable() {
        inputHandler.UIControls.UI.TextSkip.performed -= context => SkipText();
    }
}

//Create Focus Point
//Move the character relative to focus and distance variable
//First person
//Camera LookAt Focus
//Create UI
//Show text dynamic, with simple sound
//Skip appearing text
//Go to next node