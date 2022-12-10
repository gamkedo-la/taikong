using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Triggers : MonoBehaviour
{
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource bossMusic;
    [SerializeField] GameObject player;
    
    float current_player_position;
    bool boss_music = false;

    void Start() {
        
    }
    void FixedUpdate()
    {
        current_player_position = player.transform.root.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position;

        if (!boss_music && current_player_position > 3900) {
            boss_music = true;
            levelMusic.Stop();
            bossMusic.Play();
        }
    }
}
