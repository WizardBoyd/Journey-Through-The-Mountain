using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class AABBCollisionDetector<P, A>
       where P : GameObject
       where A : GameObject
    {
        private IEnumerable<P> _passiveObjects;

        /// <summary>
        /// Create an instance of the collision detector
        /// </summary>
        /// <param name="passiveObjects">passive objects don't react to collisions</param>
        public AABBCollisionDetector(IEnumerable<P> passiveObjects)
        {
            _passiveObjects = passiveObjects;
        }

        /// <summary>
        /// Detect all collisions and call a handler where a passive object *hits* an active object
        /// </summary>
        /// <param name="activeObject"></param>
        /// <param name="collisionHandler"></param>
        public bool DetectCollisions(A activeObject, Action<P, A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                if (DetectCollision(passiveObject, activeObject))
                {
                    collisionHandler(passiveObject, activeObject);
                }
            }
            return false;
        }

        /// <summary>
        /// Detect all collisions and call a handler where a passive object *hits* an active object
        /// </summary>
        /// <param name="activeObjects"></param>
        /// <param name="collisionHandler"></param>
        public bool DetectCollisions(IEnumerable<A> activeObjects, Action<P, A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                var copiedList = new List<A>();
                foreach (var activeObject in activeObjects)
                {
                    copiedList.Add(activeObject);
                }

                foreach (var activeObject in copiedList)
                {
                    if (DetectCollision(passiveObject, activeObject))
                    {
                        collisionHandler(passiveObject, activeObject);
                    }
                }
            }
            return false;
        }

        private bool DetectCollision(P passiveObject, A activeObject)
        {
            foreach (var passiveBB in passiveObject.BoundingBoxes)
            {
                foreach (var activeBB in activeObject.BoundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool DetectTriggers(IEnumerable<A> activeObjects, Action<P, A> TriggerHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                var copiedList = new List<A>();
                foreach (var activeObject in activeObjects)
                {
                    copiedList.Add(activeObject);
                }

                foreach (var activeObject in copiedList)
                {
                    if (DetectTrigger(passiveObject, activeObject))
                    {
                        TriggerHandler(passiveObject, activeObject);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DetectTrigger(P passiveObject, A activeObject)
        {
            foreach (var passiveBB in passiveObject.TriggerBoxes)
            {
                foreach (var activeBB in activeObject.TriggerBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DetectTriggers(A activeObject, Action<P, A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                if (DetectTrigger(passiveObject, activeObject))
                {
                    collisionHandler(passiveObject, activeObject);
                    return true;
                }
            }
            return false;
        }

    }
}
