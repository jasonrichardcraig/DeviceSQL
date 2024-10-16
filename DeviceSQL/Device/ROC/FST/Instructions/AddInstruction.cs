#region Imported Types

using DeviceSQL.Device.Roc.FST.Arguments;
using System;

#endregion

namespace DeviceSQL.Device.Roc.FST.Instructions
{
    public class AddInstruction : InstructionBase<DatabasePointOrConstantValueArgument, NullArgument>
    {

        #region Properties

        public override InstructionCategory InstructionCategory
        {
            get
            {
                return InstructionCategory.Math;
            }
        }

        public override InstructionType InstructionType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override byte NumberOfArguments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DatabasePointOrConstantValueArgument Argument1
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override NullArgument Argument2
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

    }
}
