public interface ICharacterFactory
{
    Character Next(CharacterRecipe recipe);
    Character Next(string name, Stats stats);

}
