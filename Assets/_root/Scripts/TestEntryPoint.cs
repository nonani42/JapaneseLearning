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
        [SerializeField] private TestQuestionPanelView _readingToMeaningPanelView;

        private TestCreationModel _testCreationModel;

        private List<IController> _controllers = new List<IController>();
        private LoadSaveController _loadSaveController;
        private KanjiListController _kanjiListController;

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
            set => _kanjiListController = value;
        }


        private void Start()
        {
            _testCreationModel = new(LoadSaveController, _allKanjiList.KanjiList, _choosingPanelView);
            CreateTests();
            Subscribe();
            _choosingPanelView.Init(LoadSaveController.KnownKanjiList.Count, LoadSaveController.QuestionsNum.oralQuestionsNum, LoadSaveController.QuestionsNum.writingQuestionsNum);
            SetStartingView();
        }

        private void CreateTests()
        {
            _testCreationModel.InitTest(TestType.oral, _kanjiToReadingPanelView);
            _testCreationModel.InitTest(TestType.oral, _kanjiToMeaningPanelView);
            _testCreationModel.InitTest(TestType.oral, _readingToMeaningPanelView);
        }

        private void InitKanjiListController() => KanjiListController.Init();

        private void SetStartingView()
        {
            _choosingPanelView.Show();
            _kanjiPanelView.Hide();
            _kanjiToReadingPanelView.Hide();
            _kanjiToMeaningPanelView.Hide();
            _readingToMeaningPanelView.Hide();
        }

        private void OnDestroy()
        {
            Unsubscribe();
            _testCreationModel.Destroy();
            for (int i = 0; i < _controllers.Count; i++)
                _controllers[i].Destroy();
        }

        private void Subscribe()
        {
            _kanjiPanelView.OnBack += SetStartingView;
            _kanjiToReadingPanelView.OnBack += SetStartingView;
            _kanjiToMeaningPanelView.OnBack += SetStartingView;
            _readingToMeaningPanelView.OnBack += SetStartingView;

            _choosingPanelView.SubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.SubscribeToKanjiListButton(InitKanjiListController);

            KanjiListController.OnKnownKanjiListUpdate += LoadSaveController.UpdateKnownKanji;

            LoadSaveController.OnKnownKanjiChange += _choosingPanelView.OnKnownKanjiChange;

            _choosingPanelView.SubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);

            _choosingPanelView.SubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }

        private void Unsubscribe()
        {
            _kanjiPanelView.OnBack -= SetStartingView;
            _kanjiToReadingPanelView.OnBack -= SetStartingView;
            _kanjiToMeaningPanelView.OnBack -= SetStartingView;
            _readingToMeaningPanelView.OnBack -= SetStartingView;

            _choosingPanelView.UnsubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.UnsubscribeToKanjiListButton(InitKanjiListController);

            KanjiListController.OnKnownKanjiListUpdate -= LoadSaveController.UpdateKnownKanji;

            LoadSaveController.OnKnownKanjiChange -= _choosingPanelView.OnKnownKanjiChange;

            _choosingPanelView.UnsubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);

            _choosingPanelView.UnsubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }
    }
}
