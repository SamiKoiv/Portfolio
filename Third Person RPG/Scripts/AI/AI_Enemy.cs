using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{

    /// <summary>
    /// 
    /// Naming example:
    /// 
    /// g_variable  GameObject
    /// t_variable  Transform
    /// c_variable  Component
    /// h_variable  Hash
    /// _variable   private simple value
    /// variable    public simple value
    /// 
    /// </summary>

    Transform player;

    NavMeshAgent agent;
    Animator anim;
    bool hasAnimator;

    int speedHash;
    int attackHash;
    int deathHash;

    Component[] colliders;

    public enum characterState
    {
        Active,
        KnockedOut,
        Dead
    };

    characterState state = characterState.Active;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren(typeof(Collider), true);

        if (anim != null)
        {
            hasAnimator = true;
            speedHash = Animator.StringToHash("Speed");
            attackHash = Animator.StringToHash("Attack");
            deathHash = Animator.StringToHash("Death");
        }
    }

    void Start()
    {
        player = Core.instance.GetPlayer();
    }

    void Update()
    {
        // Movement and Animation

        if (state == characterState.Active)
        {
            if (hasAnimator)
            {
                anim.SetFloat(speedHash, agent.velocity.magnitude);

                if (Vector3.Distance(transform.position, player.position) < 4f)
                {
                    if (hasAnimator)
                        anim.SetTrigger(attackHash);
                }

                AnimatorClipInfo[] info = anim.GetCurrentAnimatorClipInfo(0);

                if (info[0].clip.name == "Attack")
                {
                    agent.enabled = false;
                }
                else
                {
                    agent.enabled = true;
                    agent.SetDestination(player.position);
                }
            }
            else
            {
                agent.enabled = true;
                agent.SetDestination(player.position);
            }

        }

    }


    // --------------------------------------------------------------------------------------

    // Damage Distribution

    public void Death()
    {
        state = characterState.Dead;

        if (hasAnimator)
            anim.SetTrigger(deathHash);

        agent.enabled = false;

        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
    }

}
