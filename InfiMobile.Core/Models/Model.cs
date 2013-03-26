using Cirrious.MvvmCross.Plugins.Sqlite;

namespace InfiMobile.Core.Models
{
    public class Model
    {
        // local primary key
        private int _autoID;
        [PrimaryKey, AutoIncrement]
        public int AutoID
        {
            get 
            { 
                return _autoID; 
            }

            set
            {
                _autoID = value;
                if (value > 0 && (State == PersistentState.Created || State == PersistentState.Unknown))
                    State = PersistentState.Loaded;
                else
                    State = PersistentState.Created;
            }
        }

        // remote primary key (created by server)
        public int RemotedID { get; set; }

        [Ignore]
        public PersistentState State { get; set; }


        public Model()
        {
            AutoID = -1;
        }

        public virtual void Detach()
        {
            AutoID = -1;
        }

        public bool Equals(Model other)
        {
            if (other != null)
                return AutoID == other.AutoID;
            return false;
        }
    }
}
