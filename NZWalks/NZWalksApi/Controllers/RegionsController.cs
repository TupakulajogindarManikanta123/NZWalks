using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller

       
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
           
        }

        

        [HttpGet]
       public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();
            //return dto regions
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>

            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);

            //});

           var regionsDTO =  mapper.Map<List<Models.DTO.Region>>(regions);


            return Ok(regionsDTO);   
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);   
        }

        [HttpPost]
        
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {

            // request(dto) to domain model\

            var region = new Models.Domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,

            };



            //pass details to repository

            region = await regionRepository.AddAsync(region);

            //convert back to dto
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };

            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO.Id}, regionDTO);



        }

        [HttpDelete ]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from db
            var region = await regionRepository.DeleteAsync(id);
            //if null send not found
            if (region == null)
            {
                return NotFound();
            }
            // if we find  convert response to dto

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };

            // return ok
            return Ok(regionDTO);
          
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody ] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //convert dto to domain model
            var region = new Models.Domain.Region()
            {
                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,

            };

            //update region using repository
            region = await regionRepository.UpdateAsync(id, region);

            //if null then not found
            if (region == null)
            {
                return NotFound();
            }

            //convert domain back to dto
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };


            //return ok
            return Ok(regionDTO);

        }
    }

}

