using HockeyScripts.Types;
using UnityEngine;

namespace HockeyScripts.Main
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool wasJustClicked = true;
        private bool canMove;

        private Rigidbody2D rb;
        private Vector2 startingPosition;

        public Transform boundaryHolder;

        private Boundary playerBoundary;

        private Collider2D playerCollider;

        // Use this for initialization
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startingPosition = rb.position;
            playerCollider = GetComponent<Collider2D>();

            playerBoundary = new Boundary(boundaryHolder.GetChild(0).position.y,
                boundaryHolder.GetChild(1).position.y,
                boundaryHolder.GetChild(2).position.x,
                boundaryHolder.GetChild(3).position.x);

        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (wasJustClicked)
                {
                    wasJustClicked = false;

                    canMove = playerCollider.OverlapPoint(mousePos);
                }

                if (!canMove) return;
                var clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                        playerBoundary.Right),
                    Mathf.Clamp(mousePos.y, playerBoundary.Down,
                        playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
            else
            {
                wasJustClicked = true;
            }
        }

        public void ResetPosition()
        {
            rb.position = startingPosition;
        }
    }
}