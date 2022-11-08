using System;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Engine.Helpers;
using GuessTheWord.Engine.Models;

namespace GuessTheWord.Words.Games
{
    /// <summary>
    /// Игра в слова
    /// </summary>
    internal sealed class WordsGame : IGame
    {
        private readonly IRule rule;
        private readonly IDictionaryProvider provider;
        private readonly IAlphabet alphabet;
        private readonly List<string> usedWords = new();
        private readonly List<string> allWords = new();
        private string lastWord;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="rule">Правила</param>
        /// <param name="provider">Провайдер словаря</param>
        /// <param name="alphabet">Модель алфавита</param>
        public WordsGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            this.rule = rule;
            this.provider = provider;
            this.alphabet = alphabet;
        }

        /// <inheritdoc />
        public IGameResult Play(string word)
        {
            if (string.IsNullOrWhiteSpace(lastWord) && string.IsNullOrWhiteSpace(word))
            {
                // Первый раз запрос - значит игрок просит выдать первое слово нам
                Init();
                var res = GetWord(null);
                return GameResult.Ok(res);
            }

            if (string.IsNullOrWhiteSpace(word))
            {
                return GameResult.Fail("Введено пустое слово");
            }

            word = word.ToLower();
            if (!WordHelper.IsWord(word, alphabet))
            {
                return GameResult.Fail("Не походит на слово");
            }

            if (!provider.HasWord(word, rule.Culture))
            {
                return GameResult.Fail("Не знаю такого слова, может ты пытаешься меня обмануть?");
            }

            Init();
            if (string.IsNullOrWhiteSpace(lastWord))
            {
                // Отгадывать мне
                var result = GetWord(word);
                lastWord = result;
                return GameResult.Ok(result);
            }

            var length = lastWord.Length;
            if (lastWord[length - 1] != word[0])
            {
                return GameResult.Fail("Введено слово, начинающееся не с той буквы, на котором было последнее слово");
            }

            if (usedWords.Contains(word))
            {
                return GameResult.Fail("Такое слово уже называли");
            }

            // Отгадывать мне
            var fWord = GetWord(word);
            if (fWord == null)
            {
                return GameResult.Fail("Я не нашел вариантов. Ты победил");
            }

            lastWord = fWord;
            return GameResult.Ok(fWord);
        }

        private void Init()
        {
            if (allWords.Count == 0)
            {
                allWords.AddRange(provider.GetWords(new SearchRequest(rule.Culture, null)));
            }
        }

        private string GetWord(string word)
        {
            var filteredWords = word == null ? allWords : allWords.Where(a => a.StartsWith(word[^1]) && !usedWords.Contains(a)).ToList();
            if (filteredWords.Count == 0)
            {
                return null;
            }

            var findedWord = filteredWords[new Random().Next(0, filteredWords.Count - 1)];
            usedWords.Add(word);
            usedWords.Add(findedWord);
            return findedWord;
        }
    }
}