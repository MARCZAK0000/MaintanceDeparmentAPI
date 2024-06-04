using AutoMapper;
using DUR_Application.Entities;
using DUR_Application.Exceptions;
using DUR_Application.Helper;
using DUR_Application.Model;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_Magazine.AddSpareParts;
using DUR_Application.Services.Services_Magazine.ShowSpareParts;
using DUR_Application.Services.Services_Magazine.SpareParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace DUR_Application.Services.Services_Magazine.MagazineServicesController
{
    public class MagazineServices : IMagazineServices
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;

        private readonly ILogger<MagazineServices> _logger;

        public MagazineServices(DatabaseContext databaseContext, IMapper mapper, ILogger<MagazineServices> logger)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddSparePartsToDb(int MagazineID)
        {
            var getData = await DataFromFile.ReadFile(DataFromFile.FilePath);
            var getList = DataFromFile.WriteToList(getData, MagazineID);

            var listToAdd = _mapper.Map<List<SparePart>>(getList);
            var recordData = getList.Count();

            await _databaseContext.SpareParts.AddRangeAsync(listToAdd);
            await _databaseContext.SaveChangesAsync();
            _logger.LogInformation($"Add New Parts: {recordData}");

        }


        public async Task UpdatePartNumber()
        {
            var getSpareParts = await _databaseContext.SpareParts
                .ToListAsync();
            var getNotNullIndex = getSpareParts.LastOrDefault(pr=>pr.PartNumber != null);
            int index = getNotNullIndex==null? 0 : int.Parse(getNotNullIndex.PartNumber.Substring(1))+1;
            foreach (var item in getSpareParts.Where(pr=>pr.PartNumber == null))
            {

                item.PartNumber = "L"+index.ToString();
                index++;
            }
            _databaseContext.SpareParts.UpdateRange(getSpareParts);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task AddSparePart(AddSparePartsDto addSparePartsDto, int MagazineId)
        {
            addSparePartsDto.MagazineId = MagazineId;

            var result = _mapper.Map<SparePart>(addSparePartsDto);

            await _databaseContext.SpareParts.AddAsync(result);
            await _databaseContext.SaveChangesAsync();

            _logger.LogInformation($"Add Part: {addSparePartsDto.Name}");

        }

        public async Task<Response<ShowSparePartsDto>> GetSpareParts(SearchPartQuery query)
        {

            var baseQuery = _databaseContext.SpareParts
                .Where(pr => query.Search == null
                                    || pr.Name.ToLower().Contains(query.Search)
                                           || pr.Description.ToLower().Contains(query.Search));



            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columsSelector = new Dictionary<string, Expression<Func<SparePart, object>>>
                {
                    { nameof(SparePart.Name), r=>r.Name },
                    { nameof(SparePart.Type), r=>r.Type },
                    { nameof(SparePart.Price), r=>r.Price},
                };


                var selectedColum = columsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ?
                    baseQuery.OrderBy(selectedColum)
                    : baseQuery.OrderByDescending(selectedColum);
            }


            var getSpareParts = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();
                




            var spareParts = _mapper.Map<List<ShowSparePartsDto>>(getSpareParts);

            var totalItemCount = baseQuery.Count();


            var responseBuilder = new ResponseBuilder<ShowSparePartsDto>();

            var result = responseBuilder
                .SetItems(spareParts)
                .SetTotalItemsCount(totalItemCount)
                .SetItemsFrom(query.PageSize, query.PageNumber)
                .SetItemsTo(query.PageSize, query.PageNumber)
                .SetTotalPages(totalItemCount, query.PageSize)
                .Build();

           
            return result;
        }
    }
}
