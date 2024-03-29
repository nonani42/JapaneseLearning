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
        private LoadSaveController _loadSaveController;
        private KanjiListController _kanjiListController;
        private KanjiToReadingController _kanjiToReadingController;

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

        internal KanjiToReadingController KanjiToReadingController 
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
            _choosingPanelView.Init(LoadSaveController.KnownKanjiList.Count, LoadSaveController.QuestionsNum.oralQuestionsNum, LoadSaveController.QuestionsNum.writingQuestionsNum);
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
            _choosingPanelView.SubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.SubscribeToKanjiListButton(CreateKanjiListController);
            _choosingPanelView.SubscribeToReadingButton(_kanjiToReadingPanelView.Show);
            _choosingPanelView.SubscribeToReadingButton(CreateReadingController);
            _choosingPanelView.SubscribeToMeaningButton(_kanjiToMeaningPanelView.Show);
            KanjiListController.OnKnownKanjiListUpdate += _choosingPanelView.OnKnownKanjiChange;
            KanjiListController.OnKnownKanjiListUpdate += LoadSaveController.UpdateKnownKanji;
            _choosingPanelView.SubscribeOralQuestionsChange(KanjiToReadingController.SetTestLength);
            _choosingPanelView.SubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);
            _choosingPanelView.SubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }

        private void Unsubscribe()
        {
            _kanjiPanelView.OnBack -= SetStartingView;
            _kanjiToReadingPanelView.OnBack -= SetStartingView;
            _kanjiToMeaningPanelView.OnBack -= SetStartingView;
            KanjiListController.OnKnownKanjiListUpdate -= _choosingPanelView.OnKnownKanjiChange;
            KanjiListController.OnKnownKanjiListUpdate -= LoadSaveController.UpdateKnownKanji;
            _choosingPanelView.UnsubscribeOralQuestionsChange(KanjiToReadingController.SetTestLength);
            _choosingPanelView.UnsubscribeOralQuestionsChange(LoadSaveController.UpdateOralQuestionsNum);
            _choosingPanelView.UnsubscribeWritingQuestionsChange(LoadSaveController.UpdateWritingQuestionsNum);
        }
    }
}
