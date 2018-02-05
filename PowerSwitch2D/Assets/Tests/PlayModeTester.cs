using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{ 
    public class PathfinderTests
    {
        [UnityTest]
        public IEnumerator CanFindNextPathPoint()
        {
            var basePath = Resources.Load("Tests/CarPath");
            var currentPath = new GameObject().AddComponent<MovementPath>();
            var pathScript = currentPath.GetComponent<MovementPath>();
            var pointList = pathScript.PathSequence;

            yield return new WaitForSeconds(5.0f);

            Assert.GreaterOrEqual(1, pointList.Length);
        }
    }
}
