using System;
using System.Transactions;
using GuessWord.API.Model;
using GuessWord.API.Repository;

namespace GuessWord.API.Services
{
    /// <inheritdoc />
    internal sealed class GuessGameService : IGuessGameService
    {
        private readonly IGuessGameRepository guessGameRepository;
        private readonly ISettingsRepository settingsRepository;
        private readonly IWordModelRepository wordModelRepository;
        private readonly IHistoryItemRepository historyItemRepository;

        public GuessGameService(IGuessGameRepository guessGameRepository, ISettingsRepository settingsRepository, IWordModelRepository wordModelRepository, IHistoryItemRepository historyItemRepository)
        {
            this.guessGameRepository = guessGameRepository;
            this.settingsRepository = settingsRepository;
            this.wordModelRepository = wordModelRepository;
            this.historyItemRepository = historyItemRepository;
        }

        /// <inheritdoc />
        public GuessGame Create(Settings settings)
        {
            using var scope = new TransactionScope();
            var game = new GuessGame
            {
                CreationDate = DateTime.UtcNow
            };
            guessGameRepository.Save(game);
            settings.GuessGame = game;
            settingsRepository.Save(settings);
            scope.Complete();
            return game;
        }

        /// <inheritdoc />
        public GuessGame Load(long id)
        {
            var game = guessGameRepository.Load(id);
            return game;
        }

        /// <inheritdoc />
        public IEnumerable<GuessGame> List()
        {
            return guessGameRepository.Find();
        }

        /// <inheritdoc />
        public DateTime Start(long id)
        {
            using var scope = new TransactionScope();
            var game = guessGameRepository.Load(id);
            if (game.EndDate.HasValue)
            {
                throw new AggregateException("Игра уже была завершена");
            }

            if (game.StartDate.HasValue)
            {
                throw new AggregateException("Игра уже была начата");
            }

            game.StartDate = DateTime.UtcNow;
            guessGameRepository.Save(game);
            scope.Complete();
            return game.StartDate.Value;
        }

        /// <inheritdoc />
        public void Game(long id, string word)
        {
            using var scope = new TransactionScope();
            var game = guessGameRepository.Load(id);
            if (!game.StartDate.HasValue)
            {
                throw new AggregateException("Игра не начиналась");
            }

            if (game.EndDate.HasValue)
            {
                throw new AggregateException("Игра уже была завершена");
            }

            var history = new HistoryItem();
            history.CreationDate = DateTime.UtcNow;
            history.GuessGame = game;
            var wordModel = new WordModel();
            wordModel.Word = word;
            wordModel.HistoryItem = history;
            game.History.Add(history);
            wordModelRepository.Save(wordModel);
            historyItemRepository.Save(history);
            guessGameRepository.Save(game);
        }

        /// <inheritdoc />
        public DateTime End(long id)
        {
            using var scope = new TransactionScope();
            var game = guessGameRepository.Load(id);
            if (game.EndDate.HasValue)
            {
                throw new AggregateException("Игра уже была завершена");
            }

            if (!game.StartDate.HasValue)
            {
                throw new AggregateException("Нельзя завершить неначатую игру");
            }

            game.EndDate = DateTime.UtcNow;
            guessGameRepository.Save(game);
            scope.Complete();
            return game.StartDate.Value;
        }
    }
}