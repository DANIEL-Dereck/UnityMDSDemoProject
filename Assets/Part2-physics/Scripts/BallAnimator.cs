using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class BallAnimator : MonoBehaviour
    {
        public int animationSpeedRatio = 5;

        private SpritesheetAnimator animator;
        private Rigidbody2D body;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 vitesse = body.velocity;
            float amplitude = body.velocity.magnitude;

            animator.animationSpeed = amplitude * animationSpeedRatio;
            body.rotation = Mathf.Rad2Deg * Mathf.Atan2(vitesse.y, vitesse.x);
        }
    }
}
