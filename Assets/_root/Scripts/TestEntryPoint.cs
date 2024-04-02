using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    public class TestEntryPoint : MonoBehaviour
    {
        [SerializeField] private KanjiListSO _allKanjiList;
        [SerializeField] private ChoosingPanelView _choosingPanelView;
        [SerializeField] private KanjiListPanelView _kanjiPanelView;
        [SerializeField] private TestQuestionPanelView _kanjiToReadingPanelView;
        [SerializeField] private TestQuestionPanelView _kanjiToMeaningPanelView;

        private List<IController> _controllers = new List<IController>();
        private LoadSaveController _loadSaveController;
        private KanjiListController _kanjiListController;
        private TestController _kanjiToReadingController;
        private TestController _kanjiToMeaningController;

        internal LoadSaveController LoadSaveController
        {
            get
            {
                if (_loadSaveController == null)
                {
                    _loadSaveController = new LoadSaveController();
                    _controllers.Add(_loadSaveController);
                }
                return _loadSaveController;
            }
            set => _loadSaveController = value;
        }

        internal KanjiListController KanjiListController 
        { 
            get 
            {
                if (_kanjiListController == null)
                {
                    _kanjiListController = new KanjiListController(_kanjiPanelView, _allKanjiList.KanjiList, LoadSaveController.KnownKanjiList);
                    _controllers.Add(_kanjiListController);
                }
                return _kanjiListController;
            } 
            set => _kanjiListController = value; }

        internal TestController KanjiToReadingController 
        { 
            get 
            {
                if (_kanjiToReadingController == null)
                {
                    _kanjiToReadingController = new TestController(_kanjiToReadingPanelView, _allKanjiList.KanjiList, LoadSaveController.KnownKanjiList);
                    _controllers.Add(_kanjiToReadingController);
                }

                return _kanjiToReadingController;
            } 
            set => _kanjiToReadingController = value; }

        internal TestController KanjiToMeaningController 
        { 
            get 
            {
                if (_kanjiToMeaningController == null)
                {
                    _kanjiToMeaningController = new TestController(_kanjiToMeaningPanelView, _allKanjiList.KanjiList, LoadSaveController.KnownKanjiList);
                    _controllers.Add(_kanjiToMeaningController);
                }

                return _kanjiToMeaningController;
            } 
            set => _kanjiToMeaningController = value; }

        private void Start()
        {
            Subscribe();
            _choosingPanelView.Init(LoadSaveController.KnownKanjiList.Count, LoadSaveController.QuestionsNum.oralQuestionsNum, LoadSaveController.QuestionsNum.writingQuestionsNum);
            SetStartingView();
        }

        private void InitKanjiListController() => KanjiListController.Init();

        private void InitReadingController() => KanjiToReadingController.Init();
        private void InitMeaningController() => KanjiToMeaningController.Init();

        private void SetStartingView()
        {
            _choosingPanelView.Show();
            _kanjiPanelView.Hide();
            _kanjiToReadingPanelView.Hide();
            _kanjiToMeaningPanelView.Hide();
        }

        private void OnDestroy()
        {
            Unsubscribe();

            for (int i = 0; i < _controllers.Count; i++)
                _controllers[i].Destroy();
        }

        private void Subscribe()
        {
            _kanjiPanelView.OnBack += SetStartingView;
            _kanjiToReadingPanelView.OnBack += SetStartingView;
            _kanjiToMeaningPanelView.OnBack += SetStartingView;
            _choosingPanelView.SubscribeToReadingButton(InitReadingController);
            _choosingPanelView.SubscribeToMeaningButton(InitMeaningController);
            _choosingPanelView.SubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.SubscribeToKanjiListButton(InitKanjiListController);
            _choosingPanelView.SubscribeToReadingButton(_kanjiToReadingPanelView.Show);
            _choosingPanelView.SubscribeToMeaningButton(_kanjiToMeaningPanelView.Show);
            LoadSaveController.OnKnownKanjiChange += _choosingPanelView.OnKnownKanjiChange;
            LoadSaveController.OnKnownKanjiChange += KanjiToReadingController.SetKnownKanji;
            LoadSaveController.OnKnownKanjiChange += KanjiToMeaningController.SetKnownKanji;
            KanjiListController.OnKnownKanjiListUpdate += LoadSaveController.UpdateKnownKanji;
            _choosingPanelView.SubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);
            LoadSaveController.OnOralQuestionsChange += KanjiToReadingController.SetTestLength;
            LoadSaveController.OnOralQuestionsChange += KanjiToMeaningController.SetTestLength;
            _choosingPanelView.SubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }

        private void Unsubscribe()
        {
            _kanjiPanelView.OnBack -= SetStartingView;
            _kanjiToReadingPanelView.OnBack -= SetStartingView;
            _kanjiToMeaningPanelView.OnBack -= SetStartingView;
            KanjiListController.OnKnownKanjiListUpdate -= LoadSaveController.UpdateKnownKanji;
            LoadSaveController.OnKnownKanjiChange -= _choosingPanelView.OnKnownKanjiChange;
            LoadSaveController.OnKnownKanjiChange -= KanjiToReadingController.SetKnownKanji;
            LoadSaveController.OnOralQuestionsChange -= KanjiToReadingController.SetTestLength;
            LoadSaveController.OnKnownKanjiChange -= KanjiToMeaningController.SetKnownKanji;
            LoadSaveController.OnOralQuestionsChange -= KanjiToMeaningController.SetTestLength;
            _choosingPanelView.UnsubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);
            _choosingPanelView.UnsubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }
    }
}
