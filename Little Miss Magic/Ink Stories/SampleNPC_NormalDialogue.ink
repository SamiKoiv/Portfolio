VAR npc = "NPC"
VAR player = "PLAYER"
VAR onQuest = false

=== Interact ===
{npc}: Hi pal! How are you.
->Main

=== Main ===
{player}:
+ How are you?
    ->Mood
+ I want to talk about something. #Topics
    ->Topics

+ Gotta fly!
    {npc}: See ya!
    ->END

=== Mood ===
{->FirstResponse|->SecondResponse}

= FirstResponse
{npc}: Oh, I'm fine. I've just been taking naps under a big tree all day.
You know, chilling and stuff.
->Main

= SecondResponse
{npc}: The same as always.
->Main

=== Topics ===
{npc}: Sure thing.
{player}:
+ Do you have any work for me?
    ->Quest
        
+ Nevermind
    ->Main


=== Quest ===
{not onQuest:
    {npc}: Actually there is this one thing. #RandomQuest
    ->END
-else:
    {npc}: Nothing at the moment.
    ->Main
}
->END