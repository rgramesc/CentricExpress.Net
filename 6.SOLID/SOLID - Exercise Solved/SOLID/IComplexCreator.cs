namespace SOLID
{
    public interface IComplexCreator: ISimpleCreator
    {
        void AddHeader();
        void AddFooter();
    }
}
