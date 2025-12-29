using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Fading Flame/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public int levelNumber = 1;
    public string levelName = "Level 1";

    [Header("Timer Settings")]
    public float startingTime = 30f;

    [Header("Obstacle Settings")]
    public float obstacleBasePenalty = 2f;
    public float obstaclePenaltyIncrease = 0.5f;

    [Header("Task Settings")]
    public float taskTimeBonus = 3f;

    [Header("Level Description")]
    [TextArea(3, 5)]
    public string description = "Navigate through the maze and reach the finish line!";
}