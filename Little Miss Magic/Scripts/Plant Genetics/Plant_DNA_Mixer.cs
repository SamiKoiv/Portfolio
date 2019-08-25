using UnityEngine;
using TMPro;

public class Plant_DNA_Mixer : MonoBehaviour
{
    [Header("Debug")]
    public TextMeshPro plant1_genes;
    public TextMeshPro plant1_resultCode;

    public TextMeshPro plant2_genes;
    public TextMeshPro plant2_resultCode;

    public TextMeshPro hybrid_genes;
    public TextMeshPro hybrid_resultCode;

    [Header("Samples")]
    public Plant_DNA Sample1;
    public Plant_DNA Sample2;
    public Plant_DNA Result;

    GenePair[] temp = new GenePair[22];

    bool playMode;

    private void Awake()
    {
        Sample1.GenerateResult();
        plant1_genes.text = Sample1.GenePairs_ToString;
        plant1_resultCode.text = string.Empty + Sample1.ResultCode_ToString;

        Sample2.GenerateResult();
        plant2_genes.text = Sample2.GenePairs_ToString;
        plant2_resultCode.text = string.Empty + Sample2.ResultCode_ToString;

        playMode = true;
    }

    [ContextMenu("Randomize Sample 1")]
    public void RandomizeSample1()
    {
        if (!playMode)
            return;

        Sample1.Randomize();
        Sample1.GenerateResult();
        plant1_genes.text = Sample1.GenePairs_ToString;
        plant1_resultCode.text = string.Empty + Sample1.ResultCode_ToString;
    }

    [ContextMenu("Randomize Sample 2")]
    public void RandomizeSample2()
    {
        if (!playMode)
            return;

        Sample2.Randomize();
        Sample2.GenerateResult();
        plant2_genes.text = Sample2.GenePairs_ToString;
        plant2_resultCode.text = string.Empty + Sample2.ResultCode_ToString;
    }

    [ContextMenu("Mix")]
    public void Mix()
    {
        if (!playMode)
            return;

        Result.DNA = MixedSample;

        plant1_genes.text = Sample1.GenePairs_ToString;
        plant1_resultCode.text = string.Empty + Sample1.ResultCode_ToString;
        plant2_genes.text = Sample2.GenePairs_ToString;
        plant2_resultCode.text = string.Empty + Sample2.ResultCode_ToString;
        hybrid_genes.text = Result.GenePairs_ToString;
        hybrid_resultCode.text = string.Empty + Result.ResultCode_ToString;
    }

    GenePair[] MixedSample
    {
        get
        {
            for (int i = 0; i < Sample1.DNA.Length; i++)
            {
                temp[i].Set(
                    PickGene(Sample1.DNA[i], Random.Range(0, 2)),
                    PickGene(Sample2.DNA[i], Random.Range(0, 2)));
            }

            return temp;
        }
    }

    int PickGene(GenePair pair, int pick)
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                return pair.Gene1;
            case 1:
                return pair.Gene2;
        }

        Debug.Log("Error on gene randomization. Check pair randomization.");
        return 0;
    }
}
