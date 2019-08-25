[System.Serializable]
public struct GenePair
{
    public int Gene1;
    public int Gene2;

    public void Set(int gene1, int gene2)
    {
        Gene1 = gene1;
        Gene2 = gene2;
    }

    public bool Contains(int a)
    {
        if (Gene1 == a)
            return true;

        if (Gene2 == a)
            return true;

        return false;
    }

    public bool Contains(int a, int b)
    {
        if (Gene1 == a && Gene2 == b)
            return true;

        if (Gene1 == b && Gene2 == a)
            return true;

        return false;
    }
}
