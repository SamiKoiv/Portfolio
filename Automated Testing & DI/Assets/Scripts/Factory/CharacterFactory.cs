public class CharacterFactory : ICharacterFactory
{
    public Character Next(CharacterRecipe recipe)
    {
        Character character = new Character(recipe.GetName(), recipe.GetStats());
        return character;
    }

    public Character Next(string name, Stats stats)
    {
        Character character = new Character(name, stats);
        return character;
    }
}
