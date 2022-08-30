namespace IDENTITY_SERVICE.Types
{
    public class Alphabet
    {
        private Alphabet(string value) { Value = value; }

        public string Value { get; private set; }

        public static Alphabet Soft { get { return new Alphabet("abcdefghijklmnopqrstuvwxyz"); } }
        public static Alphabet Standart { get { return new Alphabet("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"); } }
        public static Alphabet Hard { get { return new Alphabet("abcdefghijklmnopqrstuvwxyz*&!#1234567890-=|:ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"); } }
    }
}