namespace Mocks
{
    public class MockCharacter : Character
    {
        public string MockName;

        public override string GetName()
        {
            return MockName;
        }
    }
}
