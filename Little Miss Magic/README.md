# Portfolio

LINK TO BUILD: https://drive.google.com/file/d/1yIiKCyhPU1uALugld8RD8pvn9Fqwvmht/view?usp=sharing

Little Miss Magic was a project name for Roseine's game which aimed in the direction of magical life simulator, with a lot of dialogue content similar to Animal Crossing.

When inspecting the code, a good place to start is _Systems folder, where I have built many of the central systems for the game. The core block for the game would be the Core script and one of it's key roles would be to host most of the game's core systems. This would also help getting rid of currently quite rich Singleton usage.

The code architecture relies quite heavily on events to keep systems and object decoupled, especially on early development phase when things are still moving around quite a bit.

One architectural thing I also used was ScriptableObjects used as variable and EventVariable containers (can be found in 'Event Variables' and 'Global Variables' folders). I made some variations of similar concept and they would be as follows:
- Public variables that act like usual public variables but are contained in ScriptableObjects instead of Monobehaviours to avoid coupling objects unnecessarily and sharing information quickly between multiple objects of very different types.
- ReadOnly variables had main purpose to act as design variables, which, due to their [serializeField] attribute, could only be set in Unity editor and function as readonly in builds. This was an attempt to increase in-game data security.
- EventVariables were a small and streamlined alternative to bigger event systems, because in testing environments the testable objects could communicate directly through EventVariables without need to have an event system present in the scene. I also created proxies that could house EventVariables for quickly implemented mechanics like a proximity sensor that is used to switch lights on when player enters a Trigger Collider equipped with EventBool_Field (TriggerField might perhaps be a more suitable name).

I also started experimenting with Unit Data Oriented Technical Stack to increase the game performance. If you take a look at UI_Juicer in Scripts/UI/UI Juice, you can find in UI_Juicer.cs that I attempted to have the juicer run multithreaded with Jobs. However, I didn't achieve significant performance gains fast enough, so I put the DOTS approach on hold.

The DialogueSystem and other scripts located in 'Dialogue' folder use Inkly's Ink engine to run dialogue (https://www.inklestudios.com/ink/). My scripts have a lot of back and forth communication with Ink stories when running a dialogue (I've included the project's Ink Stories you can check out using Inky if you are interested.). An example scenario that is produced by with this method:
- Start of dialogue, when Unity looks up dialogue actor and opens appropriate dialogue.
- Player asks actor if there's work available and and Ink sends #RandomQuest tag that is parsed as a command for QuestSystem to generate a random quest with given actor.
- QuestSystem selects Collection Quest as a template and fills it in with dialogue actor and the necessary details for sensible phrases (like pronouns and genetives).
- QuestSystem returns generated story for DialogueSystem and DialogueSystem switches to read new quest story. After player either accepts or rejects the quest, Ink story sends #Return tag as a signal for dialogue to return to first story after (and only after) the current (quest) story can no longer progress.

I've included the project's Ink Stories you can check out using Inky if you are interested.
