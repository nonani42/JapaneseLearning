using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    public class TestEntryPoint : MonoBehaviour
    {
        [SerializeField] private KanjiListSO _allKanjiList;
        [SerializeField] private ChoosingPanelView _choosingPanelView;
        [SerializeField] private KanjiListPanelView _kanjiPanelView;
        [SerializeField] private KanjiToReadingPanelView _kanjiToReadingPanelView;
        [SerializeField] private KanjiToMeaningPanelView _kanjiToMeaningPanelView;

        private List<IController> _controllers = new List<IController>();
        private KanjiListController _kanjiListController;
        private KanjiToReadingController _kanjiToReadingController;
        private List<string> _knownKanjiList = new();
        private (int oralQuestionsNum, int writingQuestionsNum) _questionsNum;
        private LoadSaveModel _loadSaveModel = new();

        internal KanjiListController KanjiListController 
        { 
            get 
            {
                if (_kanjiListController == null)
                {
                    _kanjiListController = new KanjiListController(_kanjiPanelView, _allKanjiList.KanjiList);
                    _controllers.Add(_kanjiListController);
                }
                return _kanjiListController;
            } 
            set => _kanjiListController = value; }

        public KanjiToReadingController KanjiToReadingController 
        { 
            get 
            {
                if (_kanjiToReadingController == null)
                {
                    _kanjiToReadingController = new KanjiToReadingController(_kanjiToReadingPanelView, _allKanjiList.KanjiList);
                    _controllers.Add(_kanjiToReadingController);
                }

                return _kanjiToReadingController;
            } 
            set => _kanjiToReadingController = value; }

        private void Start()
        {
            Subscribe();
            GetKnownKanji(_loadSaveModel.LoadKnownKanji());
            _questionsNum = _loadSaveModel.LoadQuestionsNumber();
            _choosingPanelView.Init(_knownKanjiList.Count, _questionsNum.oralQuestionsNum, _questionsNum.writingQuestionsNum);

            SetStartingView();
        }

        private void CreateKanjiListController() => KanjiListController.Init();

        private void CreateReadingController() => KanjiToReadingController.Init();

        private void SetStartingView()
        {
            _choosingPanelView.Show();
            _kanjiPanelView.Hide();
            _kanjiToReadingPanelView.Hide();
            _kanjiToMeaningPanelView.Hide();
        }

        private void GetKnownKanji(List<string> kanjiList)
        {
            _knownKanjiList = kanjiList;
        }

        private void GetOralQuestions(int oralQuestions)
        {
            _questionsNum.oralQuestionsNum = oralQuestions;
        }

        private void GetWritingQuestions(int writingQuestions)
        {
            _questionsNum.writingQuestionsNum = writingQuestions;
        }

        private void OnDestroy()
        {
            _loadSaveModel.SaveQuestionsNumber(_questionsNum.oralQuestionsNum, _questionsNum.writingQuestionsNum);
            Unsubscribe();

            for (int i = 0; i < _controllers.Count; i++)
                _controllers[i].Destroy();
        }

        private void Subscribe()
        {
            _kanjiPanelView.OnBack += SetStartingView;
            _kanjiToReadingPanelView.OnBack += SetStartingView;
            _kanjiToMeaningPanelView.OnBack += SetStartingView;
            _choosingPanelView.SubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.SubscribeToKanjiListButton(CreateKanjiListController);
            _choosingPanelView.SubscribeToReadingButton(_kanjiToReadingPanelView.Show);
            _choosingPanelView.SubscribeToReadingButton(CreateReadingController);
            _choosingPanelView.SubscribeToMeaningButton(_kanjiToMeaningPanelView.Show);
            KanjiListController.OnKnownKanjiListUpdate += _choosingPanelView.OnKnownKanjiChange;
            KanjiListController.OnKnownKanjiListUpdate += GetKnownKanji;
            _choosingPanelView.SubscribeOralQuestionsChange(KanjiToReadingController.SetTestLength);
            _choosingPanelView.SubscribeOralQuestionsChange(GetOralQuestions);
            _choosingPanelView.SubscribeWritingQuestionsChange(GetWritingQuestions);
        }

        private void Unsubscribe()
        {
            _kanjiPanelView.OnBack -= SetStartingView;
            _kanjiToReadingPanelView.OnBack -= SetStartingView;
            _kanjiToMeaningPanelView.OnBack -= SetStartingView;
            KanjiListController.OnKnownKanjiListUpdate -= _choosingPanelView.OnKnownKanjiChange;
            KanjiListController.OnKnownKanjiListUpdate -= GetKnownKanji;
            _choosingPanelView.UnsubscribeOralQuestionsChange(KanjiToReadingController.SetTestLength);
            _choosingPanelView.UnsubscribeOralQuestionsChange(GetOralQuestions);
            _choosingPanelView.UnsubscribeWritingQuestionsChange(GetWritingQuestions);
        }
    }
}
