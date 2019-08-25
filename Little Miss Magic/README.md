# Portfolio

Little Miss Magic was a project name for Roseine's game which aimed in the direction of magical life simulator, with a lot of dialogue content similar to Animal Crossing.

When inspecting the code, a good place to start is _Systems folder, where I have built many of the central systems for the game. The core block for the game would be the Core script and one of it's key roles would be to host most of the game's core systems. This would also help getting rid of quite rich Singleton usage.

The code architecture relies quite heavily on events to keep systems and object decoupled, especially on early development phase when things are still moving around quite a bit.

One architectural thing I also used was ScriptableObjects used as variable and EventVariable containers (can be found in 'Event Variables' and 'Global Variables' folders). I made some variations of similar concept and they would be as follows:
- Public variables that act like usual public variables but are contained in ScriptableObjects instead of Monobehaviours to avoid coupling objects unnecessarily and sharing information quickly between multiple objects of very different types.
- ReadOnly variables had main purpose to act as design variables, which, due to their [serializeField] attribute, could only be set in Unity editor and function as readonly in builds. This was an attempt to increase in-game data security.
- EventVariables were a small and streamlined alternative to bigger event systems, because in testing environments the testable objects could communicate directly through EventVariables without need to have an event system present in the scene.