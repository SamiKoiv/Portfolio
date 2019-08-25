using UnityEngine;

[CreateAssetMenu(menuName = "Tags/Character Tag")]
public class CharacterTag : Tag
{
    [SerializeField] Gender gender;
    [SerializeField] Color dialogueColor;
    public Color DialogueColor { get { return dialogueColor; } }

    public string Pronoun()
    {
        if (gender == Gender.He)
            return "he";
        else
            return "she";
    }

    public string Genetive()
    {
        if (gender == Gender.He)
            return "his";
        else
            return "her";
    }

    public string Partitive()
    {
        if (gender == Gender.He)
            return "him";
        else
            return "her";
    }

    public enum Gender
    {
        He,
        She
    }
}
