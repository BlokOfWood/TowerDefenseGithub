using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyType")]
public class EnemyType : ScriptableObject {
    public string name;
    public int damage;
    public float speed;
    
}