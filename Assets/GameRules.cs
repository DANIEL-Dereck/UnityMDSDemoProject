using Part2;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    [Serializable]
    public struct Entity
    {
        public GameObject gameObject;
        [HideInInspector]
        public Vector3 initialPosition;
    }

    [Serializable]
    public struct Team
    {
        public GameObject goal;
        public Text score;
    }

    public Entity[] players;
    public Entity ball;

    #region EquipeGauche
    public Team left;
    private int leftScore = 0;
    #endregion

    #region EquipeDroite
    public Team right;
    private int rightScore = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].initialPosition = players[i].gameObject.transform.position;
        }

        BallCollisionEmitter emitter = ball.gameObject.GetComponentInChildren<BallCollisionEmitter>();
        emitter.OnCollided += Emitter_OnCollided;

        UpdateGame();
    }

    private void UpdateGame()
    {
        // On réinitialise la position des joueurs.
        foreach (var item in players)
        {
            item.gameObject.transform.position = item.initialPosition;
        }

        // On retire l'acceleration de la balle pour pas qu'elle bouge quand on reset sa position) 
        ball.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // On replace la balle à sa position initial.
        ball.gameObject.transform.position = ball.initialPosition;

        // On affiche les scores dans les champs de texte.
        left.score.text = leftScore.ToString();
        right.score.text = rightScore.ToString();
    }

    #region Event
    private void Emitter_OnCollided(Collision2D collider)
    {
        // Dans le cas ou la balle rentre en contact avec le but gauche.
        if (collider.collider.gameObject.name == left.goal.name)
        {
            leftScore += 1;
            UpdateGame();
        }
        // Dans le cas ou la balle rentre en contact avec le but droit.
        else if (collider.collider.gameObject.name == right.goal.name)
        {
            rightScore += 1;
            UpdateGame();
        }
    }
    #endregion
}
