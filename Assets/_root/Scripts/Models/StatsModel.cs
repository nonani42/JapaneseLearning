using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TestSpace
{
    internal class StatsModel<T> where T : IStat
    {
        private Color _headerColor;
        private Transform _statHolder;
        private StatTextView _statTextPrefab;

        private List<StatTextView> _activeStatViews = new();
        private List<StatTextView> _inactiveStatViews = new();

        public StatsModel(Transform parent, StatTextView prefab, Color headerColor)
        {
            _statHolder = parent;
            _statTextPrefab = prefab;
            _headerColor = headerColor;
        }

        internal void ClearStatScreen()
        {
            if (_activeStatViews.Count >= 0)
            {
                for (int i = _activeStatViews.Count - 1; i >= 0; i--)
                {
                    ReturnToPool(_activeStatViews[i]);
                }
            }
        }

        internal void DoStats(Dictionary<T, bool> dictionary)
        {
            ClearStatScreen();
            CreateStat(dictionary, "All");

            for (int i = 0; i < Enum.GetNames(typeof(LevelEnum)).Length; i++)
                CreateStat(dictionary, (LevelEnum)i);
        }

        private StatTextView GetFromStatPool()
        {
            StatTextView result = _inactiveStatViews.Last();
            _inactiveStatViews.Remove(result);
            return result;
        }

        private void ReturnToPool(StatTextView view)
        {
            view.TurnOn(false);
            _inactiveStatViews.Add(view);
            _activeStatViews.Remove(view);
        }

        private void CreateStat(Dictionary<T, bool> wordsDictionary, string header)
        {
            (float all, float known) calc = CalcStat(wordsDictionary);
            if (calc != (0, 0))
                SetStat(header, calc.all, calc.known, true);
        }

        private void CreateStat(Dictionary<T, bool> wordsDictionary, LevelEnum level)
        {
            (float all, float known) calc = CalcStat(wordsDictionary, level);
            if (calc != (0, 0))
                SetStat(level.ToString(), calc.all, calc.known);
        }

        private (float all, float known) CalcStat(Dictionary<T, bool> wordsDictionary, LevelEnum level)
        {
            float stat = 0;
            float statKnown = 0;

            stat = wordsDictionary.Keys.Where(t => t.Level == level).Count();

            if (stat == 0)
                return (0,0);

            statKnown = wordsDictionary.Where(t => t.Value == true && t.Key.Level == level).Count();
            return (stat, statKnown);
        }

        private (float all, float known) CalcStat(Dictionary<T, bool> wordsDictionary)
        {
            float stat = 0;
            float statKnown = 0;

            stat = wordsDictionary.Count;
            statKnown = wordsDictionary.Values.Where(t => t == true).Count();

            return (stat, statKnown);
        }
        
        private void SetStat(string type, float allType, float knownType, bool isSum = false)
        {
            StatTextView view;
            if (_inactiveStatViews.Count == 0)
                view = UnityEngine.Object.Instantiate(_statTextPrefab.gameObject, _statHolder).GetComponent<StatTextView>();
            else
                view = GetFromStatPool();

            _activeStatViews.Add(view);

            ShowStat(view, type, allType, knownType, isSum);
        }

        private void ShowStat(StatTextView view, string type, float allType, float knownType, bool isSum = false)
        {
            view.TurnOn(true);

            view.StatText.text = $"{type} {knownType}/{allType} {(int)(knownType / allType * 100)}%";

            if (isSum)
            {
                view.StatText.color = _headerColor;
                view.StatText.fontStyle = FontStyles.Bold;
            }
        }
    }
}
