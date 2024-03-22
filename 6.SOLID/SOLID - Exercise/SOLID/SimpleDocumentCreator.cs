namespace SOLID
{
    public class SimpleDocumentCreator: IComplexCreator
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

        public void AddHeader()
        {
            this.document.Header = "Error: Cannot set header with simple creator";
        }

        public void AddBody()
        {
            this.document.Body = "Body is set";
        }

        public void AddFooter()
        {
            this.document.Footer = "Error: Cannot set footer with simple creator";
        }

        public Document GetDocument()
        {
            Document d = this.document;
            this.Reset();
            return d;
        }
    }
}
