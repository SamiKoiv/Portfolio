namespace Mocks
{
    public class MockWeapon : Weapon
    {
        public string MockName;

        public override string GetName()
        {
            return MockName;
        }
    }
}
