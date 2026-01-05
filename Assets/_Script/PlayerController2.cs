using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Animator anim;

    //Animator 해시 ID
    int hashMoveX;
    int hashMoveY;
    int hashJump;
    int hashSprint;
    int hashAttack;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        //애니메이터 Hash Init
        hashMoveX = Animator.StringToHash("moveX");
        hashMoveY = Animator.StringToHash("moveY");
        hashJump = Animator.StringToHash("Jump");
        hashSprint = Animator.StringToHash("Sprint");
        hashAttack = Animator.StringToHash("Attack");
    }

    private void OnEnable()
    {
        //InputManager.OnMove += HandleMove;
        InputManager.OnJump += HandleJump;
        //InputManager.OnSprint += HandleSprint;
        InputManager.OnAttack += HandleAttack;
    }
    private void OnDisable()
    {
        //InputManager.OnMove -= HandleMove;
        InputManager.OnJump -= HandleJump;
        //InputManager.OnSprint -= HandleSprint;
        InputManager.OnAttack -= HandleAttack;
    }

    void HandleMove(Vector2 input)
    {
        print($"이동 : {input}");
    }
    void HandleJump()
    {
        print("점프");
    }
    void HandleSprint(bool obj)
    {
        if (obj) print("왼쪽 쉬프트 눌림");
        else print("왼쪽 쉬프트 안눌림");
    }
    void HandleAttack()
    {
        print("공격");
        anim.SetTrigger(hashAttack);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
