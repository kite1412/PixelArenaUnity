using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Scriptable Objects/AttackData")]
public class AttackData : ScriptableObject
{
    public int damage = 5;
    public float range = 1f;
    public float width = 1f;
    public float cooldown = 2f;
    public GameObject effectPrefab;
}
