using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ServiceModel;
using NSEmbroidery.Core;
using System.IO;
using System.ServiceModel.Web;

namespace NSEmbroidery.Core.Interfaces
{
    [ServiceContract]
    public interface IEmbroideryCreatorService
    {
        [OperationContract]
        Bitmap GetEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, GridType type);

        [OperationContract(Name="PossibleResolutionsCount")]
        Dictionary<string, int> PossibleResolutions(Bitmap image, int cellsCount, int countResolutions);

        [OperationContract]
        Dictionary<string, int> PossibleResolutions(Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient);
    }
}