# Edit Mode vs. Play Mode tests

Let’s clarify a bit what Play Mode and Edit Mode test means from the Unity Test Framework perspective: 

## Edit Mode tests

**Edit Mode** tests (also known as Editor tests) are only run in the Unity Editor and have access to the Editor code.

With Edit Mode tests you may want to test any of your [Editor extensions](https://docs.unity3d.com/Manual/ExtendingTheEditor.html) using the [UnityTest](./reference-attribute-unitytest.md) attribute. In this case, your test code runs in the [EditorApplication.update](https://docs.unity3d.com/ScriptReference/EditorApplication-update.html) callback loop. 

> **Note**: You can also control entering and exiting Play Mode from your Edit Mode test. 

Edit Mode tests should meet one of the following conditions:

* They should have an [assembly definition file](./workflow-create-test-assembly.md), that references `NUnit` and has only the Editor as a target platform:

```assembly
    "includePlatforms": [
        "Editor"
    ],
```

* Legacy condition: put tests in the project’s [Editor](https://docs.unity3d.com/Manual/SpecialFolders.html) folder.

## Play Mode tests

You can run **Play Mode** tests as a [standalone in a Player](./workflow-run-playmode-test-standalone.md) or inside the Editor. Play Mode tests allow you to exercise your game code, as the tests run as [coroutines](https://docs.unity3d.com/ScriptReference/Coroutine.html) if marked with the `UnityTest` attribute. 

Play Mode tests should correspond to the following conditions:

* Have an [assembly definition](./workflow-create-test-assembly.md) with reference to *nunit.framework.dll*. 
* Have the test scripts located in a folder with the .asmdef file.
* The test assembly should reference an assembly within the code that you need to test.

```assembly
    "references": [
        "NewAssembly"
    ],
    "optionalUnityReferences": [
        "TestAssemblies"
   ],
    "includePlatforms": [],
```

## Recommendations

### Attributes

Use the [NUnit](http://www.nunit.org/) `Test` attribute instead of the `UnityTest` attribute, unless you need to [yield special instructions](./reference-custom-yield-instructions.md), in Edit Mode, or if you need to skip a frame or wait for a certain amount of time in Play Mode.

### References

Reference in your tests `UnityEngine.TestRunner` and `UnityEditor.TestRunner`, only Edit Mode tests requires the latter. The `TestAssemblies` option under `optionalUnityReference` is auto-updated into a reference to the `NUnit` and `TestRunner` assemblies.