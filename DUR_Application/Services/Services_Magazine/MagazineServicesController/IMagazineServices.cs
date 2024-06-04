using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Magazine.AddSpareParts;
using DUR_Application.Services.Services_Magazine.ShowSpareParts;
using Microsoft.AspNetCore.Components.Forms;

namespace DUR_Application.Services.Services_Magazine.MagazineServicesController
{
    public interface IMagazineServices
    {
        Task AddSparePartsToDb(int MagazineID);

        Task UpdatePartNumber();

        Task AddSparePart(AddSparePartsDto add, int MagazineID);

        Task<Response<ShowSparePartsDto>> GetSpareParts(SearchPartQuery query);
    }
}