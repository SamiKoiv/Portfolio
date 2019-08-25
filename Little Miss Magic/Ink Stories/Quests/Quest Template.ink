VAR player = "PLAYER"
VAR npc = "NPC"
VAR partitive = "THEM"
VAR genetive = "THEIR"
//-------------------------------

/*
Tags in use:
    StartQuest      - Start quest and add it to QuestLog. Use only once!
    RejectQuest     - Reject quest.
    Return          - Return to original dialogue main section on END.
*/

=== Quest ===

= QuestName
// Used in QuestLog UI
{npc}: Insert quest name.
->END

= Description
// Used in QuestLog UI
Insert quest description.
->END

= StartDialogue
// This dialogue initiates the quest so fill it with appropriate story elements and quest objectives.
{npc}: Insert quest starting dialogue.

{player}:
+ I accept.   #StartQuest #Return
    ->END
+ I reject.    #RejectQuest #Return
    ->END

= Topic
// This is a one liner to bring up the quest after start.
I want to talk about the task you gave me.
->END

= State
// If quest is not yet completed. NPC gives us this in progress message.
{player}: Can you tell me about the task you gave me?
{npc}: Insert state message. #Return
->END

= Complete
// If completion objectives are met, quest ends as completed and this message shows.
{player}: Insert quest completion dialogue #Return
->END

= Fail
// If fail objectives are met, quest ends as failed and this message shows.
{player}: Insert quest completion dialogue #Return
->END