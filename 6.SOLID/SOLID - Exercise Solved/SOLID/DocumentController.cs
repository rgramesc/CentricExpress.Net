using Microsoft.AspNetCore.Mvc;

namespace SOLID
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DocumentController : ControllerBase
    {
        [HttpGet]
        public List<Document> GetAllTypesOfDocuments()
        {
            ISimpleCreator fullDocument = new FullDocumentCreator();
            ISimpleCreator simpleDocument = new SimpleDocumentCreator();

            var result = new List<Document>
            {
                CreateDocument(fullDocument),
                CreateDocument(simpleDocument)
            };

            return result;
        }

        private Document CreateDocument(ISimpleCreator creator)
        {
            creator.AddBody();
            if (creator.GetType().Name.Equals("FullDocumentCreator"))
            {
                ((IComplexCreator)creator).AddHeader();
                ((IComplexCreator)creator).AddFooter();
            }

            return creator.GetDocument();
        }
    }
}
