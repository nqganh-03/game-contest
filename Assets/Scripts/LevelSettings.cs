using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [Header("Level Configuration")]
    public LevelData levelData;

    [Header("Manual Override (if no LevelData)")]
    public float startingTime = 30f;
    public float obstacleBasePenalty = 2f;
    public float obstaclePenaltyIncrease = 0.5f;
    public float taskTimeBonus = 3f;

    void Awake()
    {
        // Apply level settings before other scripts initialize
        ApplyLevelSettings();
    }

    void ApplyLevelSettings()
    {
        // Use LevelData if assigned, otherwise use manual override
        float time = levelData != null ? levelData.startingTime : startingTime;
        float basePenalty = levelData != null ? levelData.obstacleBasePenalty : obstacleBasePenalty;

        // Apply timer settings
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.timeRemaining = time;
        }

        // Apply obstacle settings
        Obstacle.ResetPenalty(basePenalty);

        // Update all obstacles in scene
        UpdateObstaclesInScene();

        // Update all tasks in scene
        UpdateTasksInScene();

        Debug.Log($"Level settings applied: Time={time}s, Penalty={basePenalty}s");
    }

    void UpdateObstaclesInScene()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        float basePenalty = levelData != null ? levelData.obstacleBasePenalty : obstacleBasePenalty;
        float penaltyInc = levelData != null ? levelData.obstaclePenaltyIncrease : obstaclePenaltyIncrease;

        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.basePenalty = basePenalty;
            obstacle.penaltyIncrease = penaltyInc;
        }
    }

    void UpdateTasksInScene()
    {
        TaskPoint[] tasks = FindObjectsOfType<TaskPoint>();
        float bonus = levelData != null ? levelData.taskTimeBonus : taskTimeBonus;

        foreach (TaskPoint task in tasks)
        {
            task.timeBonus = bonus;
        }
    }
}