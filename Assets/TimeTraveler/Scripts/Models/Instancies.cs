

namespace Model
{
    

    public static class Instancies
    {
        private static IMover _mover;
        public static IMover Mover
        {
            get
            {
                return _mover;
            }

            set
            {
                if (_mover == null)
                {
                    _mover = value;
                }
            }
        }

        private static TurnsManager _turnsManager;
        public static TurnsManager TurnsManager
        {
            get
            {
                return _turnsManager;
            }
            set
            {
                if (_turnsManager == null)
                {
                    _turnsManager = value;
                }
            }
        }

        public static void Refresh()
        {
            _turnsManager = null;
            _mover = null;
        }
    }
}
