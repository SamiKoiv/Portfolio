using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer_ClosestQuest : MonoBehaviour
{
    new Transform transform;
    Transform player;

    QuestManager questManager;
    GameObject pointer;

    public float LerpSpeed = 10;

    void Awake()
    {
        transform = gameObject.transform;
        questManager = Core.instance.QuestManager;
        pointer = transform.GetChild(0).gameObject;
    }

    void Start()
    {
        player = Core.instance.GetPlayer();
    }

    void LateUpdate()
    {
        transform.position = player.position;

        if (questManager.QuestsOpen())
        {
            pointer.SetActive(true);
        }
        else
        {
            pointer.SetActive(false);
        }

        Quaternion lookRotation = Quaternion.LookRotation(questManager.GetClosestTargetPosition() - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, LerpSpeed * Time.deltaTime);
        
        //transform.LookAt(questManager.GetClosestTargetPosition());
    }

    void OnEnable()
    {
        questManager.AddQuestGameObject(pointer);
    }

    void OnDisable()
    {
        questManager.RemoveQuestGameObject(pointer);
    }
}
