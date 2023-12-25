using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Core/Database")]
public class Database : ScriptableObject
{
    [Header("Player")]
    public float moveSpeed = 5f;
    [Header("Ball")]
    public float ballSpeed = 8f;
    public float maxBallSpeed = 40f;
    public float minBallSpeed = -40f;
    public LayerMask wallLayer;
    [Header("Managers")]
    public float timeToSpawm = 2f;
    [Header("Prefabs GameObject")]
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject ballObject;
    [Header("Partycles")]
    public GameObject ballDestroyPartycle;
    [Header("Sounds")]
    public AudioClip impactBall;
    [Header("Maps Sounds")]
    [Tooltip("M1 - Map One...")]
    public AudioClip destroyBallM1;
    public AudioClip winnerM1;
    public AudioClip timerOutM1;

}
