using System;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class MoveCharacter : MonoBehaviour
    {
        private const string ROLL = "roll";

        [Tooltip("Speed in Unit per second")] public float speed = 5f;

        private SpriteRenderer spriteRenderer;
        private SpritesheetAnimator animator;
        private Rigidbody2D body;

        // COOLDOWNS
        [Tooltip("Cooldown of a roll in seconds")]
        public float rollCooldownDuration = 1;
        private float rollCooldown = 0;

        public ParticleSystem particleSystem;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 vitesse = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                vitesse += Vector2.up;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                vitesse += Vector2.down;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                vitesse += Vector2.left;
                spriteRenderer.flipX = true;
                particleSystem.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                vitesse += Vector2.right;
                spriteRenderer.flipX = false;
                particleSystem.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && rollCooldown <= 0)
            {
                animator.Play(Anims.Roll);
                rollCooldown = rollCooldownDuration;
            }

            if (animator.CurrentAnimation.name != Anims.Roll || animator.LoopCount >= 1)
            {
                if (vitesse.magnitude > 0)
                {
                    particleSystem.gameObject.SetActive(true);
                    animator.Play(Anims.Run);
                }
                else
                {
                    particleSystem.gameObject.SetActive(false);
                    animator.Play(Anims.Iddle);
                }
            }
    
            body.velocity = vitesse.normalized * speed;

            rollCooldown -= Time.deltaTime;
        }
    }
}