using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' object");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        Destroy(other.gameObject);
        Destroy(this.gameObject);

        Instantiate(explosion, this.transform.position, this.transform.rotation);
        if (other.tag == "Player")
        {
            int playerNumber = int.Parse(other.gameObject.name[other.gameObject.name.Length - 1].ToString());

            gameController.AddScore(scoreValue, playerNumber);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver(playerNumber);
        }
        else
        {
            gameController.AddScore(scoreValue, int.Parse(other.transform.parent.name[other.transform.parent.name.Length - 1].ToString()));
        }
    }
}
