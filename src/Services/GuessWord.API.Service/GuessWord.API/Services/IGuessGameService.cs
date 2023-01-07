using System;
using GuessWord.API.Model;

namespace GuessWord.API.Services
{
    public interface IGuessGameService
    {
        GuessGame Create(Settings settings);

        GuessGame Load(long id);

        IEnumerable<GuessGame> List();

        DateTime Start(long id);

        void Game(long id, string word);

        DateTime End(long id);
    }
}