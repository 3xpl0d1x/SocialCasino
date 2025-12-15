using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.GameMechanics
{
    internal abstract class CasinoGameBase
    {
        public abstract void PlayGame();

        public delegate void GameResult();
        public event GameResult? OnWin;
        public event GameResult? OnLoose;
        public event GameResult? OnDraw;

        protected void OnWinInvoke() 
        {
            OnWin?.Invoke();
        }
        protected void OnLooseInvoke() 
        {
            OnLoose?.Invoke();
        }
        protected void OnDrawInvoke() 
        {
            OnDraw?.Invoke();
        }

        protected abstract void FactoryMethod();

        public CasinoGameBase() 
        {
            FactoryMethod();
        }

    }
}
