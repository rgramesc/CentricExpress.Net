namespace SOLID
{
    public interface IComplexCreator
    {
        void AddHeader();
        void AddBody();
        void AddFooter();

        public Document GetDocument();
    }
}
