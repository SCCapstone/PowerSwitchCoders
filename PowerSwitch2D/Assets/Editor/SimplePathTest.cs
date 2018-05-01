using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SimplePathTest {

	[Test]
	public void SimplePlayModeTestSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator SimplePlayModeTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame

        var currentVehicle = GameObject.Find("PlayerMover");
        var currentSpeed = currentVehicle.GetComponent<FollowPath>().Speed;

        //yield return new WaitForSeconds(5.0f);
        yield return null;

        Assert.AreEqual(currentSpeed, 0.0f);

		
	}

}
