using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject dialogueRoot;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text lineText;
    [SerializeField] private Button option1Button;
    [SerializeField] private TMP_Text option1Label;
    [SerializeField] private Button option2Button;
    [SerializeField] private TMP_Text option2Label;

    private string currentNpcName;
    private Action onClose;
    private DialogueNode currentNode;
    private Action<DialogueActionType> onAction;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        if (dialogueRoot != null)
        {
            dialogueRoot.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void StartDialogue(
        string npcName,
        DialogueNode startNode,
        Action onClosed = null,
        Action<DialogueActionType> onDialogueAction = null)
    {
        if (startNode == null)
        {
            return;
        }

        IsOpen = true;
        currentNpcName = npcName;
        currentNode = startNode;
        onClose = onClosed;
        onAction = onDialogueAction;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (dialogueRoot != null)
        {
            dialogueRoot.SetActive(true);
        }

        ShowNode(currentNode);
    }

    private void ShowNode(DialogueNode node)
    {
        if (node == null)
        {
            return;
        }

        currentNode = node;

        if (nameText != null)
        {
            nameText.text = currentNpcName;
        }

        if (lineText != null)
        {
            lineText.text = node.line;
        }

        SetupChoice(option1Button, option1Label, node.option1);
        SetupChoice(option2Button, option2Label, node.option2);
    }

    private void SetupChoice(
        Button button,
        TMP_Text label,
        DialogueChoice choice)
    {
        if (button == null)
        {
            return;
        }

        button.onClick.RemoveAllListeners();

        if (choice == null)
        {
            button.gameObject.SetActive(false);
            return;
        }

        button.gameObject.SetActive(true);

        if (label != null)
        {
            label.text = choice.text;
        }

        button.onClick.AddListener(() => SelectChoice(choice));
    }

    private void SelectChoice(DialogueChoice choice)
    {
        if (choice == null)
        {
            return;
        }

        if (choice.action != DialogueActionType.None)
        {
            onAction?.Invoke(choice.action);
        }

        if (choice.next != null)
        {
            ShowNode(choice.next);
        }
        else
        {
            Close();
        }
    }

    public void Close()
    {
        IsOpen = false;

        if (dialogueRoot != null)
        {
            dialogueRoot.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        try
        {
            onClose?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Close Error Blocked: " + e.Message);
        }

        onClose = null;
        onAction = null;
        currentNode = null;
    }
}
