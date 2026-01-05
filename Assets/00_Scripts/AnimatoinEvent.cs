using UnityEngine;

public class AnimatoinEvent : MonoBehaviour
{
    public GameObject attackHitbox;

    public void OnAttackHitbox()
    {
        attackHitbox.SetActive(true);
    }
}
