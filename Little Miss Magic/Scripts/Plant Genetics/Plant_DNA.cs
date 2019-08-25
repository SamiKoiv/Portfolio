using System.Text;
using UnityEngine;

[System.Serializable]
public class Plant_DNA
{
    GenePair[] dna = new GenePair[22];
    int[] ResultCode = new int[22];

    public string GenePairs;
    public string Result;

    public GenePair[] DNA
    {
        get
        {
            return dna;
        }
        set
        {
            dna = value;
            GenerateResult();
            GenePairs = GenePairs_ToString;
            Result = ResultCode_ToString;
        }
    }

    public string GenePairs_ToString
    {
        get
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dna.Length; i++)
            {
                sb.Append(dna[i].Gene1);
                sb.Append(dna[i].Gene2);

                if (i < dna.Length - 1)
                    sb.Append(" ");
            }

            return sb.ToString();
        }
    }

    public string ResultCode_ToString
    {
        get
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ResultCode.Length; i++)
            {
                sb.Append(ResultCode[i]);
            }

            return sb.ToString();
        }
    }

    public void GenerateResult()
    {
        // #0 Primary Dominant Green
        ResultCode[0] = BipolarDominancy(dna[0]);

        // #1 Primary Color Extension
        ResultCode[1] = TripolarDominancy(dna[1]);

        // #2 Primary Tint
        ResultCode[2] = BipolarDominancy(dna[2]);

        // #3 Primary Darken
        ResultCode[3] = TripolarDominancy(dna[3]);

        // #4 Primary Lighten
        ResultCode[4] = TripolarDominancy(dna[4]);

        // #5 Primary Silvering
        ResultCode[5] = BipolarDominancy(dna[5]);

        // #6 Secondary Color Enable
        ResultCode[6] = BipolarDominancy(dna[6]);

        // #7 Secondary Dominant Green
        ResultCode[7] = BipolarDominancy(dna[7]);

        // #8 Secondary Color Extension
        ResultCode[8] = TripolarDominancy(dna[8]);

        // #9 Secondary Tint
        ResultCode[9] = BipolarDominancy(dna[9]);

        // #10 Secondary Darken
        ResultCode[10] = TripolarDominancy(dna[10]);

        // #11 Secondary Lighten
        ResultCode[11] = TripolarDominancy(dna[11]);

        // #12 Secondary Silvering
        ResultCode[12] = BipolarDominancy(dna[12]);

        // #13 Stalk Color
        ResultCode[13] = BipolarDominancy(dna[13]);

        // #14 Stalk Tint
        ResultCode[14] = BipolarDominancy(dna[14]);

        // #15 Gradient Pattern
        ResultCode[15] = BipolarDominancy(dna[15]);

        // #16 Edge Pattern
        ResultCode[16] = BipolarDominancy(dna[16]);

        // #17 Dot Pattern
        ResultCode[17] = BipolarDominancy(dna[17]);

        // #18 Blotch Pattern
        ResultCode[18] = BipolarDominancy(dna[18]);

        // #19 Size
        ResultCode[19] = PentapolarDominancy_MidWeighted(dna[19]);

        // #20 Growth Speed
        ResultCode[20] = PentapolarDominancy_MidWeighted(dna[20]);

        // #21 Attract
        ResultCode[21] = QuadripolarDominancy(dna[21]);
    }

    int BipolarDominancy(GenePair pair)
    {
        if (pair.Contains(0))
            return 0;
        else
            return 1;
    }

    int TripolarDominancy(GenePair pair)
    {
        if (pair.Contains(0))
            return 0;
        else if (pair.Contains(1))
            return 1;
        else
            return 2;
    }

    int QuadripolarDominancy(GenePair pair)
    {
        if (pair.Contains(0))
            return 0;
        else if (pair.Contains(1))
            return 1;
        else if (pair.Contains(2))
            return 2;
        else
            return 3;
    }

    int PentapolarDominancy_MidWeighted(GenePair pair)
    {
        if (pair.Contains(2) || pair.Contains(1, 3) || pair.Contains(0, 4))
            return 2;
        else if (pair.Contains(1))
            return 1;
        else if (pair.Contains(3))
            return 3;
        else if (pair.Contains(4))
            return 4;
        else
            return 0;
    }

    public void Randomize()
    {
        RandomizePair(0, 2);
        RandomizePair(1, 3);
        RandomizePair(2, 2);
        RandomizePair(3, 3);
        RandomizePair(4, 3);
        RandomizePair(5, 2);
        RandomizePair(6, 2);
        RandomizePair(7, 2);
        RandomizePair(8, 3);
        RandomizePair(9, 2);
        RandomizePair(10, 3);
        RandomizePair(11, 3);
        RandomizePair(12, 2);
        RandomizePair(13, 3);
        RandomizePair(14, 3);
        RandomizePair(15, 3);
        RandomizePair(16, 3);
        RandomizePair(17, 3);
        RandomizePair(18, 2);
        RandomizePair(19, 5);
        RandomizePair(20, 5);
        RandomizePair(21, 4);

        GenerateResult();
        GenePairs = GenePairs_ToString;
        Result = ResultCode_ToString;
    }

    void RandomizePair(int index, int maxRange)
    {
        dna[index].Set(Random.Range(0, maxRange), Random.Range(0, maxRange));
    }
}