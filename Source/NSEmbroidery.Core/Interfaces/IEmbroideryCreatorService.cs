using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ServiceModel;
using NSEmbroidery.Core;

namespace NSEmbroidery.Core.Interfaces
{
    [ServiceContract]
    public interface IEmbroideryCreatorService
    {
        [OperationContract]
        Bitmap GetEmbroidery(Bitmap input, Settings settings);

        [OperationContract(Name="PossibleResolutionsCount")]
        Dictionary<Resolution, int> PossibleResolutions(Bitmap image, int cellsCount, int countResolutions);

        [OperationContract]
        Dictionary<Resolution, int> PossibleResolutions(Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient);

    }
}
