namespace SOLID
{
    public class SimpleDocumentCreator: ISimpleCreator
    {
        private Document document = new Document();

        public SimpleDocumentCreator()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.document = new Document();
        }

        public void AddBody()
        {
            this.document.Body = "Body is set";
        }

        public Document GetDocument()
        {
            Document d = this.document;
            this.Reset();
            return d;
        }
    }
}
