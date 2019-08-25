using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation_CycleWalker : MonoBehaviour
{

    #region Movement
    Transform spinner;

    float moveSpeed = 3;
    float spinSpeed = 360;

    Vector3 moveVelocity;
    float moveTime = 0;

    Vector3 spinnerScale;
    #endregion

    Transform foot_L;
    //Transform foot_R;
    Vector3 groundedPosition_L;
    Vector3 groundedPosition_R;
    Transform footPosition_L;
    //Transform footPosition_R;

    void Awake()
    {
        GetSpinner();

        foot_L = transform.Find("foot_L");
        //foot_R = transform.Find("foot_R");
        footPosition_L = spinner.Find("footPosition_L");
        //footPosition_R = spinner.Find("footPosition_R");
    }

    void Start()
    {

    }

    void Update()
    {
        CheckInputAndMove();

        foot_L.position = new Vector3(footPosition_L.position.x, footPosition_L.position.y, foot_L.position.z);


        //if (footPosition_L.position.y > foot_L.position.y)
        //{
        //    foot_L.position = new Vector3(footPosition_L.position.x, footPosition_L.position.y, foot_L.position.z);
        //}
        //else
        //{
        //    groundedPosition_L = foot
        //}
    }

    void GetSpinner()
    {
        spinner = transform.Find("Spinner");
        spinnerScale = spinner.localScale;
    }
    void CheckInputAndMove()
    {
        moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity += Vector3.right;
            spinner.Rotate(Vector3.up * -spinSpeed * Time.deltaTime);
            spinner.localScale = spinnerScale * moveTime;

            if (spinner.localScale.x > spinnerScale.x)
            {
                spinner.localScale = spinnerScale;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVelocity += Vector3.left;
            spinner.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
            spinner.localScale = spinnerScale * moveTime;

            if (spinner.localScale.x > spinnerScale.x)
            {
                spinner.localScale = spinnerScale;
            }
        }

        if (moveVelocity != Vector3.zero)
        {
            transform.Translate(moveVelocity * moveSpeed * Time.deltaTime);
            moveTime += Time.deltaTime;
        }
        else
        {
            moveTime = 0;
            spinner.localScale = Vector3.one * 0.001f;
        }
    }
}
