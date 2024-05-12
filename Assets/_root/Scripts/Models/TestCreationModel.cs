using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    internal class TestCreationModel
    {
        private LoadSaveController _loadSaveController; 
        private KanjiCardSO[] _allKanjiList;
        private ChoosingPanelView _choosingPanelView;

        private List<TestController> _controllersList = new();
        private List<TestQuestionPanelView> _panelViewsList = new();

        private Transform _panelViewParent;
        private Action _returnCallback;

        public TestCreationModel(LoadSaveController loadSaveController, KanjiCardSO[] allKanjiList, ChoosingPanelView choosingPanelView, Transform panelViewParent, Action returnCallback) 
        { 
            _loadSaveController = loadSaveController;
            _allKanjiList = allKanjiList;
            _choosingPanelView = choosingPanelView;
            _panelViewParent = panelViewParent;
            _returnCallback = returnCallback;
        }

        public void InitTest(Test test)
        {
            var tempView = GameObject.Instantiate(test.TestViewPrefab.gameObject, _panelViewParent).GetComponent<TestQuestionPanelView>();
            tempView.View.Header.text = test.TestName;
            tempView.OnBack += _returnCallback;
            tempView.OnBack += tempView.Hide;
            tempView.Hide();

            var tempController = new TestController(tempView, _allKanjiList, _loadSaveController.KnownKanjiList);
            Subscribe(tempController, test.TestType);

            Action[] call = new Action[]
            {
                tempView.Show,
                tempController.Init,
            };

            if (test.TestType == TestType.oral)
                _choosingPanelView.SubscribeToOralTestButton(call, test.TestButtonName);
            if(test.TestType == TestType.writing)
                _choosingPanelView.SubscribeToWritingTestButton(call, test.TestButtonName);

            _controllersList.Add(tempController);
            _panelViewsList.Add(tempView);
        }

        private void Subscribe(TestController controller, TestType testType)
        {
            _loadSaveController.OnKnownKanjiChange += controller.SetKnownKanji;
            if (testType == TestType.oral)
                _loadSaveController.OnOralQuestionsChange += controller.SetTestLength;
            if (testType == TestType.writing)
                _loadSaveController.OnWritingQuestionsChange += controller.SetTestLength;
        }

        private void Unsubscribe(TestController controller)
        {
            _loadSaveController.OnKnownKanjiChange -= controller.SetKnownKanji;
            _loadSaveController.OnOralQuestionsChange -= controller.SetTestLength;
        }

        public void Destroy()
        {
            for (int i = 0; i < _panelViewsList.Count; i++)
            {
                _panelViewsList[i].OnBack -= _returnCallback;
                _panelViewsList[i].OnBack -= _panelViewsList[i].Hide;
            }

            _panelViewsList.Clear();

            for (int i = 0; i < _controllersList.Count; i++)
            {
                Unsubscribe(_controllersList[i]);
                _controllersList[i].Destroy();
            }
            _controllersList.Clear();
        }
    }
}
