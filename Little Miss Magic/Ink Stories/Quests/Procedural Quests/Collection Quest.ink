VAR player = "PLAYER"
VAR npc = "NPC"
VAR partitive = "THEM"
VAR genetive = "THEIR"
VAR itemName = "ITEM"
VAR amount = "QUANTITY"

=== Quest ===

= QuestName
{npc}: Daily Deliveries
->END

= Description
{npc} is preparing for have dinner with {genetive} family and needs you to get deliver {partitive} some supplies.
->END

= StartDialogue
{npc}: I'm feeling hungry but my fridge is empty as are my pockets.
Could you help me find {amount} {itemName}.

{player}:
+ I'll see what I can do.   #StartQuest
    {npc}: Thanks!            #Return
    ->END
+ Maybe some other time.    #RejectQuest
    {npc}: I understand!      #Return
    ->END

= Topic
I want to talk about the task you gave me.
->END

= State
{player}: Can you tell me about the task you gave me?
{npc}: I'm counting on you to find me the {amount} {itemName}.
I hope it's not too much trouble. #Return
->END

= Complete
{player}: I've got your {amount} {itemName} right here!
{npc}: Oh, you got them! Thank you so much {player}! I knew I could count on you.
Here's a reward for your efforts. #Return
->END

= Fail
// Cannot fail
->END