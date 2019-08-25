using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Events : ScriptableObject
{
    //int comboLength;
    //float critLo;
    //float critHi;

    public void ComboLength_Set(int comboLength)
    {
        //this.comboLength = comboLength;
        combo = new ButtonInput[comboLength];
        resetCombo = new ButtonInput[comboLength];
        criticals = new bool[comboLength];
        resetCriticals = new bool[comboLength];
    }

    void SetCritLimit(float critLo, float critHi)
    {
        //this.critLo = critLo;
        //this.critHi = critHi;
    }

    #region HP

    [SerializeField] float hp;
    public Event_Function HP_Changed = new Event_Function();

    public void HP_Set(float HP)
    {
        this.hp = HP;
        HP_Changed.Invoke();
    }

    public float HP_Get()
    {
        return hp;
    }

    #endregion

    #region Hit Count

    [SerializeField] int hitCount;
    public Event_Function HitCount_Changed = new Event_Function();

    public void HitCount_Set(int Combo)
    {
        this.hitCount = Combo;
        HitCount_Changed.Invoke();
    }

    public int HitCount_Get()
    {
        return hitCount;
    }

    public void HitCount_Reset()
    {
        HitCount_Set(0);
    }

    #endregion

    #region Combo Feed

    [SerializeField] ButtonInput[] combo;
    public Event_Function Combo_Changed = new Event_Function();
    ButtonInput[] resetCombo;

    public void Combo_Set(ButtonInput[] comboInputs)
    {
        this.combo = comboInputs;
        Combo_Changed.Invoke();
    }

    public ButtonInput[] Combo_Get()
    {
        return combo;
    }

    public void Combo_Reset()
    {
        Combo_Set(resetCombo);
    }

    #endregion

    #region Finisher

    [SerializeField] Finisher finisher;

    public Event_Function Finisher_Changed = new Event_Function();

    public void Finisher_Set(Finisher finisher)
    {
        this.finisher = finisher;
        Finisher_Changed.Invoke();
    }

    public Finisher Finisher_Get()
    {
        return finisher;
    }

    public void Finisher_Reset()
    {
        Finisher_Set(0);
    }

    public void Ping()
    {
        Debug.Log(name + " PING!");
    }

    #endregion

    #region Charge

    float charge;
    bool critical;
    public Event_Function Charge_Changed = new Event_Function();

    public void Charge_Set(float charge, bool critical)
    {
        this.charge = charge;
        this.critical = critical;
        Charge_Changed.Invoke();
    }

    public float Charge_Get()
    {
        return charge;
    }

    public bool Critical_Get()
    {
        return critical;
    }

    public void Charge_Reset()
    {
        charge = 0;
        critical = false;
        Charge_Changed.Invoke();
    }

    #endregion

    #region Critical Hits

    public bool[] criticals;
    public Event_Function Criticals_Changed = new Event_Function();
    public Event_Function Critical_Hit = new Event_Function();
    bool[] resetCriticals;

    public void Criticals_Set(bool[] criticals)
    {
        this.criticals = criticals;
        Criticals_Changed.Invoke();
    }

    public void Criticals_Reset()
    {
        criticals = resetCriticals;
    }

    #endregion

    #region Open Quests

    OpenQuest _lockedQuest;
    List<OpenQuest> _openQuests = new List<OpenQuest>();
    public Event_Function OpenQuests_Changed = new Event_Function();
    public Event_Function LockedQuest_Changed = new Event_Function();

    //-----------------------------------------------------------------------------

    // OPEN QUESTS

    public void OpenQuests_Set(List<OpenQuest> quests)
    {
        _openQuests = quests;
        OpenQuests_Changed.Invoke();
    }

    public List<OpenQuest> OpenQuests_Get()
    {
        return _openQuests;
    }

    //-----------------------------------------------------------------------------

    // LOCKED QUESTS

    public void LockedQuest_Lock(OpenQuest quest)
    {
        _lockedQuest = quest;
        LockedQuest_Changed.Invoke();
    }

    public OpenQuest LockedQuest_Get()
    {
        return _lockedQuest;
    }

    public void LockedQuest_Release()
    {
        _lockedQuest = null;
        LockedQuest_Changed.Invoke();
    }

    #endregion
}
