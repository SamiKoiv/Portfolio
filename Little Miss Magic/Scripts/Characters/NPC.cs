using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] Vector3 popupOffset;
    [SerializeField] CharacterTag characterTag;
    [SerializeField] bool gizmos;

    Story story;

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
            story = Core.Instance.DB.StoryDB.GetCharacterStory(characterTag);

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
