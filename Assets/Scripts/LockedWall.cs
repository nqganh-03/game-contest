using UnityEngine;
using System.Collections.Generic;

public class LockedWall : MonoBehaviour
{
    [Header("Keys Required to Unlock this Door")]
    public List<string> requiredKeys = new List<string>();
    public bool isUnlocked = false;

    [Header("Optional Animator")]
    public Animator animator;

    // Kiểm tra Player có đủ chìa
    public bool CanUnlock(List<string> playerKeys)
    {
        foreach (string key in requiredKeys)
        {
            if (!playerKeys.Contains(key))
                return false;
        }
        return true;
    }

    public void Unlock()
    {
        if (isUnlocked) return;

        isUnlocked = true;

        if (animator != null)
            animator.SetTrigger("Open");
        else
            gameObject.SetActive(false); // cửa biến mất

        // Collider sẽ bị disable để Player đi qua
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Debug.Log("Unlocked door requiring keys: " + string.Join(", ", requiredKeys));
    }
}
