using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject CurrentPlayer { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            gameObject.transform.parent = null; // only top level can use dont destroy on load
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetCurrentPlayer(GameObject player)
    {
        CurrentPlayer = player;
    }

    // Damages the player if the ancestor of the collider is the player
    public void DamagePlayerIfHit(Collider otherCollider) {
        GameObject player = ObjectHelper.FindAncestorWithTag(otherCollider.gameObject, "Player");
        if (player != null) {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null) playerController.TakeDamage(1);
        }
    }
}