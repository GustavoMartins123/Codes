using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand_UI : MonoBehaviour
{
    [SerializeField]Animator anim;
    [SerializeField]Transform sword;

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.9f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, sword.position);
    }
}
