using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    Animator m_Animator;
    float old_posX, old_posY, old_posZ;
    void Start()
    {
        old_posX = transform.position.x;
        old_posY = transform.position.y;
        old_posZ = transform.position.z;
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (old_posY == transform.position.y) { 
            if (old_posX != transform.position.x || old_posZ != transform.position.z)
            {
                m_Animator.Play("Walk");
            }
            else
            {
                m_Animator.Play("Idle");
            }
            
        }
        else
        {

        }
        old_posX = transform.position.x;
        old_posZ = transform.position.z;
        old_posY = transform.position.y;
    }
}