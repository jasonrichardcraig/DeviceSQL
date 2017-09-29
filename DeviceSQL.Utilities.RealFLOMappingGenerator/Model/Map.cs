#region Imported Types

using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Model
{
    [Serializable]
    public class Map
    {

        #region Properties

        public Guid Id
        {
            get;
            set;
        }

        public byte[] HelpFileBytes
        {
            get;
            set;
        }

        public List<Enron.Register> EnronRegisters
        {
            get;
            set;
        }

        public List<TeleBUS.Register> TeleBUSRegisters
        {
            get;
            set;
        }

        public List<Enron.Archive> EnronArchives
        {
            get;
            set;
        }

        public List<TeleBUS.Archive> TeleBUSArchives
        {
            get;
            set;
        }

        public List<Enron.Event> EnronEvents
        {
            get;
            set;
        }

        public List<TeleBUS.Event> TeleBUSEvents
        {
            get;
            set;
        }

        #endregion

    }
}
