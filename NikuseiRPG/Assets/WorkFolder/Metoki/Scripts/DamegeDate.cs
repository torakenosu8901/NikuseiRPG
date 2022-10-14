using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Create DamegeData")]
public class DamegeData : ScriptableObject
{
    // �f�[�^�Q�̐擪��string�ɂ��Ė��O���ɐݒ肷���Inspector�Ō����Ƃ��ɍ���TOP�ɕ\�������̂Ō��₷���Ȃ�܂��B
    [SerializeField]
    private string WazaName = "Attack";

    // private�ł�[SerializeField]�����邱�Ƃ�Inspector�Ŋm�F�ł���悤�ɂȂ�܂��B
    [SerializeField]
    float ATK = 1;
}