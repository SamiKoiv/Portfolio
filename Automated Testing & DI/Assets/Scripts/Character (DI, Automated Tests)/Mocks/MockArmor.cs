namespace Mocks
{
    public class MockArmor : Armor
    {
        public string MockName;

        public override string GetName()
        {
            return MockName;
        }
    }
}

