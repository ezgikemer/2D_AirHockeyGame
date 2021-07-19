using HockeyScripts.Types;
using UnityEngine;

namespace HockeyScripts.Main
{
    public class AIScript : MonoBehaviour , IResettable
    {

        public float maxMovementSpeed;
        private Rigidbody2D rb;
        private Vector2 startingPosition;

        public Rigidbody2D puck;

        public Transform playerBoundaryHolder;
        private Boundary playerBoundary;

        public Transform puckBoundaryHolder;
        private Boundary puckBoundary;

        private Vector2 targetPosition;

        private bool isFirstTimeInOpponentsHalf = true;
        private float offsetXFromTarget;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            startingPosition = rb.position;

            playerBoundary = new Boundary(playerBoundaryHolder.GetChild(0).position.y,
                playerBoundaryHolder.GetChild(1).position.y,
                playerBoundaryHolder.GetChild(2).position.x,
                playerBoundaryHolder.GetChild(3).position.x);

            puckBoundary = new Boundary(puckBoundaryHolder.GetChild(0).position.y,
                puckBoundaryHolder.GetChild(1).position.y,
                puckBoundaryHolder.GetChild(2).position.x,
                puckBoundaryHolder.GetChild(3).position.x);

            UiManager.Instance.ResettableGameObjects.Add(this);

            maxMovementSpeed = GameValues.Difficulty switch
            {
                GameValues.Difficulties.Easy => 10,
                GameValues.Difficulties.Medium => 15,
                GameValues.Difficulties.Hard => 20,
                _ => maxMovementSpeed
            };
        }

        private void FixedUpdate()
        {
            if (PuckScript.WasGoal) return;
            float movementSpeed;

            if (puck.position.y > puckBoundary.Down)
            {
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }

                movementSpeed = maxMovementSpeed * Random.Range(0.2f, 0.4f);
                targetPosition = new Vector2(Mathf.Clamp(puck.position.x + offsetXFromTarget, playerBoundary.Left,
                        playerBoundary.Right),
                    startingPosition.y);
            }
            else
            {
                isFirstTimeInOpponentsHalf = true;
            
                movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);
                targetPosition = new Vector2(Mathf.Clamp(puck.position.x, playerBoundary.Left,
                        playerBoundary.Right),
                    Mathf.Clamp(puck.position.y, playerBoundary.Down,
                        playerBoundary.Up));
            }
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition,
                movementSpeed * Time.fixedDeltaTime));


        }
        public void ResetPosition()
        {
            rb.position = startingPosition;
        }

    
    }
}