using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{ 
    public class PathfinderTests
    {
        //Simple code-behavior test: Generate a Path, consisting of 2 new points, and then a car which follows that path and ends up at the end point
        [UnityTest]
        public IEnumerator CanFindNextPathPoint()
        {
            var basePath = Resources.Load("Tests/CarPath");
            var currentPath = new GameObject().AddComponent<MovementPath>();
            var currentVehicle = new GameObject().AddComponent<FollowPath>();
            var startPoint = new GameObject();
            var endPoint = new GameObject();
            var endVector = new Vector3(1, 3, 5);
            var zeroRotation = new Quaternion(0, 0, 0, 0);
            endPoint.transform.SetPositionAndRotation(endVector, zeroRotation);
            var singlePointPath = new Transform[2];
            singlePointPath.SetValue(startPoint.transform,0);
            singlePointPath.SetValue(endPoint.transform, 1);
            currentPath.PathSequence = singlePointPath;
            if (currentPath.movingTo == 1)
            {
                Debug.Log("New Movement Path Default Init");
            }
            currentVehicle.GetComponent<FollowPath>().MyPath = currentPath;

            yield return new WaitForSeconds(2.0f);
            
            Assert.AreEqual(endPoint.transform, currentVehicle.transform);
        }


        //Simple input test that checks for correct response to button click and outputs "You have clicked the button" to console if click sucessful
        [UnityTest]
        public IEnumerator PlayerClicksButton()
        {
            var newButton = new GameObject().AddComponent<Button>();
            var buttonListener = new GameObject().AddComponent<SimpleButton>();
            buttonListener.playButton = newButton;
            newButton.onClick.Invoke();
            buttonListener.ClickButton();
            yield return null;
        }

        //This test is meant to fail and output an error message from followpath.cs
        [UnityTest]
        public IEnumerator FailNoPathPointsTest()
        {
            var currentPath = new GameObject().AddComponent<MovementPath>();
            var currentVehicle = new GameObject().AddComponent<FollowPath>();
            var firstPoint = new GameObject();

            currentVehicle.MyPath = currentPath;
            var pathScript = currentPath.GetComponent<MovementPath>();
            var pathArray = pathScript.PathSequence;
            pathArray.Initialize();
            pathArray.SetValue(firstPoint, 0);
            
            yield return new WaitForSeconds(5.0f);

            Assert.AreEqual(firstPoint, pathArray[0]);
        }
    }
}
