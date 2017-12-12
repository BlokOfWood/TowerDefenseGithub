using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManagement : MonoBehaviour {

    public int StartHealth;
    public int Health;

    public int WaveNumber;
    public TextMeshProUGUI lifecounter;
    
	void Start () {
        Health = StartHealth;
        lifecounter.text = Health.ToString();
	}

    public void EnemyReachedEnd(int Damage) {
        Health -= Damage;
        lifecounter.text = Health.ToString();
    }
}