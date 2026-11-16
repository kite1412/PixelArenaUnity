using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public int damage = 10;
    public float range = 1f;
    public float cooldown = 0.5f;
    public GameObject effectPrefab;
}
