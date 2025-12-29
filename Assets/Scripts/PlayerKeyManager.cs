using UnityEngine;
using System.Collections.Generic;

public class PlayerKeyManager : MonoBehaviour
{
    [Header("Player's collected keys")]
    public List<string> keys = new List<string>();

    void OnTriggerEnter2D(Collider2D other)
    {
        // Nhặt chìa
        if (other.CompareTag("Key"))
        {
            Key key = other.GetComponent<Key>();
            if (key == null)
            {
                Debug.LogWarning("Key component missing!");
                return;
            }

            if (!keys.Contains(key.keyID))
            {
                keys.Add(key.keyID);
                Debug.Log("Picked up key: " + key.keyID);
            }

            Destroy(other.gameObject);

            // Kiểm tra tất cả cửa trong scene
            LockedWall[] walls = FindObjectsOfType<LockedWall>();
            foreach (var wall in walls)
            {
                if (!wall.isUnlocked && wall.CanUnlock(keys))
                {
                    wall.Unlock();
                }
            }
        }
    }
}
