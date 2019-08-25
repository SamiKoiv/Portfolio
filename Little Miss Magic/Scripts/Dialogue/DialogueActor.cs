using Ink.Runtime;
using UnityEngine;

public class DialogueActor : MonoBehaviour, IInteractable
{
    [SerializeField] TextAsset inkJsonAsset;
    [SerializeField] string storyPath;
    [SerializeField] Vector3 popupOffset;
    [SerializeField] CharacterTag characterTag;
    [SerializeField] bool gizmos;

    Story story;

    private void Awake()
    {
        if (inkJsonAsset != null)
            story = new Story(inkJsonAsset.text);
    }

    public string GetInteractionMessage()
    {
        return "Talk";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public Vector3 GetPopupOffset()
    {
        return popupOffset;
    }

    public void Interact()
    {
        if (story == null)
            return;

        if (storyPath != string.Empty)
            story.ChoosePathString(storyPath);
        else
            story.ChoosePathString("Interact");

        DialogueSystem.Instance.StartDialogue(story, characterTag);
    }

    public void InteractLong()
    {

    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + popupOffset, 0.1f);
    }
}
