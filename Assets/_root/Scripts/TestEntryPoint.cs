using System.Collections.Generic;
using UnityEngine;

namespace TestSpace
{
    public class TestEntryPoint : MonoBehaviour
    {
        [Header("Data from SO")]
        [SerializeField] private KanjiListSO _allKanjiList;
        [SerializeField] private KanaListSO _allKanaList;
        [SerializeField] private KeysListSO _allKeysList;
        [SerializeField] private WordListSO _allWordsList;
        [SerializeField] private TestSO _allTestList;

        [Header("Data from Scene")]
        [SerializeField] private Transform _panelViewParent;
        [SerializeField] private LoginPanelView _loginPanelView;
        [SerializeField] private PanelView _leftPanelView;
        [SerializeField] private ChoosingPanelView _choosingPanelView;
        [SerializeField] private KanjiListPanelView _kanjiPanelView;

        private LoginController _loginController;
        private TestCreationModel _testCreationModel;

        private List<IController> _controllers = new List<IController>();
        private LoadSaveController _loadSaveController;
        private KanjiListController _kanjiListController;
        private WordsListController _wordsListController;

        internal LoadSaveController LoadSaveController
        {
            get
            {
                if (_loadSaveController == null)
                {
                    _loadSaveController = new LoadSaveController(_loginController);
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

        internal WordsListController WordsListController
        {
            get
            {
                if (_wordsListController == null)
                {
                    _wordsListController = new WordsListController(_allWordsList.WordList, LoadSaveController.KnownWordsList, LoadSaveController.KnownKanjiList, _allKanaList.KanaList);
                    _controllers.Add(_wordsListController);
                }
                return _wordsListController;
            }
            set => _wordsListController = value;
        }


        private void Start()
        {
            HideAllPanels();
            StartAuth();
        }

        private void StartAuth()
        {
            _loginController = new LoginController(_loginPanelView);
            _loginController.OnSuccessfulAuth += log => LoadStartingPanels(log);
            _loginController.InitAuth();
        }

        private void LoadStartingPanels(string login)
        {
            CreateTests();
            Subscribe();
            _choosingPanelView.Init(LoadSaveController.KnownKanjiList.Count, 
                                    LoadSaveController.KanjiQuestionsNum.oralQuestionsNum, LoadSaveController.KanjiQuestionsNum.writingQuestionsNum,
                                    _allKanaList.KanaList.Length, LoadSaveController.KanaQuestionsNum,
                                    _allKeysList.KeyList.Length, LoadSaveController.KeyQuestionsNum,
                                    LoadSaveController.KnownWordsList.Count, LoadSaveController.WordQuestionsNum.oralQuestionsNum, LoadSaveController.WordQuestionsNum.writingQuestionsNum,
                                    login);
            SetStartingView();
        }

        private void CreateTests()
        {
            _testCreationModel = new(LoadSaveController, _allKanjiList.KanjiList, _allKanaList.KanaList, _allKeysList.KeyList, _allWordsList.WordList, _choosingPanelView, _panelViewParent, SetStartingView);
            for (int i = 0; i < _allTestList.TestsArray.Length; i++)
                _testCreationModel.InitTest(_allTestList.TestsArray[i]);
        }

        private void InitKanjiListController() => KanjiListController.Init();

        private void InitWordsListController() => WordsListController.Init();

        private void SetStartingView()
        {
            HideAllPanels();
            _choosingPanelView.Show();
            _leftPanelView.Show();
        }

        private void HideAllPanels()
        {
            _loginPanelView.Hide();
            _choosingPanelView.Hide();
            _leftPanelView.Hide();
            _kanjiPanelView.Hide();
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

            _choosingPanelView.SubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.SubscribeToKanjiListButton(InitKanjiListController);

            KanjiListController.OnKnownKanjiListChange += WordsListController.ChangeKnownWords;

            KanjiListController.OnKnownKanjiListUpdate += LoadSaveController.UpdateKnownKanji;
            WordsListController.OnKnownWordsListUpdate += LoadSaveController.UpdateKnownWords;

            LoadSaveController.OnKnownKanjiChange += _choosingPanelView.OnKnownKanjiChange;
            LoadSaveController.OnKnownKanjiChange += WordsListController.KnownKanjiListChange;

            LoadSaveController.OnKnownWordsChange += _choosingPanelView.OnKnownWordsChange;

            _choosingPanelView.SubscribeKanjiOralQuestionsChange(LoadSaveController.UpdateKanjiOralQuestionsNum);

            _choosingPanelView.SubscribeKanjiWritingQuestionsChange(LoadSaveController.UpdateKanjiWritingQuestionsNum);

            _choosingPanelView.SubscribeWordOralQuestionsChange(LoadSaveController.UpdateWordOralQuestionsNum);

            _choosingPanelView.SubscribeWordWritingQuestionsChange(LoadSaveController.UpdateWordWritingQuestionsNum);

            _choosingPanelView.SubscribeKanaQuestionsChange(LoadSaveController.UpdateKanaQuestions);

            _choosingPanelView.SubscribeKeyQuestionsChange(LoadSaveController.UpdateKeyQuestions);
        }

        private void Unsubscribe()
        {
            _loginController.OnSuccessfulAuth -= log => LoadStartingPanels(log);

            _kanjiPanelView.OnBack -= SetStartingView;

            _choosingPanelView.UnsubscribeToKanjiListButton(_kanjiPanelView.Show);
            _choosingPanelView.UnsubscribeToKanjiListButton(InitKanjiListController);

            KanjiListController.OnKnownKanjiListChange += WordsListController.ChangeKnownWords;

            KanjiListController.OnKnownKanjiListUpdate -= LoadSaveController.UpdateKnownKanji;
            WordsListController.OnKnownWordsListUpdate -= LoadSaveController.UpdateKnownWords;

            LoadSaveController.OnKnownKanjiChange -= _choosingPanelView.OnKnownKanjiChange;
            LoadSaveController.OnKnownKanjiChange -= WordsListController.KnownKanjiListChange;

            LoadSaveController.OnKnownWordsChange -= _choosingPanelView.OnKnownWordsChange;

            _choosingPanelView.UnsubscribeKanjiOralQuestionsChange(LoadSaveController.UpdateKanjiOralQuestionsNum);

            _choosingPanelView.UnsubscribeKanjiWritingQuestionsChange(LoadSaveController.UpdateKanjiWritingQuestionsNum);

            _choosingPanelView.UnsubscribeWordOralQuestionsChange(LoadSaveController.UpdateWordOralQuestionsNum);

            _choosingPanelView.UnsubscribeWordWritingQuestionsChange(LoadSaveController.UpdateWordWritingQuestionsNum);

            _choosingPanelView.UnsubscribeKanaQuestionsChange(LoadSaveController.UpdateKanaQuestions);

            _choosingPanelView.UnsubscribeKeyQuestionsChange(LoadSaveController.UpdateKeyQuestions);
        }
    }
}
