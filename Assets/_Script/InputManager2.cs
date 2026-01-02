using System;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager2 : MonoBehaviour
{
    //옵저버 패턴 : C# delegate/event, Action을 사용하는 경우 가장 대표적인 것이 옵저버패턴이다
    //옵저버 패턴이란 객체의 상태변화를 관찰해서 상태가 변할 때 그와 연관된 객체들에게 알림을 보내는 디자인패턴이다
    //결국 인풋매니저에서 관리하려는 Action들을 등록해두고 관찰중인 객체들에서 발생하는 모든 이벤트에
    //변화가 생기면 자동으로 알림을 보낸다


    //파사드 패턴 : 어렵게 생각할것 없이, 복합한 내부코드를 사용하기 쉽게 변경을 하거나
    //꼭 사용해야하는 간단한 인터페이스만(함수) 노출시킨다 -> 추상클래스 인터페이스 아님

    private PlayerInput pi;
    //외부 공개용 인터페이스, 옵저버들
    public static event Action<Vector2> OnMove;         //이동입력 이벤트
    public static event Action OnJump;                  //점프입력 이벤트
    public static event Action OnAttack;                //공격입력 이벤트
    public static event Action<bool> OnSprint;          //달리기입력 이벤트
    // 더 필요한 경우 Action들을 추가하면 된다

    //Input System의 액션들(외부에서 몰라도 됨)
    private InputAction moveAciton;
    private InputAction jumpAciton;
    private InputAction attackAciton;
    private InputAction sprintAction;
    //콜백 함수들을 저장할 변수들
    private Action<InputAction.CallbackContext> onMovePerformed;
    private Action<InputAction.CallbackContext> onMoveCanceled;
    private Action<InputAction.CallbackContext> onJumpPerformed;
    private Action<InputAction.CallbackContext> onAttackPerformed;
    private Action<InputAction.CallbackContext> onSprintPerformed;
    private Action<InputAction.CallbackContext> onSprintCanceled;

    private void OnEnable()
    {
        //PlayerInput컴포넌트 초기화
        pi = GetComponent<PlayerInput>();
        pi.defaultActionMap = "Player";
        pi.defaultControlScheme = "Keboard&Mouse"; //Default
        pi.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;

        //각각의 액션들 찾기
        moveAciton = pi.actions.FindAction("Move");
        jumpAciton = pi.actions.FindAction("Jump");
        attackAciton = pi.actions.FindAction("Attack");
        sprintAction = pi.actions.FindAction("Sprint");

        //MoveAction 콜백 등록
        if (moveAciton != null)
        {
            onMovePerformed = ctx =>
            {
                Vector2 input = ctx.ReadValue<Vector2>(); OnMove?.Invoke(input);
            };
            onMoveCanceled = ctx => { Vector2 input = Vector2.zero; OnMove?.Invoke(input); };
            moveAciton.performed += onMovePerformed;
            moveAciton.canceled += onMoveCanceled;
        }
        //점프 액션 콜백등록
        if (jumpAciton != null)
        {
            onJumpPerformed = ctx => OnJump?.Invoke();
            jumpAciton.performed += onJumpPerformed;
        }

        if (sprintAction != null)
        {
            onSprintPerformed = ctx => OnSprint?.Invoke(true);
            onSprintCanceled = ctx => OnSprint?.Invoke(false);
            sprintAction.performed += onSprintPerformed;
            sprintAction.canceled += onSprintCanceled;
        }

        if (attackAciton != null)
        {
            onAttackPerformed = ctx => OnAttack?.Invoke();
            attackAciton.performed += onAttackPerformed;
        }

    }
    private void OnDisable()
    {
        //Move Action 해제
        if (moveAciton != null)
        {
            if (onMovePerformed != null)
            {
                moveAciton.performed -= onMovePerformed;
                onMovePerformed = null;
            }
            if (onMoveCanceled != null)
            {
                moveAciton.canceled -= onMoveCanceled;
                onMoveCanceled = null;
            }
        }

        if (jumpAciton != null)
        {
            jumpAciton.performed -= onJumpPerformed;
            onJumpPerformed = null; 
        }

        if (sprintAction != null)
        {
            if (onSprintPerformed != null)
            {
                sprintAction.performed -= onSprintPerformed;
                onSprintPerformed = null;
            }
            if (onSprintCanceled != null)
            {
                sprintAction.canceled -= onSprintCanceled;
                onSprintCanceled = null;
            }
        }

        if (attackAciton != null)
        {
            attackAciton.performed -= onAttackPerformed;
            onAttackPerformed = null;
        }
    }
}
