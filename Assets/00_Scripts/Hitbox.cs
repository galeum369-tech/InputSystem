using System;
using System.Collections;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    //적 레이어
    int enemyLayer;

    private void Awake()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }


    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"{other.name}에 닿았다");
        //충돌처리
        //적 찾아서 적한테 피깍아라
        if (other.gameObject.layer == enemyLayer)
        {
            other.GetComponent<Enemy>().TakeDamage(10);
        }
        else return;
    }
}
