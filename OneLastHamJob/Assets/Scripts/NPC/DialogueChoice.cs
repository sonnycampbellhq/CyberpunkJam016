using UnityEngine;

public enum DialogueActionType
{
    None
}

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public DialogueNode next;   

    [Header("Optional Action")]
    public DialogueActionType action;
}
