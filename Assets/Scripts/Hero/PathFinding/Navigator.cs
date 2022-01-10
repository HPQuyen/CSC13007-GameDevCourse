using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


namespace GameDevCourse.Hero
{
    public class Navigator : MonoBehaviour
    {
        #region Private Fields

        #region Movement

        private Seeker seeker;
        private Path path;

        private int currentWaypoint = 0;
        private float nextWaypointDistance = 2f;

        private bool reachedEndOfPath = false;

        #endregion

        #endregion


        #region MonoBehaviour Callbacks

        private void OnDisable()
        {
            seeker.pathCallback -= OnPathUpdated;
        }

        private void Start()
        {
            seeker = GetComponent<Seeker>();
            seeker.pathCallback += OnPathUpdated;
        }

        #endregion


        #region Private Callbacks

        private void OnPathUpdated(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        #endregion


        #region Public Callbacks

        public void NavigateTarget(Vector2 current, Vector2 target)
        {
            if (seeker.IsDone())
                seeker.StartPath(current, target);
        }

        public void GoToTarget(Vector2 current, Transform transformToMove, float speed)
        {
            if (path == null) 
                return;

            reachedEndOfPath = false;

            float distanceToWaypoint;
            while (true)
            {
                distanceToWaypoint = Vector2.Distance(current, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        reachedEndOfPath = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            var speedFactor = Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance);

            var velocityFactor = 
                reachedEndOfPath ? speedFactor : 1f;

            Vector2 direction =
                ((Vector2)path.vectorPath[currentWaypoint] - current).normalized;
            Vector2 velocity =
                direction * speed * velocityFactor;

            transformToMove.position += (Vector3)velocity * Time.deltaTime;
        }

        #endregion
    }
}