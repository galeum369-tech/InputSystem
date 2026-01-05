using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    SkinnedMeshRenderer smr;
    Color originColor;

    int hashHit = Animator.StringToHash("Hit");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = smr.material.color;
    }

    public void TakeDamage(int damage)
    {
        print($"체력 {damage} 감소");

        //피격 애니메이션 제생
        anim.SetTrigger(hashHit);

        //StartCoroutine("HitColor"); //StopCorutine 가능
        StartCoroutine(HitColor()); //StopCorutine 불가 

    }

    IEnumerator HitColor()
    {
        //빨갛게 변경후 원색으로
        smr.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        smr.material.color = originColor;
    }
}
