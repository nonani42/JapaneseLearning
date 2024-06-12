using System;
using UnityEngine;

namespace TestSpace
{
    [CreateAssetMenu(fileName = nameof(TestSO), menuName = "Configs/" + nameof(TestSO))]
    internal class TestSO : ScriptableObject
    {
        [field: SerializeField] public Test[] TestsArray { get; private set; }
    }

    [Serializable]
    internal class Test
    {
        [field: SerializeField] public string TestName { get; private set; }
        [field: SerializeField] public TestType TestType { get; private set; }
        [field: SerializeField] public TestObjectEnum TestObjectType { get; private set; }
        [field: SerializeField] public string TestButtonName { get; private set; }
        [field: SerializeField] public TestQuestionPanelView TestViewPrefab { get; private set; }
    }
}
