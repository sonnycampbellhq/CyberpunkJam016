using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogue;
    [SerializeField] private string npcName;
    [SerializeField] private DialogueNode startingNode;

    [Header("Dialogue Actions")]
    [SerializeField] private Doors doors;

    private void Awake()
    {
        if (dialogue == null)
        {
            dialogue = FindFirstObjectByType<DialogueManager>();
        }
    }

    public void StartDialogue(PlayerController player)
    {
        if (dialogue == null || startingNode == null)
        {
            return;
        }

        dialogue.StartDialogue(
            npcName,
            startingNode,
            onClosed: () =>
            {
                player.setIsInDialogue(false);
            },
            onDialogueAction: HandleDialogueAction
        );
    }

    private void HandleDialogueAction(DialogueActionType action)
    {
        switch (action)
        {
            case DialogueActionType.None:
                break;

            case DialogueActionType.OpenDoors:
                if (doors != null)
                {
                    doors.DoorsOpen = true;
                }
                break;
        }
    }
}
