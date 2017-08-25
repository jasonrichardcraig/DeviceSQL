#region Imported Types

using DeviceSQL.Device.ROC.FST.Arguments;

#endregion

namespace DeviceSQL.Device.ROC.FST.Instructions
{
    public abstract class InstructionBase<TArgument1, TArgument2> where TArgument1 : ArgumentBase
                                                                        where TArgument2 : ArgumentBase
    {

        #region Properties

        public abstract InstructionCategory InstructionCategory
        {
            get;
        }

        public abstract InstructionType InstructionType
        {
            get;
        }

        public abstract byte NumberOfArguments
        {
            get;
        }

        public abstract TArgument1 Argument1
        {
            get;
        }

        public abstract TArgument2 Argument2
        {
            get;
        }

        #endregion

    }
}
