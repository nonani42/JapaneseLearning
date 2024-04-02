using System;
using System.Collections.Generic;

namespace TestSpace
{
    internal class TestCreationModel
    {
        private LoadSaveController _loadSaveController; 
        private KanjiSO[] _allKanjiList;
        private ChoosingPanelView _choosingPanelView;

        private List<TestController> _controllersList = new();
        private List<TestQuestionPanelView> _panelViewsList = new();


        public TestCreationModel(LoadSaveController loadSaveController, KanjiSO[] allKanjiList, ChoosingPanelView choosingPanelView) 
        { 
            _loadSaveController = loadSaveController;
            _allKanjiList = allKanjiList;
            _choosingPanelView = choosingPanelView;
        }

        public void InitTest(TestType type, TestQuestionPanelView panelView)
        {
            panelView.View.Init();
            var temp = new TestController(panelView, _allKanjiList, _loadSaveController.KnownKanjiList);
            Subscribe(temp);

            Action[] call = new Action[]
            {
                panelView.Show,
                temp.Init,
            };

            if(type == TestType.oral)
                _choosingPanelView.SubscribeToOralTestButton(call, panelView.View.TestName);
            if(type == TestType.writing)
                _choosingPanelView.SubscribeToWritingTestButton(call, panelView.View.TestName);

            _controllersList.Add(temp);
            _panelViewsList.Add(panelView);
        }

        private void Subscribe(TestController controller)
        {
            _loadSaveController.OnKnownKanjiChange += controller.SetKnownKanji;
            _loadSaveController.OnOralQuestionsChange += controller.SetTestLength;
        }

        private void Unsubscribe(TestController controller)
        {
            _loadSaveController.OnKnownKanjiChange -= controller.SetKnownKanji;
            _loadSaveController.OnOralQuestionsChange -= controller.SetTestLength;
        }

        public void Destroy()
        {
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
