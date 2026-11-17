using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string stringName = "Enemy";
    public int health = 10;
    public float speed = 1f;
    public int height = 32;
    public int width = 32;
    public GameObject spritePrefab;
}
