using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private struct Tiles{
        [SerializeField]
        private Tilemap tilemap;
        // Note: You could disable collision with Rigidbody2D.simulated.
        // If you don't like this method, you can also enable/disable the collider
        // components directly.
        [SerializeField]
        private Rigidbody2D rigidbody;
        public void Enable(){
            // TODO: Define "enabled" state for a set of tiles.
        }
        public void Disable(){
            // TODO: Define "disabled" state for a set of tiles.
        }
        public void Toggle(){
            // TODO: Switch between enabled state and disabled state.
        }
    }
    [SerializeField]
    private Tiles redTiles, blueTiles;
    // Singleton instance. Other classes access the instance indirectly through
    // static methods GameManager.ToggleTiles and GameManager.Restart.
    private static GameManager instance = null;
    private void Awake() {
        instance = this;
        // TODO: Initialize the states of red tiles and blue tiles.
    }
    private void Start() {
        // TODO: Fade in.
    }
    public static void ToggleTiles(){
        // TODO: Toggle red tiles and blue tiles.
    }
    public static void Restart(){
        // TODO: Reload current scene and start fading.
        // ! The scene should not change until fading completes.
    }
}
