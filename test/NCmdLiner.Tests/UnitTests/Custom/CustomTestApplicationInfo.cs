namespace NCmdLiner.Tests.UnitTests.Custom
{
    public class CustomTestApplicationInfo : IApplicationInfo
    {
        //private string _name;

        public string Name
        {
            get
            {
                throw new CustomTestApplicationInfoException();
                //return _name;
            }
            set
            {
                throw new CustomTestApplicationInfoException();
                //_name = value;
            }
        }

        public string Version { get; set; }
        public string Copyright { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string ExeFileName { get; set; }
    }
}