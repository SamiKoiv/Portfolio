public abstract class Equipment : IGetName
{
    protected string _name;

    public virtual string GetName()
    {
        return _name;
    }
}
