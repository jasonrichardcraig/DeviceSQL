namespace DeviceSQL.Device.ROC.Data
{
    public class Tlp
    {

        #region Constants

        public const int TLP_LENGTH = 3;

        #endregion

        #region Fields

        private byte pointType;
        private byte logicalNumber;
        private byte parameter;

        #endregion

        #region Properties

        public byte PointType
        {
            get { return pointType; }
            set { pointType = value; }
        }

        public byte LogicalNumber
        {
            get { return logicalNumber; }
            set { logicalNumber = value; }
        }

        public byte Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        #endregion

        #region Constructor

        public Tlp(byte pointType, byte logicalNumber, byte parameter)
        {
            this.pointType = pointType;
            this.logicalNumber = logicalNumber;
            this.parameter = parameter;
        }

        #endregion

        #region Conversion Methods

        public static Tlp Parse(string tlp)
        {
            var parsedTlp1 = tlp.Trim().Split(":".ToCharArray());
            var parsedTlp2 = parsedTlp1[0].Trim().Split(".".ToCharArray());

            return new Tlp(byte.Parse(parsedTlp2[0].Trim()), byte.Parse(parsedTlp1[1].Trim()), byte.Parse(parsedTlp2[1].Trim()));

        }

        public byte[] ToArray()
        {
            return new byte[] { PointType, LogicalNumber, Parameter };
        }

        #endregion

        #region Operators

        public static bool operator ==(Tlp p1, Tlp p2)
        {

            return ((p1.PointType == p2.PointType) && (p1.LogicalNumber == p2.LogicalNumber) && (p1.Parameter == p2.Parameter));
        }


        public static bool operator !=(Tlp p1, Tlp p2)
        {
            return !(p1 == p2);
        }

        public override bool Equals(object obj)
        {
            if(obj is Tlp)
            {
                var tlp = obj as Tlp;
                return ((tlp.PointType == this.PointType) && (tlp.LogicalNumber == this.LogicalNumber) && (tlp.Parameter == this.Parameter));
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
