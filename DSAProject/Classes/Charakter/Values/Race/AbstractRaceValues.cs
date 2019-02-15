using DSAProject.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAProject.Classes.Charakter.Values.Race
{
    public abstract class AbstractRaceValues : IValue
    {
        #region Event
        public event EventHandler ValueChanged;
        #endregion
        #region Properties
        public int Value { get; protected set; }
        protected IRace Race { get; private set; }
        public abstract string Name { get; }
        #endregion
        public AbstractRaceValues(IRace race)
        {
            Race = race;
        }
    }
}
