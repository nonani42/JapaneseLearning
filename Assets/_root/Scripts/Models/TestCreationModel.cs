using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    internal class TestCreationModel
    {
        private LoadSaveController _loadSaveController; 
        private KanjiCardSO[] _allKanjiList;
        private KanaSO[] _allKanaList;
        private KeySO[] _allKeyList;
        private ChoosingPanelView _choosingPanelView;

        private List<ITestController> _controllersList = new();
        private List<TestQuestionPanelView> _panelViewsList = new();

        private Transform _panelViewParent;
        private Action _returnCallback;

        public TestCreationModel(LoadSaveController loadSaveController, KanjiCardSO[] allKanjiList, KanaSO[] allKanaList, KeySO[] allKeyList, ChoosingPanelView choosingPanelView, Transform panelViewParent, Action returnCallback) 
        { 
            _loadSaveController = loadSaveController;
            _allKanjiList = allKanjiList;
            _allKanaList = allKanaList;
            _allKeyList = allKeyList;
            _choosingPanelView = choosingPanelView;
            _panelViewParent = panelViewParent;
            _returnCallback = returnCallback;
        }

        public void InitTest(Test test)
        {
            TestQuestionPanelView tempView = GameObject.Instantiate(test.TestViewPrefab.gameObject, _panelViewParent).GetComponent<TestQuestionPanelView>();
            tempView.View.Header.text = test.TestName;
            tempView.TestObject = test.TestObjectType;
            tempView.OnBack += _returnCallback;
            tempView.OnBack += tempView.Hide;
            tempView.Hide();

            ITestController tempController;
            switch (test.TestObjectType)
            {
                case TestObjectEnum.Kanji:
                    tempController = new KanjiTestController(tempView, _allKanjiList, _loadSaveController.KnownKanjiList);
                    SubscribeKanji(tempController as KanjiTestController, test.TestType);
                    break;
                case TestObjectEnum.Kana:
                    tempController = new KanaTestController(tempView, _allKanaList);
                    break;
                case TestObjectEnum.Key:
                    tempController = new KeyTestController(tempView, _allKeyList);
                    break;
                default:
                    Debug.Log("No sutable test controller!");
                    tempController = new StubTestController();
                    break;
            }

            Action[] call = new Action[]
            {
                tempView.Show,
                tempController.Init,
            };

            if(test.TestObjectType == TestObjectEnum.Kanji)
            {
                if (test.TestType == TestType.oral)
                    _choosingPanelView.SubscribeToOralTestButton(call, test.TestButtonName);
                else if (test.TestType == TestType.writing)
                    _choosingPanelView.SubscribeToWritingTestButton(call, test.TestButtonName);
            }
            else if(test.TestObjectType == TestObjectEnum.Kana)
            {
                _choosingPanelView.SubscribeToKanaTestButton(call, test.TestButtonName);
            }
            else if(test.TestObjectType == TestObjectEnum.Key)
            {
                _choosingPanelView.SubscribeToKeyTestButton(call, test.TestButtonName);
            }
            else
            {
                Debug.Log("No sutable test type!");
            }


            _controllersList.Add(tempController);
            _panelViewsList.Add(tempView);

            SubscribeQuestionsNum(tempController, test.TestType);
        }

        private void SubscribeKanji(KanjiTestController controller, TestType testType)
        {
            _loadSaveController.OnKnownKanjiChange += controller.SetKnownKanji;
        }

        private void SubscribeQuestionsNum(ITestController controller, TestType testType)
        {
            if (testType == TestType.oral)
                _loadSaveController.OnOralQuestionsChange += controller.SetTestLength;
            if (testType == TestType.writing)
                _loadSaveController.OnWritingQuestionsChange += controller.SetTestLength;
        }

        private void UnsubscribeKanji(KanjiTestController controller)
        {
            _loadSaveController.OnKnownKanjiChange -= controller.SetKnownKanji;
        }

        private void UnsubscribeQuestionsNum(ITestController controller)
        {
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
                UnsubscribeQuestionsNum(_controllersList[i]);
                if(_controllersList[i] is KanjiTestController)
                    UnsubscribeKanji(_controllersList[i] as KanjiTestController);
                _controllersList[i].Destroy();
            }
            _controllersList.Clear();
        }
    }
}
