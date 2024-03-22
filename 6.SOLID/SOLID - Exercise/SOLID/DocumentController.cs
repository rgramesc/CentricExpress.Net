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

            IComplexCreator fullDocument = new FullDocumentCreator();
            IComplexCreator simpleDocument = new SimpleDocumentCreator();

            var result = new List<Document>
            {
                CreateDocument(fullDocument),
                CreateDocument(simpleDocument)
            };

            return result;
        }

        private Document CreateDocument(IComplexCreator creator)
        {
            creator.AddBody();
            creator.AddHeader();
            creator.AddFooter();

            return creator.GetDocument();
        }
    }
}
