using Ink.Runtime;
using UnityEngine;

public class SealedStory : MonoBehaviour
{
    Story story;

    public SealedStory(TextAsset Json)
    {
        story = new Story(Json.text);
    }

    public SealedStory(TextAsset Json, VariablesState variableState)
    {
        story = new Story(Json.text);

        foreach(string s in variableState)
        {
            story.variablesState[s] = variableState[s];
        }
    }

    public string Continue()
    {
        return story.Continue();
    }

    public string ContinueMaximally()
    {
        return story.ContinueMaximally();
    }

    public void ChoosePath(string path)
    {
        story.ChoosePathString(path);
    }

    public void Choose(int i)
    {
        story.ChooseChoiceIndex(i);
    }
}
