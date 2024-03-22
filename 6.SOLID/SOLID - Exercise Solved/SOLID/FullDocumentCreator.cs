namespace SOLID
{
    public class FullDocumentCreator : IComplexCreator
    {
        private Document document = new Document();

        public FullDocumentCreator()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.document = new Document();
        }

        public void AddHeader()
        {
            this.document.Header = "Header is set";
        }

        public void AddBody()
        {
            this.document.Body = "Body is set";
        }

        public void AddFooter()
        {
            this.document.Footer = "Footer is set";
        }

        public Document GetDocument()
        {
            Document d = this.document;
            this.Reset();
            return d;
        }
    }
}
