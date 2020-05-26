using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class BallVisualAnimator : MonoBehaviour
    {
        public int animtionSpeedRatio = 3;

        private Rigidbody2D body;
        private SpritesheetAnimator animator;

        public float animationAplitudeMin = 1;
        public ParticleSystem particleSystem;

        void Start()
        {
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 vitesse = body.velocity;
            float amplitude = vitesse.magnitude;
            animator.animationSpeed = amplitude * animtionSpeedRatio;
            body.rotation = Mathf.Rad2Deg * Mathf.Atan2(vitesse.y, vitesse.x);
            particleSystem.gameObject.SetActive(amplitude > animationAplitudeMin);
            particleSystem.startColor = new Color(0, 255 % animator.animationSpeed, 0);
        }

        /*
        void OnCollisionEnter2D(Collision2D col)
        {
            transform.localScale *= 1.1f;
            Debug.Log("hop !");
        }
        */
    }
}
